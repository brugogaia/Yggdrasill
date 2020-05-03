using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Transform character;
    public Transform HBImage;
    // public Transform privot;
    //public Transform HBText;
    public float MaxHealth;
    public float CurrentHealth;

    private Vector3 ScaleChange;
    private float damage;
    public bool isDead = false;
    //[SerializeField] Animator animator;

    void Start()
    {

        //HBText = gameObject.GetComponentInChildren<Text>().transform;
        MaxHealth = HBImage.transform.localScale.x;
        CurrentHealth = MaxHealth;
   
        //if (GameObject.Find("Menu").GetComponent<Menu>().Riavviante()) SetCurrentHealth(GameObject.Find("Menu").GetComponent<Menu>().GetHealth());
    }

    public void FallDamage(float DamageAmount)
    {
        
        damage = DamageAmount / 100;
        //Debug.Log("danno: " + damage);
        CurrentHealth = CurrentHealth - damage;
        //animationHit();

        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        /*ScaleChange = new Vector3(CurrentHealth, HBImage.transform.localScale.y, HBImage.transform.localScale.z);
        HBImage.transform.localScale = ScaleChange;*/
        
        //CurrentHealth = Mathf.Round(CurrentHealth);
        //HBText.GetComponent<Text>().text = CurrentHealth.ToString()+"%";
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            /*ScaleChange = new Vector3(0, HBImage.transform.localScale.y, HBImage.transform.localScale.z);
            HBImage.transform.localScale = ScaleChange;*/

            
            //  Debug.Log("isShocked ---------->" + isShock);
            //if (isShock)
            //{
            //    setShock();
            //} else
            //{
            //    animationDeathHit();
            //}

            isDead = true;
            //animationDeathHit();

            //HBText.GetComponent<Text>().text = "0";
            GameObject.Destroy(character.gameObject);
        }
        HBImage.GetComponent<Image>().fillAmount = CurrentHealth;
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public void SetCurrentHealth(float health)
    {
        CurrentHealth = health;
        HBImage.GetComponent<Image>().fillAmount = CurrentHealth;
    }

    public void PiantaDamage()
    {
        float danno = 0.4f;
        CurrentHealth = CurrentHealth - danno;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            isDead = true;
            //animationDeathHit();

            //HBText.GetComponent<Text>().text = "0";
            GameObject.Destroy(character.gameObject);
        }
        HBImage.GetComponent<Image>().fillAmount = CurrentHealth;
    }


        public void Cura()
    {
        if(CurrentHealth < MaxHealth)
        {
            float cura = 0.2f;
            CurrentHealth = CurrentHealth + cura;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            HBImage.GetComponent<Image>().fillAmount = CurrentHealth;

        }
        
    }
    /*void animationHit()
    {
        if (player.GetComponent<Giocatore>().staPortandoOggetto()) player.GetComponent<Afferrare_2>().Drop();
        animator.SetTrigger("hit");
    }

    void animationDeathHit()
    {
        animator.SetTrigger("deathByFall");
        player.GetComponent<Giocatore>().setDeath();
        GameObject.FindGameObjectWithTag("CameraInterpolata").GetComponent<CameraInterpolata>().DeathImmobility();
        Invoke("OpenMenu",4f);
    }

    void OpenMenu()
    {
        GameObject.Find("Menu").GetComponent<Menu>().Death();
    }

    // public void setShock()
    // {
    //     animator.SetTrigger("deathByShock");
    // }
    //
    // public void isShocked()
    // {
    //     isShock = true;
    // }
    */
    bool isDed()
    {
        return isDead;
    }
}