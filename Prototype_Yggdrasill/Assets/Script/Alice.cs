using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alice : MonoBehaviour
{
    private Canvas MenuPausa;
    private Canvas MenuMorte;

    private float speed = 20f;
    private float waitTime = 2f;
    private float timer = 2f;
    private Transform target;
    private Vector3 position;
    private bool presa = false;
    private Image UI;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("TargetAlice").transform;
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa").GetComponentInParent<Canvas>();
        MenuMorte = GameObject.FindGameObjectWithTag("MenuMorte").GetComponentInParent<Canvas>();
        UI = GameObject.FindGameObjectWithTag("UI_acchiappa").GetComponent<Image>();
        UI.enabled = false;
        this.GetComponent<Animator>().SetBool("walk", false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuPausa.enabled && !MenuMorte.enabled)
        {

            timer = timer + Time.deltaTime;
            if (!presa)
            {
                GetComponent<AudioSource>().mute = false;
                if (timer > waitTime)
                {
                    position = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                    
                    timer = 0.0f;
                }
                transform.Translate(position * Time.deltaTime * speed);
                if (position != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(position, Vector3.up);
                    transform.rotation = rotation;
                    this.GetComponent<Animator>().SetBool("walk", true);
                }
                else
                {
                    this.GetComponent<Animator>().SetBool("walk", false);
                    
                }
                
            }
            else
            {
                GetComponent<AudioSource>().mute = true;
                speed = 100f;
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
                /*transform.parent = target.transform;
                Object.Destroy(this.GetComponent<Rigidbody>());*/
            }
        }
        else
        {
            this.GetComponent<Animator>().SetBool("walk", false);
            GetComponent<AudioSource>().mute = true;
        }

            

    }

    public void setPresa(bool boolena)
    {
        presa = boolena;
        UI.enabled = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag!="Ground")
        this.GetComponent<Animator>().SetBool("walk", false);
    }
}
