using UnityEngine;
namespace Systems.Enemy.View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Transform _pointOfAttack;
        public Transform PointOfAttack => _pointOfAttack;
        //saved all references on objects in inspector for share in another layers of Enemy abstract
    }
}