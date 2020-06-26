using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer vid;
    public Canvas canvas_video;
    private GameObject Canvas;
    private bool start = true;
    public Image white;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            canvas_video.enabled = true;
            PlayVideo();
        }
    }

    void PlayVideo()
    {
        Time.timeScale = 0;
        vid.Play();
        vid.loopPointReached += EndReached;
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        canvas_video.enabled = false;
        SceneManager.LoadScene("UscitaLabirinto", LoadSceneMode.Single);
        DontDestroyOnLoad(Canvas);
    }

    
}
