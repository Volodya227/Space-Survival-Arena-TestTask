using System.IO;
using UnityEngine;
namespace Systems.Core.Save
{
    public sealed class JsonSaveProvider : ISaveProvider
    {
        //saving out side project
        private readonly string _root;
        public JsonSaveProvider()
        {
            _root = Path.Combine(Application.persistentDataPath, "saves");
            Directory.CreateDirectory(_root);
        }
        private string AppPath => Path.Combine(_root, "applicationData.json");
        //private string SlotPath(int id) => Path.Combine(_root, $"slot_{id:000}.json");

        public void SaveApplicationData(Data.ApplicationData.DTO.ApplicationDataDTO data)
        {
            if (data == null) return;
            File.WriteAllText(AppPath, JsonUtility.ToJson(data, true));
            Debug.Log($"[SaveSystem] Save folder: {_root}");
        }
        public bool TryLoadApplicationData(out Data.ApplicationData.DTO.ApplicationDataDTO data)
        {
            if (!File.Exists(AppPath))
            {
                data = default;
                return false;
            }
            data = JsonUtility.FromJson<Data.ApplicationData.DTO.ApplicationDataDTO>(File.ReadAllText(AppPath));
            return true;
        }
        public void DeleteLoadedData()
        {
            //TODO delete directory for project in hidden folders in file explorer
            if (Directory.Exists(_root))
            {
                Directory.Delete(_root, true);
                Debug.Log($"[SaveSystem] Cleared all save data in: {_root}");
            }
        }
    }
}