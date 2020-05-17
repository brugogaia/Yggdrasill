using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zaino : MonoBehaviour
{
    public Canvas padre;
    public Canvas altroZaino;
    public Image red;
    public Image green;
    private GameObject MenuPausa;
    private bool toltoacchiappa = false;
    // Start is called before the first frame update
    void Start()
    {
        padre.enabled = false;
        altroZaino.enabled = false;
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
        red = GameObject.FindGameObjectWithTag("red").GetComponent<Image>();
        green = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (altroZaino.enabled) toltoacchiappa = true;
        if (padre.enabled) toltoacchiappa = false;
        if (!toltoacchiappa && !padre.enabled && Input.GetKeyDown(KeyCode.Q))
        {
            padre.enabled = true;
            red.enabled = false;
            green.enabled = false;
            MenuPausa.GetComponent<MenuPausa>().setPausa(true);
           
        }
        else if(!toltoacchiappa && padre.enabled && Input.GetKeyDown(KeyCode.Q))
        {
            red.enabled = true;
            green.enabled = true;
            MenuPausa.GetComponent<MenuPausa>().setPausa(false);
            padre.enabled = false;
        }
        else if(toltoacchiappa && !altroZaino.enabled && Input.GetKeyDown(KeyCode.Q))
        {
            altroZaino.enabled = true;
            red.enabled = false;
            green.enabled = false;
            MenuPausa.GetComponent<MenuPausa>().setPausa(true);

        }
        else if (toltoacchiappa && altroZaino.enabled && Input.GetKeyDown(KeyCode.Q))
        {
            red.enabled = true;
            green.enabled = true;
            MenuPausa.GetComponent<MenuPausa>().setPausa(false);
            altroZaino.enabled = false;
           
        }
    }
}
