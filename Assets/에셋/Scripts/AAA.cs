using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AAA : MonoBehaviour
{
    public UnityEvent Event;
    public List<int> Count;
    public List<int> TargetCount;
    public int Max = 3;
    public void Add(int num)
    {
        if (Count[num] + 1 > Max)
        {
            Count[num] = 0;
        }
        else
        {
            Count[num]++;
        }


        if (Enumerable.SequenceEqual(Count, TargetCount))
        {
            Event.Invoke();
        }
    }
}
