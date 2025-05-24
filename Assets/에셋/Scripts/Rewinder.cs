using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class Rewinder : MonoBehaviour
{
    public List<Vector3> InitPosition;
    public List<Quaternion> InitRotation;

    public List<Vector3> EndPosition;
    public List<Quaternion> EndRotation;

    public float ResetCastingSeconds = 1;
    public List<GameObject> gs;

    public UnityEvent ResetEndEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetChildGameObjects(gs);
        foreach (GameObject g in gs)
        {
            InitPosition.Add(g.transform.position);
            InitRotation.Add(g.transform.rotation);
        }
    }

    public void StartReset()
    {

        foreach (GameObject g in gs)
        {
            foreach (Rigidbody r in g.GetComponents<Rigidbody>())
            {
                r.isKinematic = true;
            }
            EndPosition.Add(g.transform.position);
            EndRotation.Add(g.transform.rotation);
        }
        StartCoroutine(nameof(Reset));
    }

    IEnumerator Reset()
    {
        float t = 0;
        while (t <= ResetCastingSeconds)
        {
            for (int i = 0; i < gs.Count; i++)
            {
                gs[i].transform.SetPositionAndRotation(Vector3.Lerp(EndPosition[i], InitPosition[i], t / ResetCastingSeconds),
                    Quaternion.Lerp(EndRotation[i], InitRotation[i], t / ResetCastingSeconds));
            }
            t += Time.deltaTime;
            print(t);
            yield return new WaitForEndOfFrame();
        }
        ResetEndEvent.Invoke();
    }

}
