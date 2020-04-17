using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giocatore : MonoBehaviour
{
    public float speed = 20.0f;
    public float rotationSpeed = 100.0f;
    public bool visual3D = false;

    void Update()
    {
        if (!visual3D)
        {
            float movimentoOrizzontale = Input.GetAxis("Horizontal") * speed;
            movimentoOrizzontale *= Time.deltaTime;
            transform.Translate(movimentoOrizzontale, 0, 0);
        }
        else
        {
            float movimentoOrizzontale = Input.GetAxis("Vertical") * speed;
            float movimentoVerticale = Input.GetAxis("Horizontal") * speed;
            movimentoVerticale *= Time.deltaTime;
            transform.Translate(0, 0,-movimentoVerticale);
            movimentoOrizzontale *= Time.deltaTime;
            transform.Translate(movimentoOrizzontale, 0, 0);
        }
        
        

        // Make it move 10 meters per second instead of 10 meters per frame...
        //movement3D *= Time.deltaTime;
        

        // Move translation along the object's z-axis
        //transform.Translate(0, 0, movement3D);

        // Rotate around our y-axis
        
    }

    public void CambiaVisuale(bool nuovaVisuale)
    {
        visual3D = nuovaVisuale;
    }
}
