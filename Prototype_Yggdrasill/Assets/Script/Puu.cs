using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puu : MonoBehaviour
{
    private GameObject MenuPausa;
    public Animator anim;

    public float MaxDamage = 10f;
    public float MinDamage = 5f;
    private float ScaleDamage;

    private int distanza = 70;

    private LineRenderer laserShotLine;
    private Light spellLight;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;
    private Transform enemy;
    private bool isDead = false;
    private bool shooting = false;

    public bool flying = false;

    private float waitTime = 0.3f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        laserShotLine = GetComponentInChildren<LineRenderer>();
        spellLight = laserShotLine.gameObject.GetComponent<Light>();

        laserShotLine.enabled = false;
        spellLight.intensity = 0f;
        ScaleDamage = MaxDamage - MinDamage;

        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuPausa.GetComponent<MenuPausa>().pausa)
        {
            timer += Time.deltaTime;
            if (enemy != null && !isDead)
            {
                if (timer >= waitTime && !shooting && Vector3.Distance(transform.position, enemy.position) <= 70 && !flying)
                {
                    timer = 0f;
                    Shoot();

                }
                else
                {
                    shooting = false;
                    laserShotLine.enabled = false;
                }
            }
            else
            {
                laserShotLine.enabled = false;
            }
            spellLight.intensity = Mathf.Lerp(spellLight.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
            
    }

    public void isFlying()
    {
        if (!isDead)
        {
            flying = true;
            anim.SetBool("Flying", true);
            transform.Translate(0, -3.5f, 5.5f);
        }
        
    }
    public void StopFlying()
    {
        if (flying)
        {
            anim.SetBool("Flying", false);
            flying = false;
            transform.Translate(0, 3.5f, -5.5f);
        }
    }

    public void setNemico(Transform nemico)
    {
        enemy = nemico;
    }

    private void Shoot()
    {
        shooting = true;
        float FractionalDistance = (distanza - Vector3.Distance(transform.position, enemy.position)) / distanza;
        float damage = ScaleDamage * FractionalDistance + MinDamage;
        enemy.GetComponent<Enemy>().Damage(damage);
        ShotEffects();
    }

    void ShotEffects()
    {
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        laserShotLine.SetPosition(1, enemy.position);
        laserShotLine.enabled = true;
        spellLight.intensity = flashIntensity;

    }
    public void Morte()
    {
        isDead = true;
    }
}
