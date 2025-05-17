using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gas : MonoBehaviour
{
    GameObject Elevator;
    public List<Slider> ProgressBars;
    public List<TMP_Text> ProgressTexts;
    float t = 0;
    float PlayerTriggerSeconds = 0;
    public float PlayerDamageTickInSeconds = 0;
    public float MovingTime;
    public float GasDamagePerTick;
    private void Start()
    {
        Elevator = GameObject.Find("Elevator");
    }
    void Update()
    {
        t += Time.deltaTime;

        SetLoc(t / MovingTime);
        foreach (var bar in ProgressBars)
        {
            bar.value = t / MovingTime;
        }
        foreach (var text in ProgressTexts)
        {
            text.text = (t / MovingTime).ToString("F2");
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
