using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    public List<GameObject> ElevatorWalls;
    public int Count = 0;
    public int i = 0;
    public List<float> CountList;
    public List<float> FloorList;
    public List<float> DelayList;
    public List<UnityEvent> EventList;
    static bool CanGoUP = false;
    static bool WillGoUP = false;
    public float UpValue = 0.01f;
    MeshRenderer Renderer;
    public Material LightMat;
    public Material DarkMat;

    public void Clear()
    {
        Count++;
        print("add");
        if (Count >= CountList[i])
        {
            StartCoroutine(nameof(OnClear));
        }
    }
    IEnumerator OnClear()
    {
        yield return new WaitForSeconds(FloorList[i]);
        EventList[i].Invoke();
        Count = 0;
        CanGoUP = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        i++;
        print("clear!");
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
            if (GameObject.Find("XR Origin (XR Rig)").transform.position.y >= FloorList[i - 1])
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, FloorList[i - 1], transform.position.z), transform.rotation);
                CanGoUP = false;
                FindAnyObjectByType<Gas>().Reset();
                foreach (var item in ElevatorWalls)
                {
                    item.SetActive(false);
                }
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                foreach (var item in ElevatorWalls)
                {
                    item.SetActive(true);
                }
                FindAnyObjectByType<Character>().LastFloor = gameObject.transform.position + new Vector3(0, 0.1f, 0);
                GameObject.Find("XR Origin (XR Rig)").transform.Translate(0, UpValue * Time.deltaTime, 0);
                transform.Translate(0, UpValue * Time.deltaTime, 0);
                print("going up");
            }
        }

    }
}
