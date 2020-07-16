using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class OrologioLoadScene : MonoBehaviour
{
    public Canvas canvas;
    public Image white;
    public Animator anim;
    private bool attivo = false;
    public VideoPlayer vid;
    public Canvas Canvas_dialogo;
    public Canvas canvas_video;
    private bool isplaying = false;
    // Start is called before the first frame update
    void Start()
    {
        Canvas_dialogo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canvas.enabled && !attivo)
        {
            attivo = true;
            Invoke("LaunchVideo",1);
        }
        if (isplaying)
        {
            vid.loopPointReached += EndReached;
        }
    }

    private void LaunchVideo()
    {
        canvas.enabled = false;
        isplaying = true;
        canvas_video.enabled = true;
        vid.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //vid.Stop();
        isplaying = false;
        canvas_video.enabled = false;
        Canvas_dialogo.enabled = true;
        //SceneManager.LoadScene("UscitaLabirinto", LoadSceneMode.Single);
        //DontDestroyOnLoad(Canvas);
    }
}
