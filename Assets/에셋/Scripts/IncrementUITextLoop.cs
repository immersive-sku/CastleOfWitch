using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.UI;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    /// <summary>
    /// Add this component to a GameObject and call the <see cref="IncrementText"/> method
    /// in response to a Unity Event to update a text display to count up with each event.
    /// </summary>
    public class IncrementUITextLoop : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The Text component this behavior uses to display the incremented value.")]
        Image m_Image;
        public List<Sprite> SpriteList;
        public AAA aaa;

        int m_Count;

        /// <summary>
        /// Increment the string message of the Text component.
        /// </summary>
        public void IncrementText()
        {
            m_Count += 1;
            if (m_Count > aaa.Max) m_Count = 0;
            if (m_Image != null)
                m_Image.sprite = SpriteList[m_Count];
            print(m_Count);
        }
    }
}
