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
        if (!visual3D) target = GameObject.FindGameObjectWithTag("Target2D").transform;
        else
        {
            target = GameObject.FindGameObjectWithTag("Target3D").transform;
            
        }
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void ChangeVisual(bool visuale3D)
    {
        visual3D = visuale3D;
        Debug.Log("Visuale cambiata");
        transform.Rotate(0, 45, 0);
    }

    public bool getVisual()
    {
        return visual3D;
    }
}
