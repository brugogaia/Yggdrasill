using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    // DA POSIZIONARE SU GIOCATORE

    public Transform healthbar;
    public Collider grabObj = null;
    public float Damage;
    //[SerializeField] Animator animator;


    public void TakeDamage(float DamageAmount){
        healthbar.GetComponent<HealthBar>().FallDamage(DamageAmount);
    }

    void OnCollisionEnter(Collision collision){
        
        Debug.Log("Relative velcity of collision"+collision.relativeVelocity.magnitude);
        if(collision.collider != grabObj && collision.relativeVelocity.magnitude > 12.5f)
        {
            //Debug.Log("Collisione!");
            Damage =collision.relativeVelocity.magnitude;
            TakeDamage(Damage);
            //setHit();
        }
    }

    public void Cura()
    {
        healthbar.GetComponent<HealthBar>().Cura();
    }

    /*public void SetGrabObj(Collider collider)
    {
        grabObj = collider;
    }

    public void setHit()
    {

        animator.SetTrigger("hit");
    }*/
}
