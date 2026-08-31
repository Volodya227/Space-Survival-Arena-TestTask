namespace Systems.Core.Save
{
    public class SaveSystem
    {
        private ISaveProvider _provider;
        private Data.ApplicationData.DTO.ApplicationDataDTO _applicationDataDTO;
        public Data.ApplicationData.DTO.ApplicationDataDTO GetApplicationDataDTO => _applicationDataDTO;
        public SaveSystem(ISaveProvider provider = null)
        {
            _provider = provider;
        }
        public void GetData()
        {
            if (_provider != null)
            {
                if (_provider.TryLoadApplicationData(out _applicationDataDTO))
                {
                    return;
                }
            }
            _applicationDataDTO = new Data.ApplicationData.DTO.ApplicationDataDTO();// if want to save link data for next saving
        }
        public void SetProvider(ISaveProvider provider = null) {
            if (_provider == null) return;
            //switch place for saving data
            _provider = provider;
        }
        //only for rewrite to another place
        //for example: from PlayerPrefs to json
        public void SetApplicationData(Data.ApplicationData.DTO.ApplicationDataDTO applicationDataDTO)
        {
            _applicationDataDTO = applicationDataDTO;
        }
        public void Save()
        {
            if (_provider == null) return;
            _provider.SaveApplicationData(_applicationDataDTO);
        }
        public void Delete()
        {
            //delete all data
            if (_provider == null) return;
            _provider.DeleteLoadedData();
            _applicationDataDTO = null;
        }
    }
}