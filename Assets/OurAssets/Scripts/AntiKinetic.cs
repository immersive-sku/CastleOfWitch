using UnityEngine;

public class AntiKinetic : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
