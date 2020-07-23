using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float MaxDamage = 20f;
    public float MinDamage = 10f;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    [SerializeField] LineRenderer laserShotLine;
    [SerializeField] Transform bacchetta;
    private Light spellLight;
    public Transform enemy;
    private float ScaleDamage;

    private bool shooting = false;

    private bool bacchettaScarica = false;

    private int distanza = 100;

    private GameObject Puu;

    [SerializeField] Material Blu;
    [SerializeField] Material Rosa;

    [SerializeField] Animator anim;

    private Transform Bacchetta;

    public AudioSource[] sounds;
    public AudioSource shotsound;


    private void Awake()
    {
        //laserShotLine = GetComponentInChildren<LineRenderer>();
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
        sounds = GetComponents<AudioSource>();
        shotsound = sounds[0];
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

                ShotEffects();
                shotsound.Play();
                

            }
            else if(Input.GetMouseButtonDown(1) && !bacchettaScarica && !shooting && Vector3.Distance(transform.position, enemy.position) <= distanza)
            { 
                Cura();
            }
            else
            {
                shooting = false;
                laserShotLine.enabled = false;
                anim.SetBool("spell", false);
            }
        }
        else
        {
            laserShotLine.enabled = false;
            anim.SetBool("spell", false);
        }


        spellLight.intensity = Mathf.Lerp(spellLight.intensity, 0f, fadeSpeed * Time.deltaTime);

    }

    void StopShoot()
    {
        
    }

    void Shoot()
    {
        
        shooting = true;
        float FractionalDistance = (distanza - Vector3.Distance(transform.position, enemy.position)) / distanza;
        float damage = ScaleDamage * FractionalDistance + MinDamage;
        enemy.GetComponent<Enemy>().Damage(damage);
        Bacchetta.GetComponent<Bacchetta>().HaStordito();
        laserShotLine.material = Blu;

        //Invoke("ShotEffects", 0.3f);
    }

    void ShotEffects()
    {
        anim.SetBool("spell", true);
        laserShotLine.SetPosition(0, bacchetta.position);
        laserShotLine.SetPosition(1, new Vector3(enemy.position.x,bacchetta.position.y,enemy.position.z));
        laserShotLine.enabled = true;
        spellLight.intensity = flashIntensity;
        Shoot();
    }

    void Cura()
    {
        shooting = true;
        float FractionalDistance = (distanza - Vector3.Distance(transform.position, enemy.position)) / distanza;
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
            //enemy1.Play();
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = null;
            anim.SetBool("spell", false);
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
        anim.SetBool("spell", false);
        Puu.GetComponent<Puu>().setNemico(enemy);
    }

    public void SetBacchetta(bool carica)
    {
        bacchettaScarica = carica;
    }
}
