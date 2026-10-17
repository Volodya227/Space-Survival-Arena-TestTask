namespace Systems.Resources.Items
{
    public class TypeItem : Data.Resources.Items.ITypeItem
    {
        public event System.Action EventChangeCount;
        public int ID { get; private set; }
        public int Count { get; private set; }
        private readonly Data.Resources.Items.TypeItemDTO _dto;
        public Data.Resources.Items.TypeItemDTO GetDTO => (_dto == null) ? new Data.Resources.Items.TypeItemDTO(ID, Count) : UpdateDTO();
        public TypeItem(int id = 0, int count = 0)
        {
            ID = id;
            Count = count;
        }
        public TypeItem(Data.Resources.Items.TypeItemDTO dto = null)
        {
            if (dto != null)
            {
                ID = dto.id;
                Count = dto.count;
                _dto = dto;
            }
            else
            {
                ID = 0;
                Count = 0;
            }
        }
        private Data.Resources.Items.TypeItemDTO UpdateDTO()
        {
            _dto.id = ID;
            _dto.count = Count;
            return _dto;
        }
        public void Add(int count)
        {
            if (count <= 0)
                return;
            Count += count;
            EventChangeCount?.Invoke();
        }
        public void Add(Data.Resources.Items.ITypeItemBase item)
        {
            if (item == null)
                return;
            if (item.ID == ID)
                Add(item.Count);
        }
        public bool TrySpend(int count)
        {
            if (count < 0)
                return false;
            if (count == 0)
                return true;
            return count <= Count;
        }
        public bool Spend(int count)
        {
            if (TrySpend(count))
            {
                Count -= count;
                EventChangeCount?.Invoke();
                return true;
            }
            return false;
        }
    }
}