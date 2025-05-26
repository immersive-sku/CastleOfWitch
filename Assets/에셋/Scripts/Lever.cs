using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Lever : MonoBehaviour
{
    public GameObject ClearEffect;
    public UnityEvent Event;
    List<bool> GearList;
    private void Start()
    {
        GearList = new List<bool>();
        GearList.Add(false);
        GearList.Add(false);
        GearList.Add(false);
    }
    public void Add(int i)
    {
        GearList[i] = true;
        foreach (var item in GearList)
        {
            if (!item) return;
        }
        gameObject.transform.SetPositionAndRotation(transform.position + new Vector3(0, 0.3f, 0), transform.rotation);
        Event.Invoke();
    }
    public void Sub(int i)
    {
        GearList[i] = false;

    }

    public void Ending()
    {
        StartCoroutine("End");
    }
    IEnumerator End()
    {
        while (true)
        {
            Instantiate(ClearEffect);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("End");
            print("enddd");
        }
    }
}
