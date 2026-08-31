using System.Collections.Generic;
using UnityEngine;
namespace Systems.Character
{
    [System.Serializable]
    public class SpawnCharacters
    {
        [SerializeField] private Transform[] _positions;
        public int CountPositions => _positions.Length;
        public Transform GetPosition(int i) => _positions[i];
    }
    public class CharacterSystem : MonoBehaviour
    {
        private SpawnCharacters _spawn;
        [SerializeField] private CharacterControllerBehaviour _characterControllerBehaviourPrefab;
        private readonly List<CharacterControllerBehaviour> _characters = new();
        private int _currentCharacter = -1;
        public CharacterController GetCharacter => _characters[_currentCharacter].Core;
        public void Init(SpawnCharacters spawn)
        {
            _spawn = spawn;
        }
        public void Start()
        {
            for (int i = 0; i < _spawn.CountPositions; i++) {
                InitCharacter(_spawn.GetPosition(i));
            }
        }
        public void InitCharacter(Transform position)
        {
            CharacterControllerBehaviour newItem = Instantiate(_characterControllerBehaviourPrefab, position.position, position.rotation);
            Physics.SyncTransforms();
            _characters.Add(newItem);
        }
        public void ChangeCharacter()
        {
            if (_currentCharacter >= 0)
            {
                _characters[_currentCharacter].Core.SetInput(null);
            }
            _currentCharacter++;
            _currentCharacter %= _characters.Count;
        }
    }
}