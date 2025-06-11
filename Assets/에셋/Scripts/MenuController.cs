using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas MenuCanvas;
    public Slider HeightSlider;
    public GameObject CameraOffset;
    public TMPro.TMP_Text UITextMeshPro;
    public void ToggleMenu()
    {
        MenuCanvas.enabled = !MenuCanvas.enabled;
        print(MenuCanvas.enabled);
    }
    public void HeightValueChanged()
    {
        CameraOffset.transform.SetPositionAndRotation(new Vector3(CameraOffset.transform.position.x, HeightSlider.value / 100, CameraOffset.transform.position.z), CameraOffset.transform.rotation);
        UITextMeshPro.text = HeightSlider.value.ToString();
    }
}
