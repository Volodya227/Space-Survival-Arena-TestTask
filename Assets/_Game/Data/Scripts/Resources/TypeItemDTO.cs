namespace Data.Resources.Items
{
    public enum TypeOfItems { ExperienceCrystal = 0 }
    public interface ITypeItemBase
    {
        public int ID { get; }
        public int Count { get; }
    }
    public interface ITypeItem : ITypeItemBase
    {
        public event System.Action EventChangeCount;
    }
    public readonly struct TypeItemStruct : ITypeItemBase
    {
        public int ID { get; }
        public int Count { get; }
        public TypeItemStruct(int id, int count)
        {
            ID = id;
            Count = count;
        }
    }
    [System.Serializable]
    public class TypeItemDTO
    {
        public int id;
        public int count;
        public TypeItemDTO(int id, int count)
        {
            this.id = id;
            this.count = count;
        }
    }
}