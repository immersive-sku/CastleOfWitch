using System.Threading;
using UnityEngine;

public class AAA : MonoBehaviour
{
    int Count = 0;
    public void Add()
    {
        Count++;
        if (Count >= 10)
        {
            Component.FindAnyObjectByType<Rewinder>().Reset();
        }
    }
}
