using System.Collections.Generic;
using UnityEngine;


public class MeshFader : MonoBehaviour
{
    public float FadeTime = 1;
    float CurrentFadeTime;
    public MeshRenderer FadeMeshRenderer;

    private void OnEnable()
    {
    CurrentFadeTime = FadeTime;
    }
    private void Update()
    {
        List<Material> FadeMaterial = new List<Material>();
        FadeMeshRenderer.GetMaterials(FadeMaterial);
        foreach (var item in FadeMaterial)
        {
          item.SetFloat("_Alpha", CurrentFadeTime / FadeTime * 0.5f);
        } 
        CurrentFadeTime -= Time.deltaTime;
        if (CurrentFadeTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
