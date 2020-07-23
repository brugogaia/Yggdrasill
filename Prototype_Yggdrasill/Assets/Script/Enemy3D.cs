using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3D : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;
    private Vector3 movement;
    private float speed = 15f;
    private GameObject target;
    public GameObject PathTarget;

    private GameObject MenuPausa;

    public bool little;
    [SerializeField] bool volante;
    [SerializeField] float MaxHealth;
    private float CurrentHealth;
    private float damage;
    public bool isDead = false;
    private float distanza = 100f;

    private bool playerDead = false;

    [SerializeField] float MaxDamage;
    [SerializeField] float MinDamage;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private LineRenderer laserShotLine;
    private Light spellLight;
    private float ScaleDamage;

    private bool shooting = false;

    private float waitTime = 1f;
    private float timer = 0.0f;

    public Animator anim;

    public AudioSource[] sounds;
    public AudioSource suono;
    int count = 1;
    public AudioSource morte;
    public AudioSource shotsound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody>();
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
        target = PathTarget;
        CurrentHealth = MaxHealth;

        laserShotLine = GetComponentInChildren<LineRenderer>();
        spellLight = laserShotLine.gameObject.GetComponent<Light>();
        laserShotLine.enabled = false;
        spellLight.intensity = 0f;

        ScaleDamage = MaxDamage - MinDamage;

        sounds = GetComponents<AudioSource>();
        suono = sounds[0];
        morte = sounds[1];
        shotsound = sounds[2];
    }

    // Update is called once per frame
    void Update()
    {
        playerDead = player.GetComponent<PlayerMovement>().isDed();
        if (!isDead && !playerDead && !MenuPausa.GetComponent<MenuPausa>().pausa)
        {
            timer += Time.deltaTime;
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.LookRotation(target.transform.position, Vector3.forward);

            transform.LookAt(target.transform);
            //this.transform.Rotate(-90, 0, 0);
            direction.Normalize();
            movement = direction;

            if (timer >= waitTime && !shooting && target==player.gameObject)
            {
                timer = 0f;
                anim.SetBool("spara", true);
                Invoke("ShotEffects", 0.5f);

            }
            else
            {
                shooting = false;
                laserShotLine.enabled = false;
                anim.SetBool("spara", false);
            }



            spellLight.intensity = Mathf.Lerp(spellLight.intensity, 0f, fadeSpeed * Time.deltaTime);
        }

    }
        
        

    private void FixedUpdate()
    {
        if(!isDead) moveEnemy(movement);
    }
    void moveEnemy(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Trovato player");
            target = player.gameObject;
            suono.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "CollPlayer")
        {
            player.GetComponent<PlayerMovement>().TakeDamage(100f);
        }
    }

    void Shoot()
    {
        float FractionalDistance = (70 - Vector3.Distance(transform.position, player.position)) / 70;
        float damage = ScaleDamage * FractionalDistance + MinDamage;
        damage = damage / 2;
        //Debug.Log(damage);
        player.GetComponent<PlayerMovement>().TakeDamage(damage);
        shotsound.Play();
    }

    void ShotEffects()
    {
        if (!isDead)
        {
            shooting = true;
            laserShotLine.SetPosition(0, laserShotLine.transform.position);
            laserShotLine.SetPosition(1, new Vector3(player.position.x, transform.position.y, player.position.z));
            Debug.Log("Colpito giocatore");
            laserShotLine.enabled = true;
            spellLight.intensity = flashIntensity;
            Shoot();
        }
        


    }

    public void Damage(float DamageAmount)
    {
        //Debug.Log("vita nemico: " + CurrentHealth);
        damage = DamageAmount;
        if (!isDead)
        {
            CurrentHealth = CurrentHealth - damage;
            //animationHit();

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;

                laserShotLine.enabled = false;
                spellLight.intensity = 0f;

                isDead = true;
                Debug.Log("Morto nemico");
                morte.Play();

                anim.SetBool("morto", true);

                transform.gameObject.tag = "DeadEnemy";
                player.GetComponent<PlayerShooting3D>().StopShooting();
            }
        }

    }
}
