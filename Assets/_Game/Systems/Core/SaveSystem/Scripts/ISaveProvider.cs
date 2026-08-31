namespace Systems.Core.Save
{
    public interface ISaveProvider
    {
        public void SaveApplicationData(Data.ApplicationData.DTO.ApplicationDataDTO data);
        public bool TryLoadApplicationData(out Data.ApplicationData.DTO.ApplicationDataDTO data);
        public void DeleteLoadedData();
        //TODO
        //void SaveSlot(int slotId, SaveSlotDTO data);
        //bool TryLoadSlot(int slotId, out SaveSlotDTO data);
        //void DeleteSlot(int slotId);
    }
}