using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    public GameObject RightType;
    public GameObject WrongType;
    public bool UseWrongType = false;
    public UnityEvent EventOnRightType;
    public UnityEvent EventOnWrongType;
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(UseWrongType && other.gameObject.tag == WrongType.tag)
        {
            EventOnWrongType.Invoke();
        }
        if (other.gameObject.tag == RightType.tag)
        {
            EventOnRightType.Invoke();
        }
    }
}
