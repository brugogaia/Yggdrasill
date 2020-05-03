﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puu : MonoBehaviour
{
    [SerializeField] Transform target;

    public float MaxDamage = 15f;
    public float MinDamage = 10f;
    private float ScaleDamage;

    private int distanza = 70;

    private LineRenderer laserShotLine;
    private Light spellLight;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;
    private Transform enemy;
    private bool shooting = false;

    private float waitTime = 0.1f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        laserShotLine = GetComponentInChildren<LineRenderer>();
        spellLight = laserShotLine.gameObject.GetComponent<Light>();

        laserShotLine.enabled = false;
        spellLight.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        this.GetComponent<Rigidbody>().velocity = (target.position - this.transform.position) * 80;
        if (enemy != null)
        {
            if (timer >= waitTime && !shooting && Vector3.Distance(transform.position, enemy.position) <= 70)
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
        spellLight.intensity = Mathf.Lerp(spellLight.intensity, 0f, fadeSpeed * Time.deltaTime);
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
}
