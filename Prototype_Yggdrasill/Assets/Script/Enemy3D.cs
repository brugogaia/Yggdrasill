using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3D : MonoBehaviour
{
    private Transform player;
    private Rigidbody rb;
    private Vector3 movement;
    private float speed = 10f;
    private GameObject target;
    private GameObject PathTarget;

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

    private float altezzaSalto = 0f;

    private float waitTime = 1.5f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody>();
        PathTarget = GameObject.FindGameObjectWithTag("EnemyTarget1");
        target = PathTarget;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.LookRotation(target.transform.position, Vector3.forward);

            transform.LookAt(target.transform);
            this.transform.Rotate(-90, 0, 0);
            direction.Normalize();
            movement = direction;
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
        }
    }

    void Shoot()
    {
        float FractionalDistance = (70 - Vector3.Distance(transform.position, player.position)) / 70;
        float damage = ScaleDamage * FractionalDistance + MinDamage;
        damage = damage / 2;
        //Debug.Log(damage);
        player.GetComponent<PlayerMovement>().TakeDamage(damage);
    }

    void ShotEffects()
    {
        shooting = true;
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
            laserShotLine.SetPosition(1, new Vector3(player.position.x, transform.position.y, player.position.z));
            Debug.Log("Colpito giocatore");
            Shoot();
        
        laserShotLine.enabled = true;
        spellLight.intensity = flashIntensity;


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


                isDead = true;
                Debug.Log("Morto nemico");

                if (!little)
                {
                    if (volante)
                    {
                        transform.Translate(0, -20, 0);
                        Debug.Log("morto volante");
                    }
                }
                else
                {
                    transform.localScale += new Vector3(0, -2, 0);

                }

                transform.gameObject.tag = "DeadEnemy";
                player.GetComponent<PlayerShooting3D>().StopShooting();
            }
        }

    }
}
