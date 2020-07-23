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
    public GameObject panel;
    [SerializeField] Image UI_Image_Parla;
    [SerializeField] Image UI_Image_Aiuta;
    public VideoPlayer vid;
    public Canvas canvas_video;
    public GameObject Dialogo;
    public GameObject Dialogo2;
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
        UI_Image_Parla.enabled = false;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        canvas_video.enabled = false;
        Dialogo.GetComponent<Canvas>().enabled = false;
        Dialogo2.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) StartCoroutine(FadingShortcut("2D_end"));

        if (arrivato) player.GetComponent<Giocatore>().StaiFermo();
        if(arrivato && Input.GetKeyDown(KeyCode.E))
        {
            UI_Image_Parla.enabled = false;
            arrivato = false;
            //PlayVideo();
            staparlando = true;
        }
        else if(arrivato && Input.GetKeyDown(KeyCode.X))
        {
            UI_Image_Parla.enabled = false;
            StartCoroutine(Fading("Labyrinth_noAscolta"));
        }

        if (staparlando)
        {
            if(!Dialogo.GetComponent<Canvas>().enabled && k==0)
            {
                Dialogo.GetComponent<Canvas>().enabled = true;

            }
            if (Input.GetKeyDown(KeyCode.E) && Dialogo.transform.GetChild(k).GetComponent<Image>().enabled)
            {
                Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = false;
                if (k < 3)
                {
                    k++;
                    Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = true;
                }
                else
                {
                    staparlando = false;
                    Dialogo.GetComponent<Canvas>().enabled = false;

                    UI_Image_Aiuta.enabled = true;
                }
            }
            else if(Input.GetKeyDown(KeyCode.E) && !Dialogo.transform.GetChild(k).GetComponent<Image>().enabled)
            {
                Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = true;
            }

        }
        else
        {
            if (UI_Image_Aiuta.enabled && Input.GetKeyDown(KeyCode.E))
            {
                k = 0;
                UI_Image_Aiuta.enabled = false;
                //StartCoroutine(Fading());
                Dialogo2.GetComponent<Canvas>().enabled = true;
                

            }
            else if (Dialogo2.GetComponent<Canvas>().enabled && Input.GetKeyDown(KeyCode.E) && Dialogo2.transform.GetChild(k).GetComponent<Image>().enabled)
            {
                Dialogo2.transform.GetChild(k).GetComponent<Image>().enabled = false;
                if (k < 1)
                {
                    k++;
                    Dialogo2.transform.GetChild(k).GetComponent<Image>().enabled = true;
                    Dialogo2.transform.GetChild(2).GetComponent<Canvas>().enabled = false;
                    Dialogo2.transform.GetChild(3).GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    staparlando = false;
                    Dialogo2.GetComponent<Canvas>().enabled = false;
                    StartCoroutine(Fading("Labyrinth"));
                    //UI_Image_Aiuta.enabled = true;
                }
            }
            else if (Dialogo2.GetComponent<Canvas>().enabled  && !Dialogo2.transform.GetChild(k).GetComponent<Image>().enabled)
            {
                Dialogo2.transform.GetChild(k).GetComponent<Image>().enabled = true;
            }
            else if (UI_Image_Aiuta.enabled && Input.GetKeyDown(KeyCode.X))
            {
                UI_Image_Aiuta.enabled = false;
                StartCoroutine(Fading("Labyrinth_noAiuta"));
            }
        }
        
        
        
        

    }
    
    
    IEnumerator FadingShortcut(string nomeScena)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene(nomeScena, LoadSceneMode.Single);
        //DontDestroyOnLoad(Canvas);

    }

    IEnumerator Fading(string nomeScena)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene(nomeScena, LoadSceneMode.Single);
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
