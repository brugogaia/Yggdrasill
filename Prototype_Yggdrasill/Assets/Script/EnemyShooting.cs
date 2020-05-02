using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float MaxDamage = 30f;
    public float MinDamage = 10f;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private LineRenderer laserShotLine;
    private Light spellLight;
    private Transform player;
    private float ScaleDamage;

    private bool shooting = false;

    private float waitTime = 2.0f;
    private float timer = 0.0f;

    private void Awake()
    {
        laserShotLine = GetComponentInChildren<LineRenderer>();
        spellLight = laserShotLine.gameObject.GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

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
        timer += Time.deltaTime;
        
        if (timer >= waitTime && !shooting && Vector3.Distance(transform.position, player.position)<=70)
        {
            timer = 0f;
            Shoot();

        }
        else
        {
            shooting = false;
            laserShotLine.enabled = false;
        }

        spellLight.intensity = Mathf.Lerp(spellLight.intensity, 0f, fadeSpeed * Time.deltaTime);

    }

    void Shoot()
    {
        shooting = true;
        float FractionalDistance = (70 - Vector3.Distance(transform.position, player.position)) / 70;
        float damage = ScaleDamage * FractionalDistance + MinDamage;
        damage = damage;
        player.GetComponent<FallDamage>().TakeDamage(damage);
        ShotEffects();
    }

    void ShotEffects()
    {
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        laserShotLine.SetPosition(1, player.position);
        laserShotLine.enabled = true;
        spellLight.intensity = flashIntensity;

    }

    
}
