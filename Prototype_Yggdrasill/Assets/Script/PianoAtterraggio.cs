using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoAtterraggio : MonoBehaviour
{
    public Animator anim;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == player.GetComponent<Collider>())
        {
            Debug.Log("Passato giocatore");
            anim.SetBool("Grounded", !anim.GetBool("Grounded"));
        }
    }
}
