using UnityEngine;

namespace Rossoforge.Persistence.Service
{
    public abstract class PersistenceDataService : ScriptableObject
    {
        [field: SerializeField]
        public string FileName { get; private set; }

        [field: SerializeField]
        public string EncoderKey { get; private set; }
    }
}
