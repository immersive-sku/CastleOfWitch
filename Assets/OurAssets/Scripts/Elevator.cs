using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    public int Count = 0;
    public int i = 0;
    public List<float> CountList;
    public List<float> FloorList;
    public List<UnityEvent> EventList;
    static bool CanGoUP = false;
    static bool WillGoUP = false;
    public float UpValue = 0.01f;
    MeshRenderer Renderer;
    public Material LightMat;
    public Material DarkMat;

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
    private void Start()
    {
        Renderer = gameObject.GetComponent<MeshRenderer>();
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
        if(CanGoUP)
        {
            Renderer.material = LightMat;
        }
        else
        {
            Renderer.material = DarkMat;
        }
        if (CanGoUP && WillGoUP)
        {
            if (transform.position.y >= FloorList[i - 1])
            {
                CanGoUP = false;
                FindAnyObjectByType<Gas>().Reset();
            }
            else
            {
                FindAnyObjectByType<Character>().LastFloor = gameObject.transform.position + new Vector3(0, 0.1f, 0);
                GameObject.Find("XR Origin (XR Rig)").transform.Translate(0, UpValue * Time.deltaTime, 0);
                transform.Translate(0, UpValue * Time.deltaTime, 0);
                print("going up");
            }
        }

    }
}
