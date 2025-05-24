using UnityEngine;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using UnityEditor;

public class SaveTrack : MonoBehaviour
{

    public List<Vector3> LTracker = new List<Vector3>();
    public List<Vector3> RTracker = new List<Vector3>();
    private Tracker TrackerInstance;
#if UNITY_EDITOR
    private void Start()
    {
        InvokeRepeating("UpdateLimited", 0, 0.016f);
        TrackerInstance = Object.FindFirstObjectByType<Tracker>();
    }

    private void UpdateLimited()
    {
        LTracker.Add(TrackerInstance.LTracker);
        RTracker.Add(TrackerInstance.RTracker);
    }

    private void OnDestroy()
    {
        PrefabUtility.SaveAsPrefabAsset(gameObject, "Assets/Resources/Prefabs/" + gameObject.name + ".prefab");
    }
#endif
}
