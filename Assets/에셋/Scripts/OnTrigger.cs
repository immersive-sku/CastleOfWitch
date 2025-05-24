using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    public GameObject RightType;
    public GameObject WrongType;
    public bool UseWrongType = false;
    public UnityEvent EventOnRightType;
    public UnityEvent EventOnWrongType;
    public UnityEvent EventOnRightTypeExit;
    public UnityEvent EventOnWrongTypeExit;
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        print(other.gameObject.tag);
        if (UseWrongType && other.gameObject.tag == WrongType.tag)
        {
            EventOnWrongType.Invoke();
        }
        if (other.gameObject.tag == RightType.tag)
        {
            EventOnRightType.Invoke();
            print("EnterInvoke");
        }
    }
    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (UseWrongType && other.gameObject.tag == WrongType.tag)
        {
            EventOnWrongTypeExit.Invoke();
        }
        if (other.gameObject.tag == RightType.tag)
        {
            EventOnRightTypeExit.Invoke();
            print("ExitInvoke");
        }
    }
}
