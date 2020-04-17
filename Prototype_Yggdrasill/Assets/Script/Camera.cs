using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public GameObject Player;
    public GameObject MainCamera;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    bool visual3D = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (!visual3D)
        {
            target = GameObject.FindGameObjectWithTag("Target2D").transform;
            Player.GetComponent<Giocatore>().CambiaVisuale(false);
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Target3D").transform;
            Player.GetComponent<Giocatore>().CambiaVisuale(true);
        }
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void ChangeinVisual3D()
    {
        visual3D = true;
        Debug.Log("Visuale in 3D");
        transform.Rotate(0, 45, 0);
    }

    public void ChangeinVisual2D()
    {
        visual3D = false;
        Debug.Log("Visuale in 2D");
        transform.Rotate(0, -45, 0);
    }

    public bool getVisual()
    {
        return visual3D;
    }

    
}
