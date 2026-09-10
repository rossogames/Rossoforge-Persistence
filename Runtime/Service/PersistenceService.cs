using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Rossoforge.Services.Service;
using Rossoforge.Utils.Encoding;
using Rossoforge.Utils.IO;
using Rossoforge.Utils.Logger;
using UnityEngine;

namespace Rossoforge.Persistence.Service
{
    public abstract class PersistenceService<T> : IPersistenceService<T>, IInitializable
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

        public virtual void Initialize()
        {
            _filePath = Path.Combine(Application.persistentDataPath, _dataService.FileName);

            if (!string.IsNullOrEmpty(_dataService.EncoderKey))
                Base64Encoder.SetKey(_dataService.EncoderKey);

            Load();
        }

        protected void Save(IList<JsonConverter> customConverters = null)
        {
            var json = JsonFiles.Serialize(Data, customConverters);
            WriteToDisk(json);
        }

        protected void Save(JsonSerializerSettings customSettings)
        {
            var json = JsonFiles.Serialize(Data, customSettings);
            WriteToDisk(json);
        }

        protected void Load(IList<JsonConverter> customConverters = null)
        {
            var rawJson = GetFileContent();
            if (string.IsNullOrWhiteSpace(rawJson))
                return;

            Data = JsonFiles.Deserialize<T>(rawJson, customConverters);
        }

        protected void Load(JsonSerializerSettings customSettings)
        {
            var rawJson = GetFileContent();
            if (string.IsNullOrWhiteSpace(rawJson))
                return;

            Data = JsonFiles.Deserialize<T>(rawJson, customSettings);
        }

        protected void Delete()
        {
            if (Files.ExistsFile(_filePath))
                Files.DeleteFile(_filePath);

            Data = new T();
        }

        private void WriteToDisk(string json)
        {
            var encodedJson = string.IsNullOrEmpty(_dataService.EncoderKey) ? json : Base64Encoder.Encode(json);
            TextFiles.Save(_filePath, encodedJson);
        }

        private string GetFileContent()
        {
            if (!Files.ExistsFile(_filePath))
            {
                return null;
            }

            var json = TextFiles.Load(_filePath);
            if (string.IsNullOrEmpty(json))
            {
                RossoLogger.Error($"Save file is empty: {_filePath}");
                return null;
            }

            string rawJson = json;

            if (!string.IsNullOrEmpty(_dataService.EncoderKey))
            {
                if (!Base64Encoder.TryDecode(json, out string decodedJson))
                {
                    RossoLogger.Error($"Failed to decode save file: {_filePath}");
                    return null;
                }
                rawJson = decodedJson;
            }

            return rawJson;
        }
    }
}