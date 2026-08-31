using UnityEngine;
public class DecalLifetime : MonoBehaviour
{
    [SerializeField] private float _lifetime = 5f;
    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }
}
