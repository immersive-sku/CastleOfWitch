using UnityEngine;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using UnityEditor;

public class Tracker : MonoBehaviour
{
    public Vector3 LTracker;
    private GameObject LTrackerObject;

    public Vector3 RTracker;
    private GameObject RTrackerObject;


    private void Start()
    {
        InvokeRepeating("UpdateLimited", 0, 0.016f);
    }

    private void UpdateLimited()
    {
        LTrackerObject = GameObject.Find("/XR Origin (XR Rig)/Camera Offset/Left Controller");
        RTrackerObject = GameObject.Find("/XR Origin (XR Rig)/Camera Offset/Right Controller");
        LTracker = LTrackerObject.transform.localPosition;
        RTracker= RTrackerObject.transform.localPosition;
    }
}
