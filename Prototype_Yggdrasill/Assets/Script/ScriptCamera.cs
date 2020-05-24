﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    public Transform target;
    public GameObject Player;
    private GameObject healthbar;
    public float smoothTime = 1F;
    private Vector3 velocity = Vector3.zero;
    private bool visual3D = false;
    


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        healthbar = GameObject.FindGameObjectWithTag("HealthBar");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Debug.Log("visul3D camera " + visual3D);
        if (!visual3D)
        {
            target = GameObject.FindGameObjectWithTag("Target2D").transform;
            this.transform.right = Player.transform.right;
            
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Target3D").transform;
            this.transform.forward = Player.transform.right;
            

        }
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        // Smoothly move the camera towards that target position
        if(!healthbar.GetComponent<HealthBar>().isDead) transform.position = targetPosition;
    }

    public void ChangeinVisual3D()
    {
        visual3D = true;
        Player.GetComponent<Giocatore>().CambiaVisualein3D();
        //Debug.Log("Visuale in 3D");
        // transform.Rotate(0, 90, 0);
    }

    public void ChangeinVisual2D()
    {
        visual3D = false;
        Player.GetComponent<Giocatore>().CambiaVisualein2D();
        //Debug.Log("Visuale in 2D");
        //transform.Rotate(0, 90, 0);
    }

    

    
}
