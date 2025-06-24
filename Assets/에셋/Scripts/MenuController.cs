using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas MenuCanvas;
    public Slider HeightSlider;
    public GameObject CameraOffset;
    public TMPro.TMP_Text UITextMeshPro;
    public TMPro.TMP_Dropdown UIDropdown;
    public Transform T;
    public void ToggleMenu()
    {
        MenuCanvas.enabled = !MenuCanvas.enabled;
        transform.SetPositionAndRotation(T.position, T.rotation);
        print(MenuCanvas.enabled);
    }
    public void HeightValueChanged()
    {
        CameraOffset.transform.SetPositionAndRotation(new Vector3(CameraOffset.transform.position.x, HeightSlider.value / 100, CameraOffset.transform.position.z), CameraOffset.transform.rotation);
        UITextMeshPro.text = HeightSlider.value.ToString();
        transform.SetPositionAndRotation(T.position, T.rotation);
    }
    public void SoftReset()
    {
        GameObject.Find("Elevator").GetComponent<Elevator>().Save();
        SceneManager.LoadScene(1);
    }
    public void MapSelect(int input)
    {
        switch(input)
        {
            case 0:
                break;
            case 1:
                GameObject.Find("Elevator").GetComponent<Elevator>().SetFloor(0);
                break;
            case 2:
                GameObject.Find("Elevator").GetComponent<Elevator>().SetFloor(1);
                break;
            case 3:
                GameObject.Find("Elevator").GetComponent<Elevator>().SetFloor(2);
                break;
            case 4:
                GameObject.Find("Elevator").GetComponent<Elevator>().SetFloor(3);
                break;
            case 5:
                GameObject.Find("Elevator").GetComponent<Elevator>().SetFloor(4);
                break;
            case 6:
                SceneManager.LoadScene(0);
                break;
            case 7:
                SceneManager.LoadScene(1);
                break;
            case 8:
                SceneManager.LoadScene(2);
                break;
        }
    }
}
