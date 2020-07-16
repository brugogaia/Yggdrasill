using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private GameObject MenuPausa;
    private GameObject player;
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        MenuPausa = GameObject.FindGameObjectWithTag("MenuPausa");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuPausa.GetComponent<MenuPausa>().pausa && !player.GetComponent<PlayerMovement>().fermo && !player.GetComponent<PlayerMovement>().isDead) {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
            
    }
}
