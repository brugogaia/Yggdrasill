using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float MaxDamage = 20f;
    public float MinDamage = 10f;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private LineRenderer laserShotLine;
    private Light spellLight;
    private Transform enemy;
    private float ScaleDamage;

    private bool shooting = false;

    private bool bacchettaScarica = false;

    private int distanza = 70;

    private GameObject Puu;

    [SerializeField] Material Blu;
    [SerializeField] Material Rosa;

    private Transform Bacchetta;

    private void Awake()
    {
        laserShotLine = GetComponentInChildren<LineRenderer>();
        spellLight = laserShotLine.gameObject.GetComponent<Light>();
        Puu = GameObject.FindGameObjectWithTag("Puu");
        Bacchetta = GameObject.FindGameObjectWithTag("Bacchetta").transform;

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
        bool isDead = this.GetComponent<Giocatore>().isDed();
        if (isDead) Puu.GetComponent<Puu>().setNemico(null);
        if (enemy != null && !isDead)
        {
            if (Input.GetMouseButtonDown(0) && !bacchettaScarica && !shooting && Vector3.Distance(transform.position, enemy.position) <= distanza)
            {

                Shoot();

            }
            else if(Input.GetMouseButtonDown(1) && !bacchettaScarica && !shooting && Vector3.Distance(transform.position, enemy.position) <= distanza)
            { 
                Cura();
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

    void Shoot()
    {
        shooting = true;
        float FractionalDistance = (distanza - Vector3.Distance(transform.position, enemy.position)) / distanza;
        float damage = ScaleDamage * FractionalDistance + MinDamage;
        enemy.GetComponent<Enemy>().Damage(damage);
        Bacchetta.GetComponent<Bacchetta>().HaStordito();
        laserShotLine.material = Blu;
        ShotEffects();
    }

    void ShotEffects()
    {
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        laserShotLine.SetPosition(1, enemy.position);
        laserShotLine.enabled = true;
        spellLight.intensity = flashIntensity;

    }

    void Cura()
    {
        shooting = true;
        float FractionalDistance = (distanza - Vector3.Distance(transform.position, enemy.position)) / distanza;
        enemy.GetComponent<Enemy>().Cura();
        Bacchetta.GetComponent<Bacchetta>().HaCurato();
        laserShotLine.material = Rosa;
        ShotEffects();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.transform;
            Puu.GetComponent<Puu>().setNemico(enemy);
            Debug.Log("trovato nemico");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.transform;
            Puu.GetComponent<Puu>().setNemico(enemy);
            //Debug.Log("trovato nemico");
        }
    }

    public void setDistanza3D()
    {
        distanza = 120;
    }

    public void resetDistanza2D()
    {
        distanza = 70;
    }

    public void StopShooting()
    {
        enemy = null;
        Puu.GetComponent<Puu>().setNemico(enemy);
    }

    public void SetBacchetta(bool carica)
    {
        bacchettaScarica = carica;
    }
}
