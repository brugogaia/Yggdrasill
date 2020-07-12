using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacchetta : MonoBehaviour
{
    private float MaxIntensity;
    private float CurrentIntensity;
    private bool scarica = false;
    //private bool inricarica = false; //DA USARE SE DEVE RICARICARSI GRADUALMENTE
    private float RechargeTime = 10f;
    private float timer = 0.0f;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        MaxIntensity = transform.GetComponent<Light>().intensity;
        CurrentIntensity = MaxIntensity;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        if (scarica && timer > RechargeTime && !player.GetComponent<Giocatore>().isDed())
        {
            CurrentIntensity = MaxIntensity;
            scarica = false;
        }
        //player.GetComponent<PlayerShooting>().SetBacchetta(scarica);
        transform.GetComponent<Light>().intensity = CurrentIntensity;
    }

    public void HaStordito()
    {
        CurrentIntensity = CurrentIntensity - 0.05f;
        ControllaCarica();
    }

    public void HaCurato()
    {
        CurrentIntensity = CurrentIntensity - 0.2f;
        ControllaCarica();
    }

    private void ControllaCarica()
    {
        if (CurrentIntensity <= 0)
        {
            scarica = true;
            timer = 0.0f;
        }
        
    }

    private void ControllaMax()
    {
        if (CurrentIntensity >= MaxIntensity)
        {
            CurrentIntensity = MaxIntensity;
        }
    }
}
