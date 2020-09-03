using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace JSON
{
    /// <summary>
    /// Use this class to scan in the contents of any JSON file
    /// </summary>
    ///

    public class JSONReader : MonoBehaviour
    {
        /// <summary>
        /// Read the contents of a JSON file FROM THE RESOURCES FOLDER
        /// and save it to an object of type T
        /// Make sure the "fileName" param is in format "words"
        /// NOTE: JSON in the resources folder cannot be modified at runtime
        /// </summary>
        public static T ScanToObjectFromResources<T>(string fileName)
        {
            Debug.Log("Scanning from StreamingAssets/" + fileName + "...");

            TextAsset txtAsset = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
            string tileFile = txtAsset.text;
            T data = JsonUtility.FromJson<T>(tileFile);
            return data;   
        }

        /// <summary>
        /// Read the contents of a JSON file FROM STREAMING ASSETS
        /// and save it to an object of type T
        /// Make sure the "fileName" param is in format "words"
        /// NOTE: JSON in the resources folder can be modified at runtime
        /// AND even in the build
        /// </summary>
        public static T ScanToObjectFromStreamingAssets<T>(string fileName)
        {
            Debug.Log("Scanning from Resources/" + fileName + "...");
            string path = Application.streamingAssetsPath + "/" + fileName + ".json";

            if (path == null)
            {
                Debug.Log("No file name of " + fileName + " was found.");
                return default(T);
            }

            string json = File.ReadAllText(path);
            T data = JsonUtility.FromJson<T>(json);
            return data;
        }
    }
}
