using UnityEngine;
namespace Systems.Enemy
{
    public class EnemySystem : MonoBehaviour
    {
        private bool _running = true;
        private bool _wasInit = false;
        private AI.EnemyCoreAI _coreAI;
        private Coroutine _tickCoroutine;
        private EnemyController[] _enemyList;
        //initialize by BootstrapScene
        private EnemySpawner _spawner;
        private EnemySpawnerSavedDataFromScene _data;
        [SerializeField] private _Data.IEnemyData[] _enemyData;//Don't see in inspector!
        [SerializeField] private _Data.EnemyDataScriptableObject[] _enemyDataScriptableObject;

        [SerializeField] private EnemyController _prefab;
        [SerializeField] private Resources.ItemFactory _itemFactory = new();

        // Spawner, keeping all data from out side, and saving links on all enemies

        //for moving to target need create abstact for change input for this action
        //for getting information from Scene by EnemyContainerData, which must be update a distance by raycasts

        //need save the EnemyController in Array with max Lenght, and using null value for disabled object which moving to pool in Spawner
        private Share.TargetForEnemy _target;
        public void GetInitData(EnemySpawnerSavedDataFromScene data, _Data.IEnemyData[] enemyData)
        {
            _data = data;
            if (enemyData != null)
            {
                _enemyData = enemyData;
            }
            else
            {
                _enemyData = _enemyDataScriptableObject;
            }
        }
        public void SetTarget(Share.TargetForEnemy target)
        {
            _target = target;
            _coreAI.SetTarget(_target);
        }
        private void Start()
        {
            Init();
        }
        public void Init()
        {
            if (_wasInit)
                return;
            _wasInit = true;
            _running = true;
            _spawner = new EnemySpawner(_data, _enemyData, _prefab);
            _enemyList = new EnemyController[100];
            _coreAI = new AI.EnemyCoreAI(_enemyList.Length);
            _coreAI.SetTarget(_target);
            //SpawnGroupEnemy();
            _tickCoroutine = StartCoroutine(TickCoroutine());
        }
        private void OnDestroy()
        {
            ClearEnemy(null, true);
            StopCoroutine(_tickCoroutine);
        }
        public void StopGame()
        {
            _running = false;
            StopCoroutine(_tickCoroutine);
        }
        public void Restart()
        {
            _running = true;
            ClearEnemy(null, true, true);
            _tickCoroutine = StartCoroutine(TickCoroutine());
        }
        private void ReturnToPool(EnemyController controller)
        {
            if (controller == null) return;
            ClearEnemy(controller);
            _spawner.ReturnDisposedEnemy(controller);
        }
        private void CreateNewEnemy(int indexSpawnPoint = -1, int type = -1)
        {
            EnemyController controller = _spawner.GetNewEnemy(indexSpawnPoint, type);
            AddEnemy(controller);
            controller.SetInput(_coreAI.Bind(controller.GetContainerData));
        }
        private void ClearEnemy(EnemyController controller = null, bool lose = false, bool returnToPool = false)
        {
            //must rewrite this method on three another!
            for (int i = 0; i < _enemyList.Length; i++) {
                if (lose)
                {
                    if (_enemyList[i] != null)
                    {
                        _enemyList[i].EventDisableObject -= ReturnToPool;
                        _enemyList[i].SetInput();
                        _coreAI.Unbind(_enemyList[i].GetInput);
                        if (returnToPool)
                        {
                            _enemyList[i].DeathUnitBySystem();
                            _spawner.ReturnDisposedEnemy(_enemyList[i]);
                        }
                        _coreAI.Unbind(_enemyList[i].GetInput);
                        _enemyList[i] = null;
                    }
                }
                else if (_enemyList[i] == controller) {
                    CreateResource(_enemyList[i].transform.position);
                    _enemyList[i].EventDisableObject -= ReturnToPool;
                    _enemyList[i].SetInput();
                    _coreAI.Unbind(_enemyList[i].GetInput);
                    _enemyList[i] = null;
                    break;
                }
            }
        }
        private void AddEnemy(EnemyController controller)
        {
            for (int i = 0; i < _enemyList.Length; i++)
            {
                if (_enemyList[i] == controller)
                {
                    break;
                }
                else if (_enemyList[i] == null)
                {
                    _enemyList[i] = controller;
                    controller.EventDisableObject += ReturnToPool;
                    break;
                }
            }
        }
        private int FreeCount()
        {
            int count = 0;
            for (int i = 0; i < _enemyList.Length; i++)
            {
                if (_enemyList[i] == null)
                {
                    count++;
                }
            }
            return count;
        }
        private void SpawnGroupEnemy()
        {
            if (!_running)
                return;

            int freeCount = Mathf.Min(FreeCount(), 10);//int groupSize = 10;
            for (int i = 0; i < freeCount; i++) {
                CreateNewEnemy(i);
            }
        }
        private System.Collections.IEnumerator TickCoroutine()
        {
            while (true)
            {
                SpawnGroupEnemy();

                yield return new WaitForSeconds(4f);
            }
        }
        private void CreateResource(Vector3 position, int value = 1)
        {
            _itemFactory.CreateItem(position, value: value);
        }
        private void FixedUpdate()
        {
            _coreAI.Tick();
        }
    }
}