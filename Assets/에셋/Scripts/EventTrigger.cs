using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent InitEvent;
    void Start()
    {
        InitEvent.Invoke();
    }

}
