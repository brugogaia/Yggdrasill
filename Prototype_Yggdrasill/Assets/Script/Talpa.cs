using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talpa : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CollPlayer")
        {
            if (other.tag == "CollPlayer")
            {
                player.GetComponent<Giocatore>().TakeDamage(100f);
            }
        }
    }
}
