using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giocatore : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    void Update()
    {

        float movement3D = Input.GetAxis("Vertical") * speed;
        float movement2D = Input.GetAxis("Horizontal") * speed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        //movement3D *= Time.deltaTime;
        movement2D *= Time.deltaTime;

        // Move translation along the object's z-axis
        //transform.Translate(0, 0, movement3D);

        // Rotate around our y-axis
        transform.Translate(movement2D, 0, 0);
    }
}
