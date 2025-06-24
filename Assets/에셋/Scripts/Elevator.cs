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
    public List<float> DelayList2;
    public List<UnityEvent> EventList;
    public List<UnityEvent> EventList0;
    bool CanGoUP = false;
    bool WillGoUP = false;
    public float UpValue = 0.01f;
    MeshRenderer Renderer;
    public Material LightMat;
    public Material DarkMat;
    public GameObject MagicObject;
    GameObject XrRig;
    public GameObject Gas;
    bool Teleport = false;

    public List<GameObject> FloorObjectList;

    public void Save()
    {
        PlayerPrefs.SetInt("Floor", i);
    }

    public void Clear()
    {
        Count++;
        print("add");
        if (Count >= CountList[i])
        {
            AllClear();
        }
    }
    public void AllClear()
    {
        StartCoroutine(nameof(OnClear));
    }
    IEnumerator OnClear()
    {
        yield return new WaitForSeconds(DelayList[i]);
        EventList[i].Invoke();
        yield return new WaitForSeconds(DelayList2[i]);
        EventList0[i].Invoke();
        CanGoUP = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        LoadFloor(i + 1);
        Count = 0;

        print("clear!");
    }
    public void SetFloor(int Floor)
    {
        if (Floor == 2)
        {
            FloorObjectList[1].SetActive(false);
        }
        Teleport = true;
        LoadFloor(Floor);
        Count = 0;
        CanGoUP = false;
    }
    void LoadFloor(int Floor)
    {
        if(Floor >= 1 && i == 0)
        {
            Gas.SetActive(true);
        }

        if (i != Floor && i - Floor != -1)
        {
            FloorObjectList[i].SetActive(false);
        }
        i = Floor;
        if (i >= 2)
        {
            FloorObjectList[i - 2].SetActive(false);
        }
        FloorObjectList[i].SetActive(true);
    }
    private void Start()
    {
        Renderer = MagicObject.GetComponent<MeshRenderer>();
        XrRig = GameObject.Find("XR Origin (XR Rig)");
        if(!PlayerPrefs.HasKey("Floor"))
        {
            PlayerPrefs.SetInt("Floor", 0);
        }
        if (PlayerPrefs.GetInt("Floor") != 0)
        {
            SetFloor(PlayerPrefs.GetInt("Floor"));
        }
        PlayerPrefs.SetInt("Floor", 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "XR Origin (XR Rig)") WillGoUP = true;
    }

    void Update()
    {
        if (Teleport)
        {
            Finish();
            Teleport = false;
            return;
        }
        if (CanGoUP)
        {
            Renderer.material = LightMat;
        }
        else
        {
            Renderer.material = DarkMat;
        }
        if (CanGoUP && WillGoUP)
        {
            if (gameObject.transform.position.y >= FloorList[i - 1])
            {
                Finish();
            }
            else
            {
                foreach (var item in ElevatorWalls)
                {
                    item.SetActive(true);
                }
                XrRig.transform.position = transform.position + new Vector3(XrRig.transform.position.x, 0.6f, XrRig.transform.position.z);
                transform.Translate(0, UpValue * Time.deltaTime, 0);
            }
        }

        void Finish()
        {
            foreach (var item in ElevatorWalls)
            {
                item.SetActive(false);
            }
            XrRig.GetComponent<Character>().LastFloor = transform.position;
            XrRig.transform.SetPositionAndRotation(new Vector3(0, FloorList[i - 1], 0), XrRig.transform.rotation);
            transform.SetPositionAndRotation(new Vector3(transform.position.x, FloorList[i - 1], transform.position.z), transform.rotation);
            CanGoUP = false;
            WillGoUP = false;
            FindAnyObjectByType<Gas>().Reset();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
