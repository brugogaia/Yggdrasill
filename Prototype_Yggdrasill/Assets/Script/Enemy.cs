﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject MenuPausa;

    public Transform player;
    public Rigidbody rb;
    private Transform end;
    public Vector2 movement;
    [SerializeField] float speed;
    public bool little;
    [SerializeField] bool volante;
    [SerializeField] float MaxHealth;
    public float CurrentHealth;
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
    private int k = 0;

    private float waitTime = 1.5f;
    private float timer = 0.0f;

    private void Awake()
    {
        if (!little)
        {
            laserShotLine = GetComponentInChildren<LineRenderer>();
            spellLight = laserShotLine.gameObject.GetComponent<Light>();
            laserShotLine.enabled = false;
            spellLight.intensity = 0f;
        }
        end = GameObject.FindGameObjectWithTag("End").transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        ScaleDamage = MaxDamage - MinDamage;

        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody>();
        
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuPausa.GetComponent<MenuPausa>().pausa)
        {
            playerDead = player.GetComponent<Giocatore>().isDed();
            Vector3 direction = end.position - transform.position;
            direction.Normalize();
            movement = direction;
            timer += Time.deltaTime;
            if (!little)
            {
                if (!playerDead && timer >= waitTime && !shooting && !isDead && Vector3.Distance(transform.position, player.position) <= 70)
                {
                    timer = 0f;
                    ShotEffects();

                }
                else
                {
                    shooting = false;
                    laserShotLine.enabled = false;
                }



                spellLight.intensity = Mathf.Lerp(spellLight.intensity, 0f, fadeSpeed * Time.deltaTime);
            }
            else
            {

                /*if (colpito && timer > waitTime)
                {
                    altezzaSalto = 0;
                    transform.Translate(0, -transform.position.y+1, 0);
                    Debug.Log("sono nell if");
                }

                if(isDead && transform.position.y > 0) transform.Translate(0, -transform.position.y+1, 0);*/

            }
        }
            
        
        
    }

    private void FixedUpdate()
    {
        if(!isDead && !playerDead && !MenuPausa.GetComponent<MenuPausa>().pausa)   
            moveEnemy(movement);
        else if(playerDead && !little) this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
    void moveEnemy(Vector3 direction)
    {
        if (!little)
        {
            float distanza = Vector3.Distance(transform.position, player.position);
            if (distanza >= 40 && distanza <=100 && !volante)
                rb.MovePosition((Vector3)transform.position + (direction * speed * Time.deltaTime));
            else if (volante && distanza <=100)
            {
                Vector3 direccio = new Vector3(direction.x, 0, direction.z);
                rb.MovePosition((Vector3)transform.position + (direccio * speed * Time.deltaTime));
            }
        }    
        else
        {
            if(Vector3.Distance(transform.position, player.position) <= distanza )
            {
                float step = speed * Time.deltaTime; 
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(end.position.x, altezzaSalto, end.position.z), step);

            }

        }
    }

    void Shoot()
    {
        float FractionalDistance = (70 - Vector3.Distance(transform.position, player.position)) / 70;
        float damage = ScaleDamage * FractionalDistance + MinDamage;
        damage = damage / 2;
        player.GetComponent<Giocatore>().TakeDamage(damage);
    }

    void ShotEffects()
    {
        shooting = true;
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        int layerMask = 1 << 8;
        if (Physics.Raycast(transform.position, end.position - transform.position, Mathf.Infinity, layerMask))
        {
            laserShotLine.SetPosition(1, new Vector3(player.position.x,transform.position.y,player.position.z));
            Debug.Log("Colpito giocatore");
            Shoot();
        }
        else
        {
            //Debug.Log("Did not Hit");
            laserShotLine.SetPosition(1, new Vector3(end.position.x, transform.position.y, end.position.z));
        }

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

                if (!little)
                {
                    transform.Rotate(0, 0, -90);
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
                    

                transform.Translate(0, 0, 10);
                transform.gameObject.tag = "DeadEnemy";
                player.GetComponent<PlayerShooting>().StopShooting();
            }
        }

    }

    public void Cura()
    {
        CurrentHealth = CurrentHealth + 20f;
        if(CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.collider == player.GetComponent<Collider>() && little && !isDead)
        {
            player.GetComponent<Giocatore>().TakeDamage(10f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == player.GetComponent<Collider>() && little && !isDead)
        {
            /*altezzaSalto = 17f;
            timer = 0.0f;
            waitTime = 0.25f;*/
        }
    }

    public bool isLittle()
    {
        return little;
        
    }
}
