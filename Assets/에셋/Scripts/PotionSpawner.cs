using System.Collections;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject TransformObject;
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
        while(true)
        {
            Instantiate(PotionDrop, TransformObject.transform.position, TransformObject.transform.rotation);
            print("spawnPotion");
            yield return new WaitForSeconds(1);
        }
    }
}
