using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Giocatore : MonoBehaviour
{
    private GameObject MenuPausa;

    public float speed = 50f;
    public float rotationSpeed = 100.0f;
    public bool vis3D = false;
    private bool isGrounded = true;
    private bool isDead = false;
    private Quaternion rotIniziale;
    private bool stavolando = false;
    bool ContattoTerra = true;

    private float y_prec = 0f;
    private float y_now = 0f;
    private bool staDecollando = false;
    private bool staAtterrando = false;

    private bool GroundDestro = true;
    private bool GroundSinistro = true;
    private bool GroundCentrale = true;

    private Transform enemy;

    private float velocita_verticale_camera = 1.0f;
    private float velocita_orizzontale_camera = 1.0f;

    private GameObject Puu;
    private Transform healthbar;

    public float Damage;

    private float waitTime = 0.2f;
    private float timer = 0.0f;

    private int k = 0;

    private bool stoCollidendo = false;
    private bool stoPrecipitando = false;

    public Animator anim;

    private bool Puu_Carico_Onda = true;

    private bool fermo = false;

    private Transform target;
    private Transform target_ricognizione;
    private Transform target_camera;

    private GameObject Camera;
    private Vector3 pos_iniziale;

    private float waitTime2 = 5.0f;
    private float timer2 = 0.0f;

    private float waitTime_Puu = 5.0f;
    private float timer_Puu = 0.0f;

    public AudioSource[] sounds;
    public AudioSource suonobacchetta;
    int count = 1;
    int puuspower = 1;
    public AudioSource vola;
    public AudioSource muore;
    public AudioSource poweR;

    private void Start()
    {
        rotIniziale = transform.rotation;
        Puu = GameObject.FindGameObjectWithTag("Puu");
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
        healthbar = GameObject.FindGameObjectWithTag("HealthBar").transform;
        healthbar.GetComponent<HealthBar>().setTreD(false);
        //anim = this.transform.GetComponentInChildren<Animator>();
        y_now = transform.position.y;
        target = GameObject.FindGameObjectWithTag("Target2D").transform;
        target_ricognizione = GameObject.FindGameObjectWithTag("Target_ricognizione").transform;
        target_camera = target;
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
        anim.SetBool("Grounded", true);


        sounds = GetComponents<AudioSource>();
        suonobacchetta = sounds[0];
        vola = sounds[1];
        muore = sounds[2];
        poweR = sounds[3];
    }

    void Update()
    {
        Camera.GetComponent<ScriptCamera>().GetTarget(this.target_camera);
        //Debug.Log("isGrounded = " + isGrounded);
        if (GroundDestro || GroundSinistro || GroundCentrale)
        {
            isGrounded = true;
            anim.SetBool("Grounded", true);
        }
        else isGrounded = false;

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
            stavolando = false;
            anim.SetBool("Flying", false);
            k = 0;
            Puu.GetComponent<Puu>().StopFlying();
        }
        else
        {
            anim.SetBool("Grounded", false);
        }

        y_prec = y_now;
        y_now = transform.position.y;
        if (y_now > y_prec)
        {
            staDecollando = true;
            staAtterrando = false;
        }
        else if (y_now < y_prec && (transform.position.y <12 && transform.position.y < -30))
        {
            stoPrecipitando = true;
        }
        else if (y_now < y_prec)
        {
            staDecollando = false;
            staAtterrando = true;
        }
        else if(y_now == y_prec)
        {
            staDecollando = false;
            staAtterrando = false;
        } 
        if (staAtterrando && ContattoTerra) anim.SetBool("Grounded", true);
        if (fermo) anim.SetBool("running", false);
            if (!MenuPausa.GetComponent<MenuPausa>().pausa && !fermo)
            {
                enemy = this.GetComponent<PlayerShooting>().enemy;

                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

                if (stavolando && !Puu.GetComponent<Puu>().flying) Puu.GetComponent<Puu>().isFlying();

                isDead = healthbar.GetComponent<HealthBar>().isDed();
            if (isDead)
            {
                Puu.GetComponent<Puu>().setNemico(null);
                if (count == 1)
                {
                    muore.Play();
                    count++;
                }
            }
                timer = timer + Time.deltaTime;
                //Debug.Log("is grounded " + isGrounded);
                if (Input.GetKeyDown("space") && isGrounded && !isDead)
                {
                    Jump();
                }
                else if (Input.GetKeyDown("space") && !isGrounded && !isDead)
                {
                    stavolando = true;
                Debug.Log("qua ci sta yahoo 2");
                    vola.Play();
                    
                    anim.SetBool("Flying", true);

                    if (k == 0)
                    {
                        Puu.GetComponent<Puu>().isFlying();
                        k++;
                    }
                }
                //if (Input.GetKey("space") && stavolando && !isDead) Fly();
                else if (Input.GetKeyUp("space") && stavolando && !isDead)
                {
                    stavolando = false;
                    anim.SetBool("Flying", false);
                    Puu.GetComponent<Puu>().StopFlying();
                    k = 0;
                }


                
            
            
            if(!Puu_Carico_Onda && timer_Puu <= waitTime_Puu)
            {
                timer_Puu = timer_Puu + Time.deltaTime;
            } else if(!Puu_Carico_Onda && timer_Puu > waitTime_Puu)
            {
                Puu_Carico_Onda = true;
                timer_Puu = 0.0f;
            }

            if (Input.GetKeyUp(KeyCode.R) || timer2>waitTime2)
            {
                timer2 = 0.0f;
                target_ricognizione.position = target.position;
                target_camera = target;
                Puu_Carico_Onda = false;
                puuspower = 1;
                //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptCamera>().StopRicognizione();
            } else if (Input.GetKey(KeyCode.R) && timer2 <= waitTime2 && Puu_Carico_Onda)
            {
                if (puuspower == 1)
                {
                    //Debug.Log("pusspower");
                    poweR.Play();
                    puuspower++;
                }
                target_camera = target_ricognizione;
                timer2 = timer2 + Time.deltaTime;

                if (transform.position.y > 0)
                {
                    if (target_ricognizione.position.y <= -30)
                    {
                        target_ricognizione.Translate(Input.GetAxis("Horizontal2") * Time.deltaTime * speed * 3f, 0, 0);

                    }
                    else
                    {
                        target_ricognizione.Translate(0, -Input.GetAxis("Horizontal2") * Time.deltaTime * speed, Input.GetAxis("Horizontal2") * Time.deltaTime * speed * 0.2f);

                    }
                    //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptCamera>().Ricognizione();
                }
                else
                {
                    if (target_ricognizione.position.y >= -31)
                    {
                        target_ricognizione.Translate(Input.GetAxis("Horizontal2") * Time.deltaTime * speed * 3f, 0, 0);

                    }
                    else
                    {
                        target_ricognizione.Translate(0, Input.GetAxis("Horizontal2") * Time.deltaTime * speed, Input.GetAxis("Horizontal2") * Time.deltaTime * speed * 0.2f);

                    }
                }

            }


            if(stoPrecipitando && !isGrounded)
            {
                anim.SetBool("precipita", true);
                //Debug.Log("Precipita");
            }
            else if(stoPrecipitando && isGrounded)
            {
                anim.SetBool("precipita", false);
                stoPrecipitando = false;
            }

            if(transform.position.y<12 && Input.GetKey(KeyCode.F) && !isDead)
            {
                Risali();
                anim.SetBool("Flying", true);

                if (k == 0)
                {
                    Puu.GetComponent<Puu>().isFlying();
                    k++;
                }
            } else if (Input.GetKeyUp(KeyCode.F))
            {
                stavolando = false;
                anim.SetBool("Flying", false);
                Puu.GetComponent<Puu>().StopFlying();
                k = 0;
            }

            speed = 50f;
                //Debug.Log("k giocatore = " + k);
                if (!vis3D)
                {


                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
                    {
                        anim.SetBool("running", true);
                    }
                    else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
                    {
                        anim.SetBool("running", false);
                    }


                    float movimentoOrizzontale = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
                    //Debug.Log(movimentoOrizzontale);
                    if (!isDead && !fermo)
                    {

                        if (enemy == null || (enemy != null && !enemy.GetComponent<Enemy>().isLittle() /*&& Vector3.Distance(transform.position, enemy.position)>=20)*/ ) || (enemy != null && enemy.GetComponent<Enemy>().isLittle()))
                        {
                            transform.Translate(movimentoOrizzontale, 0, 0);
                            if (movimentoOrizzontale > 0)
                                GameObject.FindGameObjectWithTag("idle").transform.forward = Vector3.right;
                            else if (movimentoOrizzontale == 0)
                                GameObject.FindGameObjectWithTag("idle").transform.forward = GameObject.FindGameObjectWithTag("idle").transform.forward;
                            else
                                GameObject.FindGameObjectWithTag("idle").transform.forward = -Vector3.right;
                        }

                    }

                }
                else
                {

                    float movimentoAvantiIndietro = Input.GetAxis("Vertical") * speed;
                    float movimentoDxSx = Input.GetAxis("Horizontal") * speed;
                    movimentoDxSx *= Time.deltaTime;
                    movimentoAvantiIndietro *= Time.deltaTime;
                    Vector3 AD = transform.right * movimentoAvantiIndietro;
                    Vector3 DxSx = transform.forward * -movimentoDxSx;
                    if (!isDead) transform.position = transform.position + AD + DxSx;



                    Vector2 mouseMovement = new Vector2();
                    mouseMovement.x = Input.GetAxis("Mouse X");
                    mouseMovement.y = Input.GetAxis("Mouse Y");

                    float angolo_orizzontale_rotazione = 0.0f;
                    float angolo_verticale_rotazione = 0.0f;
                    if (mouseMovement.magnitude > 0.05)
                    {
                        //Rotazione asse verticale camera
                        angolo_verticale_rotazione -= mouseMovement.y * this.velocita_verticale_camera;

                        //Rotazione asse orizzontale camera
                        angolo_orizzontale_rotazione = mouseMovement.x * velocita_orizzontale_camera;
                    }


                    transform.Rotate(0, angolo_orizzontale_rotazione, 0);
                    //transform.Rotate(0, 0, angolo_verticale_rotazione);
                }
            }
            else
            {
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        
        
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            Atterra();
        }

        if (Input.GetKey("space") && stavolando && !isDead) Fly();
    }

    private void Jump()
    {
        //Debug.Log("Lo sto spingendo su");
        anim.SetBool("Jump", true);
        if (anim.GetBool("running"))
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 25f, 0f), ForceMode.Impulse);
        }

        else
        {
            Invoke("ForzaSalto", 0.5f);
        }
        
    }

    private void ForzaSalto()
    {
        
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 25f,0f), ForceMode.Impulse);
    }

    private void Atterra()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -1f, 0f), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("sto collidendo con " + collision.collider);
        
        if(collision.gameObject.tag == "Enemy" && !collision.gameObject.GetComponent<Enemy>().little)
        {
            TakeDamage(100f);
            
        }
        else if (collision.collider != Puu.GetComponent<Collider>())
        {
            if(collision.relativeVelocity.magnitude > 40f)
            {
                //Debug.Log("Collisione!");
                Damage = collision.relativeVelocity.magnitude;
                Damage = Damage / 50;
                TakeDamage(Damage);
                //setHit();
            }
            stoCollidendo = true;
        }
    }

    
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("sto collidendo con " + collision.collider);
        if (collision.gameObject.tag != "Ground")
        {
            stoCollidendo = true;

        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("non sto piu collidendo con " + collision.collider);
        if (collision.gameObject.tag != "Ground")
        {
            stoCollidendo = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground") ContattoTerra = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground") ContattoTerra = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground") ContattoTerra = false;
    }

    private void Fly()
    {
        
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0.6f, 0f), ForceMode.Impulse);
    }

    private void Risali()
    {
        stavolando = true;

        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 1.5f, 0f), ForceMode.Impulse);
    }


    public void TakeDamage(float DamageAmount)
    {
        healthbar.GetComponent<HealthBar>().FallDamage(DamageAmount);
    }
    public void Cura()
    {
        healthbar.GetComponent<HealthBar>().Cura();
    }

    public void PiantaDamage()
    {
        healthbar.GetComponent<HealthBar>().PiantaDamage();
    }

    public bool isDed()
    {
        return isDead;
    }

    public void setGroundDestro(bool bolena)
    {
        GroundDestro= bolena;
    }

    public void setGroundSinistro(bool bolena)
    {
        GroundSinistro = bolena;
    }

    public void setGroundCentrale(bool bolena)
    {
        GroundCentrale = bolena;
    }

    public void StaiFermo()
    {
        fermo = true;
    }
}
