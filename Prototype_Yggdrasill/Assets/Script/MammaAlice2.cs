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
    public GameObject Dialogo;
    private bool arrivato = true;
    public Image white;
    public Animator anim;
    private bool staparlando = false;
    private int k = 0;
    private Canvas Orologio;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        Orologio = GameObject.FindGameObjectWithTag("Orologio").GetComponent<Canvas>();
        Orologio.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (white.color.a==0 && start)
        {
            start = false;
            canvas_video.enabled = true;
            PlayVideo();
        }*/
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
                if (k < 1)
                {
                    k++;
                    Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = true;
                }
                else
                {
                    staparlando = false;
                    GameObject.Destroy(Dialogo);
                    Orologio.enabled = true;
                    arrivato = false;
                    //StartCoroutine(Fading());
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && !Dialogo.transform.GetChild(k).GetComponent<Image>().enabled)
            {
                Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = true;
            }

        }
            
        }


    
    

    IEnumerator Fading()
    {
        anim.SetBool("Fadee", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("Labyrinth", LoadSceneMode.Single);
        DontDestroyOnLoad(Canvas);

    }

    
}
