using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento3D : MonoBehaviour
{
    private bool vis = false;
    private float velocita_verticale_camera = 1.0f;
    private float velocita_orizzontale_camera = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vis)
        {
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


            transform.Rotate(0, angolo_orizzontale_rotazione, 0);
            transform.Rotate(0, 0, angolo_verticale_rotazione);
        }
    }

    public void setVis(bool bol)
    {
        vis = bol;
    }
}
