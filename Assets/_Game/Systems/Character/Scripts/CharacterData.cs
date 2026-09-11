using UnityEngine;
namespace Systems.Character.Data
{
    [CreateAssetMenu(fileName = "data Character", menuName = "Character/Data")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _maxHealth;
        public float Speed => _speed / 3.6f;
        public int MaxHealth => _maxHealth;
    }
}