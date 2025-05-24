using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Transform Original;
    public Transform AttackTransform;
    public void Attac()
    {
        Original = transform;
        StartCoroutine(nameof(AttacCorutine));
    }
    IEnumerator AttacCorutine()
    {
        float PassedFrame = 0;
        while (PassedFrame <= 2)
        {
            transform.SetPositionAndRotation(Vector3.Lerp(Original.position, AttackTransform.position, PassedFrame), transform.rotation);
            yield return new WaitForEndOfFrame();
            PassedFrame += Time.deltaTime;
        }
        transform.SetPositionAndRotation(Original.position, Original.rotation);
    }
}
