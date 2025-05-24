using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Vector3 OriginalPosition;
    Vector3 OriginalScale;
    public Transform AttackTransform;
    public void Attac()
    {
        OriginalPosition = transform.position;
        OriginalScale = transform.localScale;
        StartCoroutine(nameof(AttacCorutine));
        print("wtf");
    }
    IEnumerator AttacCorutine()
    {
        float PassedFrame = 0;
        while (PassedFrame <= 2)
        {
            transform.SetPositionAndRotation(Vector3.Lerp(OriginalPosition, AttackTransform.position, PassedFrame), transform.rotation);
            transform.localScale = Vector3.Lerp(OriginalScale, AttackTransform.localScale, PassedFrame);
            PassedFrame += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.SetPositionAndRotation(OriginalPosition, transform.rotation);
        transform.localScale = OriginalScale;
    }
}
