﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer vid;
    public Canvas Canvas_dialogo;
    public Canvas canvas_video;
    private bool isplaying = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Canvas_dialogo.enabled = false;
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
        //Canvas_dialogo.enabled = true;
        //SceneManager.LoadScene("UscitaLabirinto", LoadSceneMode.Single);
        //DontDestroyOnLoad(Canvas);
    }

    
}
