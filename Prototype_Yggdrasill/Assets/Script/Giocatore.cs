using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giocatore : MonoBehaviour
{
    public float speed = 20.0f;
    public float rotationSpeed = 100.0f;
    public bool visual3D = false;

    private float velocita_verticale_camera = 1.0f;
    private float velocita_orizzontale_camera = 1.0f;

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
            float movimentoAvantiIndietro = Input.GetAxis("Vertical") * speed;
            float movimentoDxSx = Input.GetAxis("Horizontal") * speed;
            movimentoDxSx *= Time.deltaTime;
            transform.Translate(0, 0,-movimentoDxSx);
            movimentoAvantiIndietro *= Time.deltaTime;
            transform.Translate(movimentoAvantiIndietro, 0, 0);


            Vector2 mouseMovement = new Vector2();
            mouseMovement.x = Input.GetAxis("Mouse X");
            mouseMovement.y = Input.GetAxis("Mouse Y");

            float angolo_orizzontale_rotazione = 0.0f;
            float angolo_verticale_rotazione = 0.0f;
            if (mouseMovement.magnitude > 0.05)
            {
                //Rotazione asse verticale camera
                angolo_verticale_rotazione -= mouseMovement.y * this.velocita_verticale_camera;

                //Rotazione asse orizzontale camera
                angolo_orizzontale_rotazione = mouseMovement.x * velocita_orizzontale_camera;
            }


            this.transform.Rotate(0, angolo_orizzontale_rotazione, 0);
            this.transform.Rotate(0, 0, angolo_verticale_rotazione);
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
