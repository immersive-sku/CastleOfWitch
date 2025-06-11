using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LightMover : MonoBehaviour
{
    public List<Transform> LightTransforms;
    public List<float> MoveTimes;
    public int i;
    float t = 0;
    Vector3 LastTransform = new Vector3();

    private void Start()
    {
        LastTransform = transform.position;
    }

    public void SetPosition(int i)
    {
        t = 0;
        this.i = i;
        LastTransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if(t / MoveTimes[i] <= 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(LastTransform, LightTransforms[i].position, t / MoveTimes[i]);
        }
    }
}
