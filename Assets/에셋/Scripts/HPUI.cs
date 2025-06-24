using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.UI;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    /// <summary>
    /// Add this component to a GameObject and call the <see cref="IncrementText"/> method
    /// in response to a Unity Event to update a text display to count up with each event.
    /// </summary>
    public class HPUI : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The Text component this behavior uses to display the incremented value.")]
        Image m_Image;
        public List<Sprite> SpriteList;
        public Character aaa;


        int m_Count;

        public void Update()
        {
                m_Image.sprite = SpriteList[Mathf.Min(Mathf.Max(0, 10 - (int)aaa.HP / 10), 9)];
        }
    }
}
