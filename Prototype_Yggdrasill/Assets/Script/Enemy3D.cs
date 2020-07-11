using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3D : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public Vector3 movement;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        
        this.transform.Rotate(0, angle, 0);
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
}
