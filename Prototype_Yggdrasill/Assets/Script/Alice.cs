using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alice : MonoBehaviour
{
    private GameObject MenuPausa;

    private float speed = 10f;
    private float waitTime = 2f;
    private float timer = 2f;
    private Transform player;
    private Transform target;
    private Vector3 position;
    private bool presa = false;
    private Image UI;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("TargetAlice").transform;
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
        UI = GameObject.FindGameObjectWithTag("UI_acchiappa").GetComponent<Image>();
        UI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuPausa.GetComponent<MenuPausa>().pausa)
        {
            GetComponent<AudioSource>().mute = false;

            timer = timer + Time.deltaTime;
            if (!presa)
            {
                if (timer > waitTime)
                {
                    position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));

                    timer = 0.0f;
                }
                transform.Translate(position * Time.deltaTime * speed);
            }
            else
            {
                speed = 100f;
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
                /*transform.parent = target.transform;
                Object.Destroy(this.GetComponent<Rigidbody>());*/
            }
        }
        else
        {
            
        }

            

    }

    public void setPresa(bool boolena)
    {
        presa = boolena;
        UI.enabled = true;
    }
    
}
