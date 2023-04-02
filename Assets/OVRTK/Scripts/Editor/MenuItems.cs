using UnityEngine;
using UnityEditor;

public class MenuItems
{
    [MenuItem("OVRTK/Components/Add OVRTK VR Camera Rig")]
    [MenuItem("GameObject/OVRTK/Components/Add OVRTK VR Camera Rig")]
    public static void AddCameraRig()
    {
        // Give the path of the object to load and cache it in a variable.
        Object OVRTK_Prefab = AssetDatabase.LoadAssetAtPath("Assets/OVRTK/Prefabs/Components/OVRTK Camera Rig.prefab", typeof(GameObject));

        // Check if the prefab has already been instantiated in the scene hierarchy.
        GameObject cameraRig = GameObject.Find("OVRTK Camera Rig");
        if (cameraRig == null)
        {
            if (OVRTK_Prefab != null)
            {
                PrefabUtility.InstantiatePrefab(OVRTK_Prefab);
            }
        }
        else
        {
            Debug.LogWarning("OVRTK Camera Rig prefab has already been instantiated in the scene hierarchy.");
        }
    }
}
