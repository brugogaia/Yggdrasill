using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string NomeScena;
    private GameObject MenuPausa;
    // Start is called before the first frame update
    void Start()
    {
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CollPlayer") MenuPausa.GetComponent<MenuPausa>().setNomeScena(NomeScena);
    }
}
