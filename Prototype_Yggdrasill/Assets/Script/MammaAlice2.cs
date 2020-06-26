using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MammaAlice2 : MonoBehaviour
{
    private Transform player;
    private GameObject Canvas;
    public GameObject panel;
    public VideoPlayer vid;
    public Canvas canvas_video;
    public GameObject Dialogo;
    private bool arrivato = false;
    public bool parlato = false;
    public Image white;
    public Animator anim;
    private bool staparlando = false;
    private int k = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvas_video.enabled = false;
        Dialogo.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

      

        if (arrivato)
        {
                player.GetComponent<Giocatore>().StaiFermo();
                if (!Dialogo.GetComponent<Canvas>().enabled && k == 0)
            {
                Dialogo.GetComponent<Canvas>().enabled = true;

            }
            if (Input.GetKeyDown(KeyCode.E) && Dialogo.transform.GetChild(k).GetComponent<Image>().enabled)
            {
                Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = false;
                if (k < 4)
                {
                    k++;
                    Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = true;
                }
                else
                {
                    staparlando = false;
                    Dialogo.GetComponent<Canvas>().enabled = false;

                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && !Dialogo.transform.GetChild(k).GetComponent<Image>().enabled)
            {
                Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = true;
            }

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
        SceneManager.LoadScene("SampleScene - Copia", LoadSceneMode.Single);
        Debug.Log("cambio scena");
        DontDestroyOnLoad(Canvas);
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
        if (other == player.GetComponent<Collider>())
        {

            arrivato = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {

            arrivato = false;
        }
    }
}
