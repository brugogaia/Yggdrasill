using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPremessa : MonoBehaviour
{
    public VideoPlayer vid;
    private GameObject Canvas;
    public Canvas canvas_video;
    public Canvas welcome;
    private bool isplaying = true;

    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (isplaying)
        {
            vid.loopPointReached += EndReached;
        }

    }



    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //vid.Stop();
        isplaying = false;
        canvas_video.enabled = false;
        welcome.enabled = true;
        //SceneManager.LoadScene("UscitaLabirinto", LoadSceneMode.Single);
        //DontDestroyOnLoad(Canvas);
    }


}
