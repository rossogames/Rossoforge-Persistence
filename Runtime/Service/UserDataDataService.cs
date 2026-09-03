using UnityEngine;

namespace Rossoforge.UserData.Service
{
    [CreateAssetMenu(fileName = nameof(UserDataDataService), menuName = "Rossoforge/Data Service/User Data")]
    public class UserDataDataService : ScriptableObject
    {
        [field: SerializeField]
        public string FileName { get; private set; }

        [field: SerializeField]
        public string EncoderKey { get; private set; }
    }
}
