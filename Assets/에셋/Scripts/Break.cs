using UnityEngine;
using UnityEngine.Events;

public class Break : MonoBehaviour
{
    public GameObject KillType;
    public GameObject WrongType;
    public bool UseWrongType = false;
    public UnityEvent EventOnRightType;
    public UnityEvent EventOnWrongType;
    public GameObject Particle;
    private void OnCollisionEnter(Collision collision)
    {
        if(UseWrongType && collision.gameObject.tag == WrongType.tag)
        {
            EventOnWrongType.Invoke();
        }
        if (collision.gameObject.tag == KillType.tag)
        {
            EventOnRightType.Invoke();
            Instantiate(Particle, transform.position, transform.rotation);
            Component.FindAnyObjectByType<Elevator>().Clear();
            Destroy(gameObject);
        }

    }
}
