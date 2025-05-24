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
    public Vector3 StartAngle;
    public Vector3 EndAngle;
    private void Start()
    {
        Elevator = GameObject.Find("Elevator");
    }
    void Update()
    {
        t += Time.deltaTime;

        SetLoc(t / MovingTime);
        foreach (var i in Gauge)
        {
            i.transform.rotation = Quaternion.Euler(Vector3.Lerp(StartAngle, EndAngle ,t/MovingTime));
        }
    }
    public void SetLoc(float t)
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(Elevator.transform.position.y - 3, Elevator.transform.position.y, t), transform.position.z);
    }
    public void Reset()
    {
        t = 0;
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
