using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class Character : MonoBehaviour
{
    public GameObject HPUI;
    public Slider Slider;
    public TMP_Text text;
    public UnityEvent OnDamaged;
    public UnityEvent OnDead;
    public UnityEvent OnRevived;
    public float ReviveDelay;
    const float MaxHP = 100.0f;
    public float HP = MaxHP;
    public Vector3 LastFloor = new Vector3(0, 1.7f, 0);
    public Quaternion LastRotation = new Quaternion();
    public Transform RealBase;
    public void SetProperPosition()
    {
        Vector3 r = RealBase.rotation.eulerAngles + LastRotation.eulerAngles;
        TeleportRequest TR = new TeleportRequest();
        TR.destinationPosition = RealBase.position + LastFloor;
        TR.destinationRotation = Quaternion.Euler(r.x, r.y, r.z);
        TR.requestTime = 0;
        TR.matchOrientation = MatchOrientation.TargetUpAndForward;
 
        gameObject.GetComponent<TeleportationProvider>().QueueTeleportRequest(TR);
    }
    public void Damage(float damage)
    {
        if (HP <= 0)
        {
            return;
        }
        HP -= damage;
        HPUI.SetActive(true);
        OnDamaged.Invoke();
        if(HP <= 0)
        {
            StartCoroutine(nameof(DamageCorutine));
        }
    }
    IEnumerator DamageCorutine()
    {
        OnDead.Invoke();
        print("a");
        yield return new WaitForSeconds(ReviveDelay);
        print("a");

        //SetProperPosition();
        HP = MaxHP;
        FindAnyObjectByType<Gas>().Reset();
        OnRevived.Invoke();
    }
}
