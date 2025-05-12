using System.Collections.Generic;
using NUnit.Framework;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
    public List<Transform> InitTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<GameObject> gs = null;
        gameObject.GetChildGameObjects(gs);
        foreach (GameObject g in gs)
        {
            InitTransform.Add(g.transform);
        }
    }

    public void Reset()
    {
        foreach(Rigidbody r in gameObject.GetComponents<Rigidbody>())
        {
            r.isKinematic = true;
        }
        List<GameObject> gs = null;
        gameObject.GetChildGameObjects(gs);
        for(int i=0; i<= gs.Count; i++)
        {
            gs[i].transform.SetLocalPositionAndRotation(InitTransform[i].position, InitTransform[i].rotation);
        }
    }
}
