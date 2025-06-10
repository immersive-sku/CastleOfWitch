using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class LightMover : MonoBehaviour
{
    public List<Transform> LightTransforms;
    public List<float> MoveTimes;
    public int i;
    float t = 0;
    Transform LastTransform;

    private void Start()
    {
        LastTransform = transform;
    }

    public void SetPosition(int i)
    {
        t = 0;
        this.i = i;
        LastTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {   
        if(t / MoveTimes[i] <= 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(LastTransform.position, LightTransforms[i].position, t / MoveTimes[i]);
        }
    }
}
