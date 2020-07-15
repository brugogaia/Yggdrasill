using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer vid;
    private GameObject Canvas;
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
        SceneManager.LoadScene("UscitaLabirinto", LoadSceneMode.Single);
        //DontDestroyOnLoad(Canvas);
    }

    
}
