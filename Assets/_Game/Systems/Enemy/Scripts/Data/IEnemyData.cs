namespace Systems.Enemy.Data
{
    public interface IEnemyData
    {
        public float Speed { get; }
        public int MaxHealth { get; }
        public View.EnemyView Prefab { get; }
        public float DistanceDamage { get; }
        public float Damage { get; }
    }
    public class EnemyDataDTO
    {
        //DTO class use for getting data from ISaveProvider, witch can get this from data base or jsons files but hidden by abstraction the realizeable
        //if get from ISaveProvider this file need move to namespace Data.Configs.Enemy
        public View.EnemyView gameObject;// need delete, or this item saving link an object in folders on prefab
        public float speed;
        public float distanceDamage;
        public float damage;
    }
    public class EnemyData : IEnemyData
    {
        private readonly EnemyDataDTO _dto;
        public EnemyData(EnemyDataDTO dto)
        {
            _dto = dto;
        }
        public float Speed => _dto.speed;
        public int MaxHealth => 100;
        public View.EnemyView Prefab => _dto.gameObject;
        public float DistanceDamage => _dto.distanceDamage;
        public float Damage => _dto.damage;
    }
}