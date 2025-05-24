using System.Xml.Serialization;
using UnityEngine;

public class pose : MonoBehaviour
{
    public void teleport()
    { 
        transform.position = gameObject.transform.position + new Vector3(0, 1, 0);
    }
}
