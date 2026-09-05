using Rossoforge.Utils.IO;
using Rossoforge.Utils.Logger;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Rossoforge.Persistence.Editor
{
    public class PersistenceEditor
    {
        [MenuItem("Rossoforge/Persistent Data/Delete Folder")]
        public static void Delete()
        {
            string path = Application.persistentDataPath + Path.DirectorySeparatorChar;

            if (Files.ExistsDirectory(path))
            {
                Files.DeleteDirectory(path);
                EditorUtility.DisplayDialog("Folder deleted", "Folder deleted successfully", "OK");
            }
            else
            {
                RossoLogger.Warning("Save folder does not exist");
            }
        }

        [MenuItem("Rossoforge/Persistent Data/Open Folder")]
        public static void OpenSaveFolder()
        {
            string path = Application.persistentDataPath + Path.DirectorySeparatorChar;

            if (Files.ExistsDirectory(path))
            {
                EditorUtility.RevealInFinder(path);
                Debug.Log("Opening save folder: " + path);
            }
            else
            {
                Debug.LogWarning("Save folder does not exist: " + path);
            }
        }
    }
}
