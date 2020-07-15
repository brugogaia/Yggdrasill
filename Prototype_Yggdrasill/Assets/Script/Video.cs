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
    
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        PlayVideo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayVideo()
    {
        Time.timeScale = 0;
        vid.Play();
        vid.loopPointReached += EndReached;
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vid.Stop();
        canvas_video.enabled = false;
        SceneManager.LoadScene("UscitaLabirinto", LoadSceneMode.Single);
        DontDestroyOnLoad(Canvas);
    }

    
}
