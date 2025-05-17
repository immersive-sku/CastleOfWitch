using UnityEngine;

public class BrokenPipe : MonoBehaviour
{
    public void Break()
    {
        GameObject.Find("FLOOR_1_gas_pipe_BRE").SetActive(true);
        gameObject.SetActive(false);
    }
}
