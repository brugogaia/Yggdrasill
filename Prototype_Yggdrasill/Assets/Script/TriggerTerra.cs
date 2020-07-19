using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTerra : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!anim.GetBool("Flying") && other.tag == "Ground")
        {
            player.GetComponent<Giocatore>().setGroundCentrale(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!anim.GetBool("Flying") && other.tag == "Ground")
        {
            player.GetComponent<Giocatore>().setGroundCentrale(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            player.GetComponent<Giocatore>().setGroundCentrale(false);
        }
    }
}
