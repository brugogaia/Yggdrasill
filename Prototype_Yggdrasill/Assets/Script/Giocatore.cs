using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Giocatore : MonoBehaviour
{
    [SerializeField] int Livello_Partenza;
    private int LivelloAttuale;

    private bool StaPrendendoOggetto = false;

    [SerializeField] Transform healthbar;
    private float VitaInizioLivello;
    private float VitaAttuale;

    private bool girataGravitaInSalto = false;
    private int num_oggetti_collidenti = 0;
    private GameObject camera_interpolata;
    private int speed = 7;
    private int jump = 8;
    private bool deathReaction = false;
    [SerializeField]
 
    private float rotationSpeed = 8f;
    private bool isGrounded;
    private float vel, previousFrameVel;
    private Vector2 inputController = new Vector2();
    private Rigidbody rigidBody;
    private bool rotating = false;
    private bool inPausa;
    [SerializeField]
    private Animator animator;

    private Vector3 velocitaPrimaDelSalto = new Vector3();

    private Dictionary<string, float> ultimaPressionePulsantiCambioGravita = new Dictionary<string, float>();


    //AUDIO
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] stepSounds;
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip landSound;
    [SerializeField]
    private AudioClip damageSound;
    void Start()
    {
        LivelloAttuale = Livello_Partenza;

        //VitaAttuale = healthbar.GetComponent<HealthBar>().GetCurrentHealth();
        //VitaInizioLivello = VitaAttuale;
        

        ultimaPressionePulsantiCambioGravita.Add("sinistra", 0);
        ultimaPressionePulsantiCambioGravita.Add("destra", 0);
        rigidBody = gameObject.GetComponent<Rigidbody>();   //Prende il Component Rigidbody dal GameObject attuale

        Cursor.visible = false;//Visibilità mouse
        Cursor.lockState = CursorLockMode.Locked;
        inPausa = false;



        camera_interpolata = GameObject.FindGameObjectWithTag("MainCamera");

        //AUDIO
        audioSource = GetComponent<AudioSource>();
    }

    public void setStaPrendendoOggetto(bool boolean)
    {
        StaPrendendoOggetto = boolean;
    }

    public bool isDead()
    {
        return deathReaction;
    }

    public void SetLivelloAttuale(int livello)
    {
        if (livello != LivelloAttuale)
        {
            VitaInizioLivello = VitaAttuale;
        }
        LivelloAttuale = livello;
        Debug.Log("nuovo livello: " + LivelloAttuale);
    }

    public int GetLivelloAttuale()
    {
        return LivelloAttuale;
    }

    public float GetVitaInizioLivello()
    {
        return VitaInizioLivello;
    }

    public float GetVitaAttuale()
    {
        return VitaAttuale;
    }


    public bool PlayerIsGrounded()
    {
        return isGrounded;
    }
    // Update is called once per frame
    void Update()
    {

        //Debug.Log(deathReaction + " <------ death reaction giocatore");
        //deathReaction = GetComponent<HealthBar>().isDead;
        inputController.x = Input.GetAxis("Horizontal");
        inputController.y = Input.GetAxis("Vertical");

//        VitaAttuale = healthbar.GetComponent<HealthBar>().GetCurrentHealth();

        //Debug.Log(num_oggetti_collidenti);
        // Debug.Log(isGrounded);
        if (isGrounded)
        {
            velocitaPrimaDelSalto = rigidBody.velocity;
            if (animator.GetBool("inVolo") && !rotating) animator.SetBool("inVolo", false);
        }
        
        if (!rotating && isGrounded && Input.GetButtonDown("Jump"))
        {
            isGrounded = false;
            
            Invoke("funzioneSalto", 0.1f);
        }


        Vector3[] basi = basi_view_dependant_proiettate();
        if ( (!rotating))
            {
                float mouse_orizzontale = Input.GetAxis("Mouse X");
                if (Mathf.Abs(mouse_orizzontale) > 0.05f)
                {
                    Quaternion rotazione = Quaternion.AngleAxis(mouse_orizzontale*10, basi[1]);

                    this.rotazionePersonaggioVersoInput(rotazione * basi[2], false);
                }

            }
    }


    public Quaternion rotazione_view_dependant_proiettate()
    {
        Vector3[] v = basi_view_dependant_proiettate();

        return Quaternion.LookRotation(-v[1], v[2]);
    }

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
        Debug.Log("Rallentato! Speed = " + speed);
    }
    public void setDeath()
    {
        deathReaction = true;
    }

    public Vector3[] basi_view_dependant_proiettate()
    {
        /*
        Indici:
        0 → destra
        1 → alto
        2 → avanti
        Messi per rispettare le coordinate sinistrorse usate da Unity.

        I vettori sono già normalizzati
        */
        Vector3[] v = new Vector3[3];

        v[0] = camera_interpolata.transform.right;
        if (rotating)
        {
            v[1] = camera_interpolata.transform.up;
        }
        else
        {
            //Non vorrei che saltasse tutto in aria quando si gira la gravità
            v[1] = -Physics.gravity.normalized;
        }
        v[2] = camera_interpolata.transform.forward;

        v[0] = Vector3.ProjectOnPlane(v[0], v[1]).normalized;
        v[2] = Vector3.ProjectOnPlane(v[2], v[1]).normalized;

        return v;
    }

    void FixedUpdate()
    {
        if (StaPrendendoOggetto)
        {
            inputController.x = 0;
            inputController.y = 0;
            jump = 0;
            speed = 0;
        }
        else
        {
            inputController.x = Input.GetAxis("Horizontal");
            inputController.y = Input.GetAxis("Vertical");
            jump = 8;
            speed = 7;
        }

        if (deathReaction)
        {
            inputController.x = 0;
            inputController.y = 0;
            jump = 0;
            speed = 0;
        }
        Vector3[] basi = basi_view_dependant_proiettate();

        Vector3 input_tastiera_view_dependant = (
            basi[2] * inputController.y
            +
            basi[0] * inputController.x
        );
        //Debug.Log("Input tastiera view "+input_tastiera_view_dependant);
        // if (inputController.magnitude > 0.05)
        // {
        //     rotazionePersonaggioVersoInput(input_tastiera_view_dependant, false);
        // }

        if (!rotating)
        {
            

            if (inputController.magnitude > 0.05 && rigidBody.velocity.magnitude < speed)
            {
                float moltiplicatoreForza = 0.0f;
                if (isGrounded)
                {
                    moltiplicatoreForza = 1;
                }
                else
                {
                    // Debug.Log("giratagravitainsalto: "+girataGravitaInSalto);
                    // if (!girataGravitaInSalto)//Se è la prima volta che giro la gravità a mezz'aria in questo salto
                    // {
                    //     moltiplicatoreForza = 1.1f;
                    // }
                    // else
                    // {
                    //     moltiplicatoreForza = 0;
                    // }
                    moltiplicatoreForza = 0.1f;

                }

                float x = rigidBody.velocity.magnitude;
                float modulo_vel = -2 * (x / 2 - speed) + speed;
                rigidBody.AddForce(modulo_vel * (input_tastiera_view_dependant) * moltiplicatoreForza);

               


                //Evito drifting
                Vector3 forza = Vector3.Dot(rigidBody.velocity, transform.right) * transform.right;
                rigidBody.AddForce(-6 * forza);
            }
            else
            {
                if (this.isGrounded)
                {
                    //Smorzamento
                    rigidBody.AddForce(-8 * rigidBody.velocity);
                }

            }

        }
    }

    private void rotazionePersonaggioVersoInput(Vector3 input_tastiera_view_dependant, bool istantanea)
    {
        float parametro_temporale = Time.fixedDeltaTime * rotationSpeed;

        if (istantanea)
        {
            parametro_temporale = 1;
        }

        //Rotazione verso input tastiera proiettati
        Quaternion to_rotation = Quaternion.LookRotation(input_tastiera_view_dependant, -Physics.gravity);
        rigidBody.MoveRotation(
            Quaternion.Slerp(transform.rotation, to_rotation, parametro_temporale)
        );


    }

    
    

    

    public void SetPausa(bool pausa)
    {
        inPausa = pausa;
    }

    void funzioneSalto()
    {
        isGrounded = false;
        girataGravitaInSalto = false;
        rigidBody.velocity = Vector3.ProjectOnPlane(rigidBody.velocity, -Physics.gravity.normalized);
        rigidBody.velocity += jump * 1.0f * Vector3.Normalize(-Physics.gravity);
        if (inputController.magnitude > 0.05)
        {
            Vector3[] basi = basi_view_dependant_proiettate();
            Vector3 input_tastiera_view_dependant = (
                basi[2] * inputController.y
                +
                basi[0] * inputController.x
            );
            input_tastiera_view_dependant = input_tastiera_view_dependant.normalized;
            //Mi prendo solo la direzione

            Vector3 vel_da_aggiungere = Vector3.Lerp(
                input_tastiera_view_dependant * 4,
                input_tastiera_view_dependant * 0.1f,
                Vector3.ProjectOnPlane(rigidBody.velocity, -Physics.gravity.normalized).magnitude
            );
            rigidBody.velocity += vel_da_aggiungere;

            //Se non ha il cubo in mano
            

        }

        //AUDIO
        audioSource.clip = jumpSound;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" || collider.isTrigger)
        {
            return;
        }
        this.num_oggetti_collidenti++;
        if (num_oggetti_collidenti == 1)
        {
            //Debug.Log(num_oggetti_collidenti);
            this.isGrounded = true;
            animator.SetBool("inSalto", false);
            animator.SetBool("inVolo", false);

            //AUDIO
            if (/*audioSource.clip == jumpSound && */!audioSource.isPlaying && Time.timeScale != 0)
            {
                audioSource.clip = landSound;
                audioSource.volume = 0.3f;
                audioSource.Play();
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player" || collider.isTrigger)

        {
            return;
        }
        this.num_oggetti_collidenti--;
        if (num_oggetti_collidenti < 0)
        {
            num_oggetti_collidenti = 0;
        }
        if (num_oggetti_collidenti == 0)
        {
            // Debug.Log(num_oggetti_collidenti);
            // animator.SetBool("inVolo", true);
            this.isGrounded = false;
        }

    }

    /*public void AnimazioneContinua()
    {
        animator.SetFloat("velocita",
            Vector3.ProjectOnPlane(
                velocitaPrimaDelSalto, -Physics.gravity.normalized
            ).magnitude
        );
        animator.SetFloat("getAxis", inputController.magnitude);
    }
    public void AnimazioneSalto()
    {
        animator.SetBool("inSalto", true);
    }

    //AUDIO
    public void PlayDamage()
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = damageSound;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    public void PlayStep(string stepType)
    {
        float vol = 0.5f;
        switch (stepType)
        {
            case "walking":
                vol = 0.3f;
                break;
            case "running":
                vol = 0.5f;
                break;
            case "walkToStop":
                vol = 0.3f;
                break;
            case "runToStop":
                vol = 0.5f;
                break;
        }
        audioSource.PlayOneShot(stepSounds[Random.Range(0, 4)], vol);
    }

    IEnumerator GetVita()
    {
        yield return new WaitForSecondsRealtime(0.5f);
       // VitaAttuale = healthbar.GetComponent<HealthBar>().GetCurrentHealth();
        VitaInizioLivello = VitaAttuale;
    }*/
}
