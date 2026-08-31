using UnityEngine;
namespace Systems.Enemy
{
    [System.Serializable]
    public class EnemySpawnerSavedDataFromScene
    {
        [SerializeField] private Transform[] _points;
        public int GetCountPoints => _points.Length;
        public Transform GetPoint(int index) {
            return _points[Mathf.Clamp(index, 0, _points.Length - 1)];
        }
    }
    [System.Serializable]
    public class EnemySpawner
    {
        //Pool
        private readonly System.Collections.Generic.Queue<EnemyController>[] _pool;
        //there can creat a pool! "in stack<> or queue<> save disabled objects"
        private readonly EnemySpawnerSavedDataFromScene _data;
        private readonly Data.IEnemyData[] _enemyData;
        private readonly EnemyController _prefab;
        public EnemySpawner(EnemySpawnerSavedDataFromScene data, Data.IEnemyData[] enemyData, EnemyController prefab)
        {
            _pool = new System.Collections.Generic.Queue<EnemyController>[2] { new(), new() };
            _data = data;
            _enemyData = enemyData;
            _prefab = prefab;
            //this class is Factory of Enemy
        }
        public EnemyController GetNewEnemy(int indexSpawnPoint = -1, int type = -1) {
            //if some value is "-1" we get random value from "0" to "Array.Lenght-1"
            if(_enemyData.Length == 0)
                return null;
            if (_data.GetCountPoints == 0)
                return null;
            if (indexSpawnPoint < 0)
                indexSpawnPoint = Random.Range(0, _data.GetCountPoints);
            if (type < 0)
                type = Random.Range(0, _enemyData.Length);
            //!warning pool don't keep a typing, or we can do two pools on any type, but this result need create pool[] when we will have a lot of types
            EnemyController controller = GetFromPool(indexSpawnPoint, type);
            if(controller != null)
                return controller;
            return CreateNew(indexSpawnPoint, type);
            //return null;
        }
        public void ReturnDisposedEnemy(EnemyController controller) {
            _pool[controller.IDType].Enqueue(controller);
        }
        private EnemyController CreateNew(int indexSpawnPoint, int type)
        {
            EnemyController controller = Object.Instantiate(_prefab, _data.GetPoint(indexSpawnPoint).position, _data.GetPoint(indexSpawnPoint).rotation);
            controller.SetData(_enemyData[type], type);

            //TODO bind on Kill enemy event
            return controller;
        }
        private EnemyController GetFromPool(int indexSpawnPoint, int type)
        {
            if (_pool[type].Count == 0)
                return null;
            EnemyController controller = _pool[type].Dequeue();
            controller.transform.SetPositionAndRotation(_data.GetPoint(indexSpawnPoint).position, _data.GetPoint(indexSpawnPoint).rotation);
            //controller.SetData(_enemyData[type]); can't use, by static initialize, but if rewrite, we need write very hard code for lossing old references on scene
            controller.Respawn();
            return controller;
        }
    }
}