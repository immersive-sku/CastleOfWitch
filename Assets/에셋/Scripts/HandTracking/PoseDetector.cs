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
    private Vector3 objLoc;
    void Start()
    {
        Renderer = textObject.GetComponent<MeshRenderer>();
        TrackerInstance = Object.FindFirstObjectByType<Tracker>();
        LoadPrefabs();
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
        objLoc = textObject.transform.position;
    }
    public void StopTrack()
    {
        textObject.SetActive(false);
        bIsDetecting = false;
        idkL = 0;
        idkR = 0;
    }

    void Update()
    {
        if (bIsDetecting)
        {
            textObject.transform.position = objLoc;
            int framesL = 0;
            int framesR = 0;
            double LowestAverageL = TrackLimit;
            double LowestAverageR = TrackLimit;
            for (int i = 0; i < prefabs.Count; i++)
            {
                
                for (int j = 0; j < 300; j++)
                {
                    if (j + idkL >= prefabs[i].LTracker.Count)
                    {
                        break;
                    }
                    
                    double AverageL =
                        Mathf.Abs(Vector3.Distance(TrackerInstance.LTracker ,objLoc) - Vector3.Distance(prefabs[i].LTracker[idkL + j], objLoc));
                    if (LowestAverageL > AverageL)
                    {
                        print(prefabs[i].LTracker[idkL + j]);
                        LowestAverageL = AverageL;
                        framesL = j;
                        Renderer.material.SetFloat("_ProgressR", (float)(idkL + framesL) / prefabs[i].LTracker.Count); //????????????????????????????????????????????????????
                    }
                }
                
                for (int K = 0; K < 300; K++)
                {
                    if (K + idkR >= prefabs[i].RTracker.Count)
                    {
                        break;
                    }
                    double AverageR = Vector3.Distance(TrackerInstance.RTracker - objLoc, prefabs[i].RTracker[idkR + K] - objLoc);
                    if (LowestAverageR > AverageR)
                    {
                        LowestAverageR = AverageR;
                        framesR = K;
                        Renderer.material.SetFloat("_ProgressL", (float)(idkR + framesR) / prefabs[i].RTracker.Count);
                    }
                }
                
            }
            idkL += framesL;
            idkR += framesR;

            if (LowestAverageL >= TrackLimit)
            {
                idkL = 0;
                Renderer.material.SetFloat("_ProgressR", 0);
            }
            if (LowestAverageR >= TrackLimit)
            {
                idkR = 0;
                Renderer.material.SetFloat("_ProgressL", 0);
            }
            else if (idkR >= prefabs[0].RTracker.Count - 60 && idkL >= prefabs[0].LTracker.Count - 60)
            {
                OnPoseDetected.Invoke();
                Debug.Log("Pose detected");
                StopTrack();
            }
        }
    }
}
