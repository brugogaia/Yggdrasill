using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisioneTerreno : MonoBehaviour
{
    private GameObject player;
    public bool destro;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Ground")
        {
            if(destro) player.GetComponent<Giocatore>().setGroundDestro(true);
            else player.GetComponent<Giocatore>().setGroundSinistro(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            //Debug.Log("a terra");
            if (destro) player.GetComponent<Giocatore>().setGroundDestro(true);
            else player.GetComponent<Giocatore>().setGroundSinistro(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Ground")
        {
            //Debug.Log("non più a terra");
            if (destro) player.GetComponent<Giocatore>().setGroundDestro(false);
            else player.GetComponent<Giocatore>().setGroundSinistro(false);
        }
    }
}
