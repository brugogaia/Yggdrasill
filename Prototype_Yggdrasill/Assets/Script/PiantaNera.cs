using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiantaNera : MonoBehaviour
{
    public GameObject player;
    public bool isFlower;
    public bool treD;
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
        if (other.tag == "CollPlayer" && !isFlower)
        {
            Debug.Log("colpito player");
            if (!treD) player.GetComponent<Giocatore>().PiantaDamage();
            else player.GetComponent<PlayerMovement>().PiantaDamage();
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "CollPlayer" && isFlower)
        {
            Debug.Log("colpito player");
            if(!treD) player.GetComponent<Giocatore>().PiantaDamage();
            else player.GetComponent<PlayerMovement>().PiantaDamage();
            //GameObject.Destroy(this.gameObject);
        }
    }
}
