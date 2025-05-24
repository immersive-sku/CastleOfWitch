using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent InitEvent;

    private void OnEnable()
    {
        InitEvent.Invoke();
        print("Invoke");
    }

}
