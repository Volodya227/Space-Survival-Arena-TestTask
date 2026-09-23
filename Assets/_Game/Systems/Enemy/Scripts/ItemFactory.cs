using UnityEngine;
namespace Systems.Resources
{
    [System.Serializable]
    public class ItemFactory
    {
        [SerializeField] private Items.RealTimeObjectItem _itemPrefab;
        public Items.RealTimeObjectItem CreateItem(Vector3 position, int id = 0, int value = 1) {
            Items.RealTimeObjectItem item = Object.Instantiate(_itemPrefab, position: position, rotation: Quaternion.identity);
            item.Init(id, value);
            return null;
        }
    }
}