using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    GameObject Elevator;
    public List<GameObject> Gauge;
    float t = 0;
    float PlayerTriggerSeconds = 0;
    public float PlayerDamageTickInSeconds = 0;
    public float MovingTime;
    public float GasDamagePerTick;
    public List<Vector3> StartAngle;
    public List<Vector3> EndAngle;
    public float LastElevatorY;
    private void Start()
    {
        Elevator = GameObject.Find("Elevator");
        LastElevatorY = Elevator.transform.position.y;
    }
    void Update()
    {
        t += Time.deltaTime;
        int Level = GameObject.Find("Elevator").GetComponent<Elevator>().i;
        SetLoc(t / MovingTime);
        foreach (var i in Gauge)
        {
            i.transform.rotation = Quaternion.Euler(Vector3.Lerp(StartAngle[Level], EndAngle[Level] ,t/MovingTime));
        }
    }
    void SetLoc(float t)
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(LastElevatorY - 3, LastElevatorY, t), transform.position.z);
    }
    public void SetTime(float NewT)
    {
        this.t = NewT;
    }
    public void Reset()
    {
        t = 0;
        LastElevatorY = Elevator.transform.position.y;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTriggerSeconds += Time.deltaTime;
            if(PlayerTriggerSeconds > PlayerDamageTickInSeconds)
            {
                other.gameObject.GetComponent<Character>().Damage(GasDamagePerTick);
                PlayerTriggerSeconds = 0;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerTriggerSeconds = 0;
    }
}
