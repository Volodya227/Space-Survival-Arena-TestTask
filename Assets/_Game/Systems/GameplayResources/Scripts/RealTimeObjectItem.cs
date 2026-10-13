using UnityEngine;
namespace Systems.Resources.Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class RealTimeObjectItem : MonoBehaviour
    {
        private Data.Resources.Items.TypeItemStruct _typeItem;
        private bool _wasInit = false;
        public void Init(int id = 0, int count = 1)
        {
            if (_wasInit)
                return;
            _wasInit = true;
            _typeItem = new Data.Resources.Items.TypeItemStruct(id, count);
        }
        public void Use(out Data.Resources.Items.TypeItemStruct item)
        {
            item = _typeItem;// or item.add(_typeItem); but, when id not correctly, it was problem
            Destroy();
        }
        public void Destroy()
        {
            _wasInit = false;
            Destroy(gameObject);// потім можна додати пул обєктів
        }
    }
}