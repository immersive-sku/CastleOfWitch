using System.Collections;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject PotionDrop;
    public void Spawn()
    {
        StartCoroutine("SpawnCorutine");
    }

    public void StopSpawn()
    {
        StopCoroutine("SpawnCorutine");
    }
    IEnumerator SpawnCorutine()
    {
        yield return new WaitForSeconds(1);
        Instantiate(PotionDrop, gameObject.transform.position, gameObject.transform.rotation);
    }
}
