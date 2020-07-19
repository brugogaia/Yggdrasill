using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 0.5f;
    private GameObject MenuPausa;
    public bool fermo = false;

    public bool isDead = false;

    private Transform healthbar;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
        healthbar = GameObject.FindGameObjectWithTag("HealthBar").transform;
        healthbar.GetComponent<HealthBar>().setTreD(true);
    }

    // Update is called once per frame
    void Update()
    {
        isDead = healthbar.GetComponent<HealthBar>().isDed();
        if (!MenuPausa.GetComponent<MenuPausa>().pausa && !fermo && !isDead)
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            if (x == 0 && z == 0) anim.SetBool("running", false);
            else anim.SetBool("running", true);
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
        }
        else
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

            
    }

    public void Staifermo()
    {
        fermo = true;
        anim.SetBool("running", false);
    }

    public void Muoviti()
    {
        fermo = false;
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
