using UnityEngine;
namespace Systems.Enemy
{
    public class EnemySystem : MonoBehaviour
    {
        private EnemyController[] _enemyList;
        //initialize by BootstrapScene
        private EnemySpawner _spawner;
        private EnemySpawnerSavedDataFromScene _data;
        [SerializeField] private Data.IEnemyData[] _enemyData;//Don't see in inspector!
        [SerializeField] private Data.EnemyDataScriptableObject[] _enemyDataScriptableObject;

        [SerializeField] private EnemyController _prefab;

        // Spawner, keeping all data from out side, and saving links on all enemies

        //for moving to target need create abstact for change input for this action
        //for getting information from Scene by EnemyContainerData, which must be update a distance by raycasts

        //need save the EnemyController in Array with max Lenght, and using null value for disabled object which moving to pool in Spawner
        public void GetInitData(EnemySpawnerSavedDataFromScene data, Data.IEnemyData[] enemyData)
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
        private void Start()
        {
            _spawner = new EnemySpawner(_data, _enemyData, _prefab);
            _enemyList = new EnemyController[100];
            SpawnGroupEnemy();
        }
        private void OnDestroy()
        {
            ClearEnemy(null, true);
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
        }
        private void ClearEnemy(EnemyController controller = null, bool lose = false)
        {
            for (int i = 0; i < _enemyList.Length; i++) {
                if (lose)
                {
                    if (_enemyList[i] != null)
                    {
                        _enemyList[i].EventDisableObject -= ReturnToPool;
                        _enemyList[i] = null;
                    }
                }
                else if (_enemyList[i] == controller) {
                    controller.EventDisableObject -= ReturnToPool;
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
            int freeCount = Mathf.Min(FreeCount(), 10);//int groupSize = 10;
            for (int i = 0; i < freeCount; i++) {
                CreateNewEnemy(i);
            }
        }
    }
}