using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PoseDetector : MonoBehaviour
{
    private Tracker TrackerInstance;
    public double TrackLimit = 2.0f;
    private bool bIsDetecting = false;
    private int idk = 0;
    public UnityEvent OnPoseDetected;
    public string folderName = "Prefabs";  // The folder inside "Resources"
    private List<SaveTrack> prefabs = new List<SaveTrack>();
    private UnityEngine.XR.InputDevice device;
    private TextMeshProUGUI text;
    public GameObject textObject;
    void Start()
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
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
        bIsDetecting = true;
    }

    void Update()
    {
        if (bIsDetecting)
        {                
            int frames = 0;
            double LowestAverage = TrackLimit;
            for (int i = 0; i < prefabs.Count; i++)
            {
                double Average = 0;

                for (int j = 0; j < 60; j++)
                {
                    int b;
                    if (j + idk >= prefabs[i].RTracker.Count)
                    {
                        break;
                    }
                    Average += Vector3.Distance(TrackerInstance.RTracker, prefabs[i].RTracker[idk + j]);
                    Average += Vector3.Distance(TrackerInstance.RTracker, prefabs[i].RTracker[idk + j]);
                    Average /= 2;
                    if (LowestAverage > Average)
                    {
                        Debug.Log(Average);
                        text.text = Average.ToString() + "/" + idk.ToString();
                        LowestAverage = Average;
                        frames = j + 1;
                    }
                }
            }
            idk += frames;
            Debug.Log(idk);
            if (LowestAverage >= TrackLimit)
            {
                bIsDetecting = false;
                idk = 0;
                Debug.LogError("Pose not detected");
                return;
            }
            if(idk >= prefabs[0].RTracker.Count- 30)
            {
                bIsDetecting = false;
                OnPoseDetected.Invoke();
                Debug.Log("Pose detected");
                idk = 0;
                return;
            }
        }
    }
}
