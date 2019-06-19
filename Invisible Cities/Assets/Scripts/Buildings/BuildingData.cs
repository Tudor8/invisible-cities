using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingData : ScriptableObject {
#if UNITY_EDITOR 
    [MenuItem (DataHelper.DEFAULT_MENU_PATH + "Building Data")]
    public static void CreateAsset () {
        DataHelper.CreateAsset<BuildingData> ("building", "Buildings");
    }
#endif
}
