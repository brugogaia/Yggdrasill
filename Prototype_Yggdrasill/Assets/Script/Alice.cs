using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alice : MonoBehaviour
{
    private float speed = 5f;
    private float waitTime = 2f;
    private float timer = 2f;
    private Transform player;
    private Transform target;
    private Vector3 position;
    private bool presa = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("TargetAlice").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (!presa)
        {
            if (timer > waitTime)
            {
                position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));

                timer = 0.0f;
            }
            transform.Translate(position * Time.deltaTime * speed);
        }
        else
        {
            speed = 10f;
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
            /*transform.parent = target.transform;
            Object.Destroy(this.GetComponent<Rigidbody>());*/
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == player.GetComponent<Collider>())
        {
            presa = true;
            this.GetComponent<AudioSource>().enabled = false;
        }
    }
}
