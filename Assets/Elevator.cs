using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int Count = 0;
    public int i = 0;
    public List<float> CountList;
    public List<float> FloorList;
    static bool CanGoUP = true;
    static bool WillGoUP = false;
    public float UpValue = 0.01f;
    public  void Clear()
    {
        Count++;
        print("add");
        if (Count >= CountList[i])
        {
            i++;
            Count = 0;
            CanGoUP = true;
            print("clear!");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "XR Origin (XR Rig)")WillGoUP = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "XR Origin (XR Rig)") WillGoUP = false;
    }

    void Update()
    {
        if(CanGoUP && WillGoUP)
        {
            if (transform.position.y >= FloorList[i-1])
            {
                CanGoUP = false;
            }
            else
            {
                GameObject.Find("XR Origin (XR Rig)").transform.Translate(0, UpValue * Time.deltaTime, 0);
                transform.Translate(0, UpValue * Time.deltaTime, 0);
                print("going up");
            }
        }

    }
}
