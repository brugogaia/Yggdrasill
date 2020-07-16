﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiantaCurativa : MonoBehaviour
{
    public bool treD;
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
        if(other.CompareTag("CollPlayer"))
        {
            if (!treD) player.GetComponent<Giocatore>().Cura();
            else player.GetComponent<PlayerMovement>().Cura();
            GameObject.Destroy(this.gameObject);
        }
    }
}
