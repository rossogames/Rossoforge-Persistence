using Rossoforge.Services.Service;
using Rossoforge.Utils.Encoding;
using Rossoforge.Utils.IO;
using Rossoforge.Utils.Logger;
using System.IO;
using UnityEngine;

namespace Rossoforge.Persistence.Service
{
    public class PersistenceService<T> : IPersistenceService<T>, IInitializable
        where T : IPersistentData, new()
    {
        private PersistenceDataService _dataService;
        private string _filePath;

        public T Data { get; private set; }

        public PersistenceService(PersistenceDataService dataService)
        {
            _dataService = dataService;
            Data = new T();
        }

        public void Initialize()
        {
            _filePath = Path.Combine(Application.persistentDataPath, _dataService.FileName);

            if (!string.IsNullOrEmpty(_dataService.EncoderKey))
                Base64Encoder.SetKey(_dataService.EncoderKey);

            Load();
        }

        public void Save()
        {
            var json = JsonFiles.Serialize(Data);
            var encodedJson = string.IsNullOrEmpty(_dataService.EncoderKey) ? json : Base64Encoder.Encode(json);
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

            if (string.IsNullOrEmpty(_dataService.EncoderKey))
            {
                Data = JsonFiles.Deserialize<T>(json);
                return;
            }

            if (!Base64Encoder.TryDecode(json, out string decodedJson))
            {
                RossoLogger.Error($"Failed to decode save file: {_filePath}");
                return;
            }

            Data = JsonFiles.Deserialize<T>(decodedJson);
        }

        public void Delete()
        {
            if (Files.ExistsFile(_filePath))
                Files.DeleteFile(_filePath);

            Data = new T();
        }
    }
}
