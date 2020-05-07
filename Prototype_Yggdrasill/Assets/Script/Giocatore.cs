﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giocatore : MonoBehaviour
{
    public float speed = 50f;
    public float rotationSpeed = 100.0f;
    private bool vis3D = false;
    private bool isGrounded = true;
    private bool isDead = false;
    private Quaternion rotIniziale;

    private float velocita_verticale_camera = 1.0f;
    private float velocita_orizzontale_camera = 1.0f;

    private GameObject Puu;
    public Transform healthbar;

    public float Damage;

    private float waitTime = 0.2f;
    private float timer = 0.0f;

    private int k = 0;

    private void Start()
    {
        rotIniziale = transform.rotation;
        Puu = GameObject.FindGameObjectWithTag("Puu");
    }

    void Update()
    {
        isDead = healthbar.GetComponent<HealthBar>().isDed();
        timer = timer + Time.deltaTime;
        //Debug.Log("is grounded " + isGrounded);
        if (Input.GetKeyDown("space") && isGrounded &&!isDead)
        {
            Jump();
        }
        if (!isGrounded)
        {
            Atterra();
        }

        if(Input.GetKey(KeyCode.F) && !isGrounded && !isDead)
        {
            Fly();
            if (k == 0)
            {
                Puu.GetComponent<Puu>().isFlying();
                k++;
            }
        }
        if (Input.GetKeyUp(KeyCode.F) && !isGrounded && !isDead)
        {
            Puu.GetComponent<Puu>().StopFlying();
            k = 0;
        }
        speed = 50f;
        //Debug.Log("k giocatore = " + k);
        if (!vis3D)
        {
            
            float movimentoOrizzontale = Input.GetAxis("Horizontal") * Time.deltaTime * speed ;
            //Debug.Log(movimentoOrizzontale);
            if(!isDead) transform.Translate(movimentoOrizzontale, 0, 0);
            
        }
        else
        {
            
            float movimentoAvantiIndietro = Input.GetAxis("Vertical") * speed;
            float movimentoDxSx = Input.GetAxis("Horizontal") * speed;
            movimentoDxSx *= Time.deltaTime;
            movimentoAvantiIndietro *= Time.deltaTime;
            Vector3 AD = transform.right*movimentoAvantiIndietro;
            Vector3 DxSx = transform.forward * -movimentoDxSx;
            if (!isDead)  transform.position = transform.position + AD + DxSx;
            


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

    private void Jump()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 20f,0f), ForceMode.Impulse);
    }

    private void Atterra()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -0.5f, 0f), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("sto collidendo con " + collision.collider);
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.collider != Puu.GetComponent<Collider>() && collision.relativeVelocity.magnitude > 40f)
        {
            //Debug.Log("Collisione!");
            Damage = collision.relativeVelocity.magnitude;
            Damage = Damage / 10;
            TakeDamage(Damage);
            //setHit();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("sto collidendo con " + collision.collider);
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            Puu.GetComponent<Puu>().StopFlying();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("non sto piu collidendo con " + collision.collider);
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void Fly()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0.22f, 0f), ForceMode.Impulse);
    }

    public void CambiaVisualein3D()
    {
        vis3D = true;
        Debug.Log("Giocatore in 3D, vis3D vale "+vis3D);
        this.GetComponent<PlayerShooting>().setDistanza3D();
        
    }

    public void CambiaVisualein2D()
    {
        vis3D = false;
        this.GetComponent<PlayerShooting>().resetDistanza2D();
        
        if (transform.rotation != rotIniziale)
        {
            transform.rotation = rotIniziale;
            Debug.Log("sono nell if");
        }
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
}
