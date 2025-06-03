using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public float WaitTime = 0.0f;
    public UnityEvent InitEvent;

    private void OnEnable()
    {
        print("Invoke");
        StartCoroutine(nameof(TriggerEvent));
    }

    IEnumerator TriggerEvent()
    {
        yield return new WaitForSeconds(WaitTime);
        InitEvent.Invoke();
    }

}
