using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float MaxDamage = 30f;
    public float MinDamage = 10f;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private LineRenderer laserShotLine;
    private Light spellLight;
    private Transform enemy;
    private float ScaleDamage;

    private bool shooting = false;


    private void Awake()
    {
        laserShotLine = GetComponentInChildren<LineRenderer>();
        spellLight = laserShotLine.gameObject.GetComponent<Light>();

        laserShotLine.enabled = false;
        spellLight.intensity = 0f;

        ScaleDamage = MaxDamage - MinDamage;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(enemy != null)
        {
            if (Input.GetKeyDown(KeyCode.P) && !shooting && Vector3.Distance(transform.position, enemy.position) <= 70)
            {

                Shoot();

            }
            else
            {
                shooting = false;
                laserShotLine.enabled = false;
            }
        }
        

        spellLight.intensity = Mathf.Lerp(spellLight.intensity, 0f, fadeSpeed * Time.deltaTime);

    }

    void Shoot()
    {
        shooting = true;
        float FractionalDistance = (100 - Vector3.Distance(transform.position, enemy.position)) / 100;
        float damage = ScaleDamage * FractionalDistance + MinDamage;
        enemy.GetComponent<EnemyLife>().Damage(damage);
        ShotEffects();
    }

    void ShotEffects()
    {
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        laserShotLine.SetPosition(1, enemy.position);
        laserShotLine.enabled = true;
        spellLight.intensity = flashIntensity;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemy = other.transform;
            Debug.Log("trovato nemico");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = null;
        }
    }
}
