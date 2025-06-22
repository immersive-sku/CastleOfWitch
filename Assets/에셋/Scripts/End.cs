using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class End : MonoBehaviour
{
    public string NextScene;
    VideoPlayer vp;
    void Start()
    {
        vp = gameObject.GetComponent<VideoPlayer>();
        vp.loopPointReached += OpenNextScene;
    }

    public void OpenNextScene(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(NextScene);
    }
}
