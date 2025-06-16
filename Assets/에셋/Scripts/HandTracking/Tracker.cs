using UnityEngine;
public class Tracker : MonoBehaviour
{
    float L = 0;
    float R = 0;
    public float Buffer = 0.2f ;
    public void SetL(float newL)
    {
        if (newL > L && newL <= L + Buffer)
        {
            L = newL;
        }
    }
    public void SetR(float newR)
    {
        if (newR > R && newR <= R + Buffer)
        {
            R = newR;
        }
    }
    public void ResetTracker()
    {
        R = 0;
        L = 0;
    }
    public float GetL()
    {
        return L;
    }
    public float GetR()
    {
        return R;
    }
}
