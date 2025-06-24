using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Subtitle : MonoBehaviour
{
    public List<String> Subs;
    public List<float> Lens;
    public List<int> Idk;
    public List<Transform> Locs;
    public List<TMP_FontAsset> Fonts;
    public int Num;
    public TMPro.TMP_Text UITextMeshPro;
    public void Reset()
    {
        Num = 0;
    }
    public void TurnOn(int Num1)
    {
        Num = Num1;
        int Level = GameObject.Find("Elevator").GetComponent<Elevator>().i;
        transform.position = Locs[Idk[Level] + Num].position;
        transform.rotation = Locs[Idk[Level] + Num].rotation;
        UITextMeshPro.font = Fonts[Idk[Level] + Num];
        UITextMeshPro.text = Subs[Idk[Level] + Num];
        StartCoroutine(nameof(TurnOff));
    }
    IEnumerator TurnOff()
    {
        int Level = GameObject.Find("Elevator").GetComponent<Elevator>().i;
        yield return new WaitForSeconds(Lens[Level + Num]);
        UITextMeshPro.text = "";
    }
}
