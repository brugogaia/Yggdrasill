using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zaino : MonoBehaviour
{
    public Image red;
    public Image green;
    private GameObject MenuPausa;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().enabled = false;
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<Image>().enabled && Input.GetKeyDown(KeyCode.Q))
        {
            this.GetComponent<Image>().enabled = true;
            red.enabled = false;
            green.enabled = false;
            MenuPausa.GetComponent<MenuPausa>().setPausa(true);
        }
        else if(this.GetComponent<Image>().enabled && Input.GetKeyDown(KeyCode.Q))
        {
            this.GetComponent<Image>().enabled = false;
            red.enabled = true;
            green.enabled = true;
            MenuPausa.GetComponent<MenuPausa>().setPausa(false);
        }
    }
}
