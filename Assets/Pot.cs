using UnityEngine;

public class Pot : MonoBehaviour
{
    Vector3 StartLocation;
    float t = 0;
    public float FillTime = 1;
    public Transform FinishTransform;
    public GameObject EffectObject;
    private void OnEnable()
    {
        StartLocation = transform.position;
        EffectObject.SetActive(true);
    }
    void Update()
    {
        t += Time.deltaTime;
        transform.position = Vector3.Lerp(StartLocation, FinishTransform.position, t / FillTime);
        if (t <= FillTime)
        {
        } else
        {
            t = 0;
            this.enabled = false;
        }
    }
}
