using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class AAA : MonoBehaviour
{
    public UnityEvent Event;
    public List<int> Count;
    public List<int> TargetCount;
    public void Add(int num)
    {
            Count[num]++;

        if (Enumerable.SequenceEqual(Count, TargetCount))
        {
            Event.Invoke();
        }
    }
}
