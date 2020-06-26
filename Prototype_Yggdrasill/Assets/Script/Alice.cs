using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Alice : MonoBehaviour
{
    private Canvas MenuPausa;
    private Canvas MenuMorte;

    private float speed = 20f;
    private float waitTime = 2f;
    private float timer = 2f;
    private Transform target;
    private Vector3 position;
    private bool presa = false;
    [SerializeField] Canvas Dialogo;
    private bool staparlando = false;
    private int k = 0;

    private GameObject Canvas;
    public Image white;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("TargetAlice").transform;
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa").GetComponentInParent<Canvas>();
        MenuMorte = GameObject.FindGameObjectWithTag("MenuMorte").GetComponentInParent<Canvas>();
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.GetComponent<Animator>().SetBool("walk", false);
        Dialogo.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuPausa.enabled && !MenuMorte.enabled)
        {
            if (staparlando)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Staifermo();
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
                        StartCoroutine(Fading());

                    }
                }
                else if (Input.GetKeyDown(KeyCode.E) && !Dialogo.transform.GetChild(k).GetComponent<Image>().enabled)
                {
                    Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = true;
                }

            }
        

            timer = timer + Time.deltaTime;
            if (!presa)
            {
                GetComponent<AudioSource>().mute = false;
                if (timer > waitTime)
                {
                    position = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                    
                    timer = 0.0f;
                }
                transform.Translate(position * Time.deltaTime * speed);
                if (position != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(position, Vector3.up);
                    transform.rotation = rotation;
                    this.GetComponent<Animator>().SetBool("walk", true);
                }
                else
                {
                    this.GetComponent<Animator>().SetBool("walk", false);
                    
                }
                
            }
            else
            {
                GetComponent<AudioSource>().mute = true;
                this.GetComponent<Animator>().SetBool("walk", false);
                //speed = 100f;
                //transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
                /*transform.parent = target.transform;
                Object.Destroy(this.GetComponent<Rigidbody>());*/
            }
        }
        else
        {
            this.GetComponent<Animator>().SetBool("walk", false);
            GetComponent<AudioSource>().mute = true;
        }

            

    }

    public void setPresa(bool boolena)
    {
        presa = boolena;
        staparlando = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag!="Ground")
        this.GetComponent<Animator>().SetBool("walk", false);
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("UscitaLabirinto", LoadSceneMode.Single);
        DontDestroyOnLoad(Canvas);

    }

}
