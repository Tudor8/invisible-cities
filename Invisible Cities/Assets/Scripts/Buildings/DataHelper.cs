#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataHelper {
    public const string DEFAULT_MENU_PATH = "Assets/Level Design/Data/";
    public const string ASSET_SUFFIX = ".asset";

    /// <summary>
    /// <para> Creates a scriptable object at a given location. </para>
    /// <para> The path will be formed like this: dataPath + '/' + folderPath + '/' + name + '.asset' </para>
    /// <para> Default dataPath is 'Assets/Data/'. </para>
    /// </summary>
    /// <typeparam name="T"> The type of the scriptable object that will be created.</typeparam>
    /// <param name="name"> The object will be called this inside of the specified folder.</param>
    /// <param name="folderPath"> 
    ///     The name the folder the asset will be created in. Will be relative to the path. 
    ///     Do not use slashes '/' at the end, as they are automatically added
    /// </param>
    public static void CreateAsset<T> (string name, string folderPath, string dataPath = "Assets/Data/") where T : ScriptableObject {
        T asset = ScriptableObject.CreateInstance<T> ();

        AssetDatabase.CreateAsset (asset, dataPath + folderPath + "/" + name + ASSET_SUFFIX);
        AssetDatabase.SaveAssets ();

        EditorUtility.FocusProjectWindow ();

        Selection.activeObject = asset;
    }
}
#endif