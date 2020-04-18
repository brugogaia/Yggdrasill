using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public GameObject Player;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private bool visual3D = false;
    


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

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
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
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
