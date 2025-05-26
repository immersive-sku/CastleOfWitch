using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoseDetector : MonoBehaviour
{
    public float Boost = 0.01f;
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

    void FixedUpdate()
    {
        if (bIsDetecting)
        {                
            int framesL = 0;
            int framesR = 0;
            double LowestAverageL = TrackLimit;
            double LowestAverageR = TrackLimit;
            for (int i = 0; i < prefabs.Count; i++)
            {
                


                for (int j = 0; j < 3; j++)
                {
                    if (j + idkL >= prefabs[i].LTracker.Count)
                    {
                        break;
                    }
                    double AverageL = 0;
                    AverageL += Vector3.Distance(TrackerInstance.LTracker, prefabs[i].LTracker[idkL + j]);
                    AverageL /= 1;
                    if (LowestAverageL > (AverageL - j * Boost))
                    {
                        LowestAverageL = AverageL;
                        framesL = j;
                        Renderer.material.SetFloat("_ProgressL", (float)(idkL + framesL) / prefabs[i].LTracker.Count);
                    }
                }

                for (int j = 0; j < 3; j++)
                {
                    if (j + idkR >= prefabs[i].RTracker.Count)
                    {
                        break;
                    }
                    double AverageR = 0;
                    AverageR += Vector3.Distance(TrackerInstance.RTracker, prefabs[i].RTracker[idkR + j]);
                    AverageR /= 1;
                    if (LowestAverageR > (AverageR - j * Boost))
                    {
                        print(j);
                        LowestAverageR = AverageR;
                        framesR = j;
                        Renderer.material.SetFloat("_ProgressR", (float)(idkR + framesR) / prefabs[i].RTracker.Count);
                    }
                }
            }
            idkL += framesL;
            idkR += framesR;


            if (LowestAverageL >= TrackLimit)
            {
                idkL = 0;
                Debug.LogError("Pose not detected");
                Renderer.material.SetFloat("_ProgressL", 0);
            }
            if (LowestAverageR >= TrackLimit)
            {
                idkR = 0;
                Debug.LogError("Pose not detected");
                Renderer.material.SetFloat("_ProgressR", 0);
            }
            else if (idkR >= prefabs[0].RTracker.Count - 60 && idkL >= prefabs[0].LTracker.Count - 60)
            {
                OnPoseDetected.Invoke();
                Debug.Log("Pose detected");
                idkL = 0;
                idkR = 0;
                StopTrack();
            }
        }
    }
}
