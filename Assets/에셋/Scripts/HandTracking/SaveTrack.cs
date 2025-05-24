using UnityEngine;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using UnityEditor;
using System.IO;

public class SaveTrack : MonoBehaviour
{

    public List<Vector3> LTracker = new List<Vector3>();
    public List<Vector3> RTracker = new List<Vector3>();
    private Tracker TrackerInstance;
#if UNITY_EDITOR
    public void StartTracking()
    {
        InvokeRepeating(nameof(UpdateLimited), 0, 0.016f);
        TrackerInstance = Object.FindFirstObjectByType<Tracker>();
    }

    private void UpdateLimited()
    {
        LTracker.Add(TrackerInstance.LTracker);
        RTracker.Add(TrackerInstance.RTracker);
    }

    public void Save()
    {
        LTracker = new List<Vector3>();
        RTracker = new List<Vector3>();
        int i = 0;
        if(!File.Exists(Application.dataPath + "/Resources/Prefabs/" + gameObject.name + ".prefab"))
        {
            PrefabUtility.SaveAsPrefabAsset(gameObject, "Assets/Resources/Prefabs/" + gameObject.name + ".prefab");
        }
        else if(!File.Exists(Application.dataPath + "/Resources/Prefabs/" + gameObject.name + 0 + ".prefab"))
        {
            PrefabUtility.SaveAsPrefabAsset(gameObject, "Assets/Resources/Prefabs/" + gameObject.name + 0 + ".prefab");
            print("wth???");
        }
        else while (File.Exists(Application.dataPath + "/Resources/Prefabs/" + gameObject.name + i + ".prefab"))
        {
                i++;
        }
        PrefabUtility.SaveAsPrefabAsset(gameObject, "Assets/Resources/Prefabs/" + gameObject.name + i + ".prefab");
    }
#endif
}
