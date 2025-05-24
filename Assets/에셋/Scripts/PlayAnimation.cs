using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
 public void Play()
    {
        gameObject.GetComponent<Animation>().Play();
    }
}
