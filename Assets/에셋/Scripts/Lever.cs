using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lever : MonoBehaviour
{
    public GameObject ClearEffect;
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
        Ending();
    }
    public void Sub(int i)
    {
        GearList[i] = false;

    }

    public void Ending()
    {
        StartCoroutine("End");
    }
    IEnumerable End()
    {
        Instantiate(ClearEffect);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("End");
    }
}
