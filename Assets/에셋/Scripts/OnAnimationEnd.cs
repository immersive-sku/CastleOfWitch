using UnityEngine;
using UnityEngine.Events;

public class OnAnimationEnd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animation ani;
    public UnityEvent EndEvent;
    bool HasAniPlayed = false;
    void Start()
    {
        ani = gameObject.GetComponent<Animation>();
    }
    private void Update()
    {
        if (ani.isPlaying)
        {
            HasAniPlayed = true;
        }
        if(ani.isActiveAndEnabled && !ani.isPlaying && HasAniPlayed)
        {
            EndEvent.Invoke();
            HasAniPlayed = false;
        }
    }
}
