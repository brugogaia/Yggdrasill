using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vibra_Zaino : MonoBehaviour
{
    private Image zaino;
    public GameObject Alice;
    private bool presaAlice = false;
    private float timer = 0.0f;
    private float waitime = 0.2f;
    private float wattime = 0.1f;
    private bool stop = false;
    private bool lampeggia = false;
    private Image bottone;
    private float a = 1f;
    private GameObject acchiappa;
    private bool scendi = true;
    // Start is called before the first frame update
    void Start()
    {
        zaino = GameObject.FindGameObjectWithTag("green").GetComponent<Image>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        bottone = GameObject.FindGameObjectWithTag("bottone_acchiappa").GetComponent<Image>();
        acchiappa = GameObject.FindGameObjectWithTag("acchiappasogni");
        timer = timer + Time.deltaTime;
        if (!stop && presaAlice && timer>=waitime)
        {
            timer = 0.0f;
            if(zaino.color.r == 1)
            {
                
                zaino.color = new Color(0.7f, 0f, 0f);

            }
            else
            {
                zaino.color = Color.white;
            }
        }
        if (presaAlice && Input.GetKeyDown(KeyCode.Q))
        {
            stop = true;
            lampeggia = true;
            timer = 0.0f;
        }

        if(stop && zaino.color.r == 0.7f)
        {
            zaino.color = Color.white;
        }

        if (a == 1) scendi = true;

        if (lampeggia && timer >= wattime)
        {
            timer = 0.0f;
            Debug.Log(a);
            if (a >= 0.2 && scendi)
            {
                a = a - 0.1f;
                scendi = true;
            }
            else
            {
                a = a + 0.1f;
                scendi = false;
            }
            
            bottone.color = new Color(1, 1, 1, a);
        }
        if (stop && acchiappa.GetComponent<MeshRenderer>().enabled)
        {
            lampeggia = false;
            bottone.color = new Color(1, 1, 1, 1);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == Alice.GetComponent<Collider>())
        {
            presaAlice = true;
            timer = 0.0f;
        }
    }
}
