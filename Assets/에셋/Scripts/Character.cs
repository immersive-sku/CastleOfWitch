using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    float HP = MaxHP;
    public Vector3 LastFloor = new Vector3(0, 1.7f, 0);
    public void Damage(float damage)
    {
        HP -= damage;
        print(HP);

        HPUI.SetActive(true);
        Slider.value = HP / MaxHP;
        text.text = HP.ToString() + '%';
        OnDamaged.Invoke();
        if(HP < 0)
        {
            StartCoroutine(nameof(DamageCorutine));
        }
    }
    IEnumerable DamageCorutine()
    {
        OnDead.Invoke();
        yield return new WaitForSeconds(ReviveDelay);
        gameObject.transform.position = LastFloor;
        HP = MaxHP;
        FindAnyObjectByType<Gas>().Reset();
        OnRevived.Invoke();
    }
}
