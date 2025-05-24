using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoseDetector : MonoBehaviour
{
    private Tracker TrackerInstance;
    public double TrackLimit = 2.0f;
    private bool bIsDetecting = false;
    private int idkL = 0;
    private int idkR = 0;
    public UnityEvent OnPoseDetected;
    public string folderName = "Prefabs";  // The folder inside "Resources"
    private List<SaveTrack> prefabs = new List<SaveTrack>();
    private UnityEngine.XR.InputDevice device;
    private MeshRenderer Renderer;
    public GameObject textObject;
    void Start()
    {
        Renderer = textObject.GetComponent<MeshRenderer>();
        TrackerInstance = Object.FindFirstObjectByType<Tracker>();
        LoadPrefabs();
        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        if (leftHandDevices.Count == 1)
        {
            device = leftHandDevices[0];
            Debug.Log(string.Format("Device name '{0}' with role '{1}'", device.name, device.characteristics.ToString()));
        }
        else if (leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }
    }
    void LoadPrefabs()
    {
        GameObject[] prefab = Resources.LoadAll<GameObject>(folderName);
        foreach (GameObject go in prefab)
        {
            SaveTrack st = go.GetComponent<SaveTrack>();
            if (st != null)
            {
                prefabs.Add(st);
            }
        }
    }
    public void StartTrack()
    {
        textObject.SetActive(true);
        bIsDetecting = true;
    }
    public void StopTrack()
    {
        textObject.SetActive(false);
        bIsDetecting = false;
    }

    void Update()
    {
        if (bIsDetecting)
        {                
            int framesL = 0;
            int framesR = 0;
            double LowestAverage = TrackLimit;
            for (int i = 0; i < prefabs.Count; i++)
            {
                double AverageL = 0;
                double AverageR = 0;

                for (int j = 0; j < 60; j++)
                {
                    if (j + idkL >= prefabs[i].RTracker.Count)
                    {
                        break;
                    }
                    AverageL += Vector3.Distance(TrackerInstance.LTracker, prefabs[i].LTracker[idkL + j]);
                    AverageL += Vector3.Distance(TrackerInstance.LTracker, prefabs[i].LTracker[idkL + j]);
                    AverageL /= 2;
                    if (LowestAverage > AverageL)
                    {
                        Debug.Log(AverageL);
                        Renderer.material.SetFloat("ProgressL", idkL / prefabs.Count);
                        LowestAverage = AverageL;
                        framesL = j + 1;
                    }
                }

                for (int j = 0; j < 60; j++)
                {
                    if (j + idkR >= prefabs[i].RTracker.Count)
                    {
                        break;
                    }
                    AverageR += Vector3.Distance(TrackerInstance.RTracker, prefabs[i].RTracker[idkR + j]);
                    AverageR += Vector3.Distance(TrackerInstance.RTracker, prefabs[i].RTracker[idkR + j]);
                    AverageR /= 2;
                    if (LowestAverage > AverageR)
                    {
                        Debug.Log(AverageR);
                        Renderer.material.SetFloat("ProgressR", idkR / prefabs.Count);
                        LowestAverage = AverageR;
                        framesR = j + 1;
                    }
                }
            }
            idkL += framesL;
            idkR += framesR;
            Debug.Log(idkL);
            Debug.Log(idkR);
            if (LowestAverage >= TrackLimit)
            {
                idkL = 0;
                idkR = 0;
                Debug.LogError("Pose not detected");
                return;
            }
            if(idkR >= prefabs[0].RTracker.Count- 30 && idkL >= prefabs[0].LTracker.Count - 30)
            {
                OnPoseDetected.Invoke();
                Debug.Log("Pose detected");
                idkL = 0;
                idkR = 0;
                StopTrack();
                return;
            }
        }
    }
}
