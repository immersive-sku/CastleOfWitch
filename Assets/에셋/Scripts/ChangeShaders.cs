using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class ChangeShaders : MonoBehaviour
{
    public List<GameObject> gs;
    public Material NewMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetChildGameObjects(gs);
        List<Material> m = new List<Material>();
        m.Add(NewMaterial);
        foreach (GameObject g in gs)
        {

            g.GetComponent<Renderer>().SetMaterials(m);
        }
    }
}
