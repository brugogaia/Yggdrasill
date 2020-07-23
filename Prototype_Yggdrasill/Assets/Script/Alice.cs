using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Alice : MonoBehaviour
{
    private Canvas MenuPausa;
    private Canvas MenuMorte;

    private GameObject player;
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
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa").GetComponentInParent<Canvas>();
        MenuMorte = GameObject.FindGameObjectWithTag("MenuMorte").GetComponentInParent<Canvas>();
        //Canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.GetComponent<Animator>().SetBool("walk", false);
        Dialogo.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) StartCoroutine(Fading("Labyrinth_Alice"));

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
                    if (k < 0)
                    {
                        k++;
                        Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = true;
                    }
                    else
                    {
                        staparlando = false;
                        Dialogo.GetComponent<Canvas>().enabled = false;
                        StartCoroutine(Fading("UscitaLabirinto"));

                    }
                }
                else if (Input.GetKeyDown(KeyCode.E) && !Dialogo.transform.GetChild(k).GetComponent<Image>().enabled)
                {
                    Dialogo.transform.GetChild(k).GetComponent<Image>().enabled = true;
                }

            }
        

            if (!presa)
            {
                GetComponent<AudioSource>().mute = false;

                /*Vector3 direction = target.transform.position - transform.position;
                transform.LookAt(target.transform);
                direction.Normalize();
                movement = direction;*/
                this.GetComponent<Animator>().SetBool("walk", true);

            }
            else
            {
                GetComponent<AudioSource>().mute = true;
                this.GetComponent<Animator>().SetBool("walk", false);
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                this.GetComponentInParent<PathFollower>().speed = 0;
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

    private void FixedUpdate()
    {
        //moveAlice(movement);
    }
    void moveAlice(Vector3 direction)
    {
        //rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

    public void setPresa(bool boolena)
    {
        presa = boolena;
        staparlando = true;
    }


    IEnumerator Fading(string nomeScena)
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene(nomeScena, LoadSceneMode.Single);
        //DontDestroyOnLoad(Canvas);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CollPlayer" && !presa)
        {
            Vector3 direction = transform.position - player.transform.position;
            //float distanza = Vector3.Distance(player.transform.position, transform.position);
            player.transform.Translate(direction * 3);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CollPlayer" && !presa)
        {
            Vector3 direction = player.transform.position - transform.position;
            //float distanza = Vector3.Distance(player.transform.position, transform.position);
            player.transform.Translate(direction * 3);
        }
    }

    

}
