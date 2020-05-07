using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    // DA POSIZIONARE SU GIOCATORE

    
    //[SerializeField] Animator animator;

    private void Start()
    {
        
    }

    

    void OnCollisionEnter(Collision collision){
        
        Debug.Log("Relative velcity of collision"+collision.relativeVelocity.magnitude);
        
        
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
