using UnityEngine;
using UnityEngine.Events;

public class OnAnimationEnd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animation ani;
    public UnityEvent EndEvent;
    void Start()
    {
        ani = gameObject.GetComponent<Animation>();
    }
    private void Update()
    {
        if(ani.isActiveAndEnabled && !ani.isPlaying)
        {
            EndEvent.Invoke();
        }
    }
}
