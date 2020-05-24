using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Affogamento : MonoBehaviour
{

    private GameObject player;
    private GameObject healthbar;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthbar = GameObject.FindGameObjectWithTag("HealthBar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other == player.GetComponent<Collider>())
        {

            healthbar.GetComponent<HealthBar>().Affoga();
        }
    }
}
