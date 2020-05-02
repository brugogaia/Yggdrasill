using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;

    private float damage;
    public bool isDead = false;
    //[SerializeField] Animator animator;

    void Start()
    {

        MaxHealth = 100f;
        CurrentHealth = MaxHealth;

       
    }

    public void Damage(float DamageAmount)
    {
        //Debug.Log("vita nemico: " + CurrentHealth);
        damage = DamageAmount;

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

            this.transform.position = this.transform.position + Vector3.up*-1000;
        }
        
    }

}
