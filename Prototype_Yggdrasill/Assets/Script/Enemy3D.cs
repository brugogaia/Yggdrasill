using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3D : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    private Vector3 movement;
    private float speed = 10f;
    private GameObject target;
    private GameObject PathTarget;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody>();
        PathTarget = GameObject.FindGameObjectWithTag("EnemyTarget1");
        target = PathTarget;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            
        Quaternion rotation = Quaternion.LookRotation(target.transform.position, Vector3.forward);
        
        transform.LookAt(target.transform);
        this.transform.Rotate(-90, 0, 0);
        direction.Normalize();
        movement = direction;
        
    }

    private void FixedUpdate()
    {
        moveEnemy(movement);
    }
    void moveEnemy(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Trovato player");
            target = player.gameObject;
        }
    }
}
