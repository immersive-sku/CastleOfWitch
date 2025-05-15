using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public float FadeTime = 1;
    float CurrentFadeTime;
    public CanvasGroup CanvasG;
    private void OnEnable()
    {
    CurrentFadeTime = FadeTime;
    }
    private void Update()
    {
        CanvasG.alpha = CurrentFadeTime / FadeTime;
        CurrentFadeTime -= Time.deltaTime;
        if (CurrentFadeTime <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
