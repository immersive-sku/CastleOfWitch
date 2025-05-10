using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject KillType;
    public GameObject Particle;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == KillType)
        {
            Instantiate(Particle, transform.position, transform.rotation);
            Component.FindAnyObjectByType<Elevator>().Clear();
            Destroy(gameObject);
        }

    }
}
