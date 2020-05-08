using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MammaAlice : MonoBehaviour
{
    private Transform player;
    private GameObject Canvas;
    [SerializeField] Image UI_Image_Parla;
    [SerializeField] Image UI_Image_Aiuta;
    public VideoPlayer vid;
    public Canvas canvas_video;
    private bool arrivato = false;
    public bool parlato = false;
    public Image white;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UI_Image_Parla.enabled = false;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvas_video.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(arrivato && Input.GetKeyDown(KeyCode.E))
        {
            UI_Image_Parla.enabled = false;
            canvas_video.enabled = true;
            arrivato = false;
            //PlayVideo();
            StartCoroutine(Fading());

        }
        else if(arrivato && Input.GetKeyDown(KeyCode.X))
        {
            UI_Image_Parla.enabled = false;
            //Carichiamo scena labirinto senza Alice
        }
        
        if (parlato && Input.GetKeyDown(KeyCode.E))
        {
            UI_Image_Aiuta.enabled = false;
            StartCoroutine(Fading());
            

        }
        else if (parlato && Input.GetKeyDown(KeyCode.X))
        {
            UI_Image_Aiuta.enabled = false;
            //Carichiamo scena labirinto con Alice invisibile che si sentono solo i passi
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
        parlato = true;
        UI_Image_Aiuta.enabled = true;
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("Labyrinth", LoadSceneMode.Single);
        DontDestroyOnLoad(Canvas);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == player.GetComponent<Collider>())
        {
            UI_Image_Parla.enabled = true;
            arrivato = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            UI_Image_Parla.enabled = false;
            arrivato = false;
        }
    }
}
