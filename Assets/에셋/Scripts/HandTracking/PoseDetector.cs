using UnityEngine;
using UnityEngine.Events;

public class PoseDetector : MonoBehaviour
{
    private bool bIsDetecting = false;
    public UnityEvent OnPoseDetected;
    private MeshRenderer Renderer;
    public GameObject textObject;
    public Transform CameraLoc;
    public Tracker Tracker;
    public float tL = 0, tR = 0;
    float MaxL = 0, MaxR = 0;
    public float TransitionTime = 1;
    void Start()
    {
        Renderer = textObject.GetComponent<MeshRenderer>();
    }
    public void StartTrack()
    {
        textObject.SetActive(true);
        bIsDetecting = true;
        textObject.transform.position = CameraLoc.position;

    }
    public void StopTrack()
    {
        Tracker.ResetTracker();
        textObject.SetActive(false);
        bIsDetecting = false;
        tL = 0;
        tR = 0;
    }
    void Update()
    {
        if (bIsDetecting)
        {
            MaxL = Tracker.GetL() * TransitionTime;
            MaxR = Tracker.GetR() * TransitionTime;
            textObject.transform.rotation = CameraLoc.rotation;
            tL = tL <= MaxL ? tL + Time.deltaTime : MaxL;
            tR = tR <= MaxR ? tR + Time.deltaTime : MaxR;
            Renderer.material.SetFloat("_ProgressR", tR / TransitionTime);
            Renderer.material.SetFloat("_ProgressL", tL / TransitionTime);
            if ((tL / TransitionTime >= 0.9) && (tR / TransitionTime >= 0.9))
            {
                StopTrack();
                OnPoseDetected.Invoke();
            }
        }
    }
}
