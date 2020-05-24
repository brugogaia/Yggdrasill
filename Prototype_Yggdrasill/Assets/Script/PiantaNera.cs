using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiantaNera : MonoBehaviour
{
    public GameObject player;
    public bool isFlower;
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
        if (other == player.GetComponent<Collider>() && !isFlower)
        {
            player.GetComponent<Giocatore>().PiantaDamage();
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == player.GetComponent<Collider>() && isFlower)
        {
            player.GetComponent<Giocatore>().PiantaDamage();
            //GameObject.Destroy(this.gameObject);
        }
    }
}
