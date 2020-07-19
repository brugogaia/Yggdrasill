using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    private Transform target;
    private GameObject Player;
    private GameObject healthbar;
    public float smoothTime = 1F;
    private Vector3 velocity = Vector3.zero;

    


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        healthbar = GameObject.FindGameObjectWithTag("HealthBar");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        target = GameObject.FindGameObjectWithTag("Target2D").transform;
    }

    void Update()
    {
        //Debug.Log("visul3D camera " + ricognizione);

        this.transform.right = Player.transform.right;
            
        
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        // Smoothly move the camera towards that target position
        if(!healthbar.GetComponent<HealthBar>().isDead) transform.position = targetPosition;
    }

    public void GetTarget(Transform target_camera)
    {
        target = target_camera;
    }

    
    

    
}
