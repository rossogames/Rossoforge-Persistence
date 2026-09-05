using UnityEngine;

namespace Rossoforge.Persistence.Service
{
    [CreateAssetMenu(fileName = nameof(PersistenceDataService), menuName = "Rossoforge/Data Service/Persistence")]
    public class PersistenceDataService : ScriptableObject
    {
        [field: SerializeField]
        public string FileName { get; private set; }

        [field: SerializeField]
        public string EncoderKey { get; private set; }
    }
}
