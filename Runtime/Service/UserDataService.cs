using Rossoforge.Core.UserData;
using Rossoforge.Utils.Encoding;
using Rossoforge.Utils.IO;
using Rossoforge.Utils.Logger;
using System.IO;
using UnityEngine;

namespace Rossoforge.UserData.Service
{
    public class UserDataService<T> : IUserDataService<T>
        where T : IGameSave, new()
    {
        private UserDataServiceData _serviceData;
        private string _filePath;

        public T CurrentSave { get; private set; }

        public UserDataService(UserDataServiceData serviceData)
        {
            _serviceData = serviceData;
            CurrentSave = new T();
        }

        public void Initialize()
        {
            _filePath = Path.Combine(Application.persistentDataPath, _serviceData.FileName);

            if (!string.IsNullOrEmpty(_serviceData.EncoderKey))
                Base64Encoder.SetKey(_serviceData.EncoderKey);

            Load();
        }

        public void Save()
        {
            var json = JsonFiles.Serialize(CurrentSave);
            var encodedJson = string.IsNullOrEmpty(_serviceData.EncoderKey) ? json : Base64Encoder.Encode(json);
            TextFiles.Save(_filePath, encodedJson);
        }

        public void Load()
        {
            if (!Files.ExistsFile(_filePath))
            {
                return;
            }

            var json = TextFiles.Load(_filePath);
            if (string.IsNullOrEmpty(json))
            {
                RossoLogger.Error($"Save file is empty: {_filePath}");
                return;
            }

            if(string.IsNullOrEmpty(_serviceData.EncoderKey))
            {
                CurrentSave = JsonFiles.Deserialize<T>(json);
                return;
            }

            if (!Base64Encoder.TryDecode(json, out string decodedJson))
            {
                RossoLogger.Error($"Failed to decode save file: {_filePath}");
                return;
            }

            CurrentSave = JsonFiles.Deserialize<T>(decodedJson);
        }

        public void Delete()
        {
            if (Files.ExistsFile(_filePath))
                Files.DeleteFile(_filePath);

            CurrentSave = new T();
        }
    }
}
