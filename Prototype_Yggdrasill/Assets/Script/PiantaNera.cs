using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiantaNera : MonoBehaviour
{
    public GameObject player;
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
        if (other == player.GetComponent<Collider>())
        {
            player.GetComponent<FallDamage>().PiantaDamage();
            GameObject.Destroy(this.gameObject);
        }
    }
}
