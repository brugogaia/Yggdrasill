using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giocatore : MonoBehaviour
{
    public float speed = 10000f;
    public float rotationSpeed = 100.0f;
    private bool vis3D = false;
    private bool isGrounded = true;

    private Quaternion rotIniziale;

    private float velocita_verticale_camera = 1.0f;
    private float velocita_orizzontale_camera = 1.0f;

    private GameObject Puu;

    private void Start()
    {
        rotIniziale = transform.rotation;
        Puu = GameObject.FindGameObjectWithTag("Puu");
    }

    void Update()
    {
        Debug.Log("is grounded " + isGrounded);
        if (Input.GetKeyDown("space") && isGrounded)
        {
            Jump();
        }
        if (!isGrounded)
        {
            Atterra();
        }

        if(Input.GetKey(KeyCode.F) && !isGrounded )
        {
            Fly();
        }
        else
        {
            Puu.GetComponent<Puu>().StopFlying();
        }
        speed = 30f;
        //Debug.Log("k giocatore = " + k);
        if (!vis3D)
        {
            
            float movimentoOrizzontale = Input.GetAxis("Horizontal") * Time.deltaTime * speed ;
            Debug.Log(movimentoOrizzontale);
            transform.Translate(movimentoOrizzontale, 0, 0);
            
        }
        else
        {
            
            float movimentoAvantiIndietro = Input.GetAxis("Vertical") * speed;
            float movimentoDxSx = Input.GetAxis("Horizontal") * speed;
            movimentoDxSx *= Time.deltaTime;
            movimentoAvantiIndietro *= Time.deltaTime;
            Vector3 AD = transform.right*movimentoAvantiIndietro;
            Vector3 DxSx = transform.forward * -movimentoDxSx;
            transform.position = transform.position + AD + DxSx;
            


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
            //transform.Rotate(0, 0, angolo_verticale_rotazione);
        }
        
        
    }

    private void Jump()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 12f,0f), ForceMode.Impulse);
    }

    private void Atterra()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -0.5f, 0f), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("sto collidendo con " + collision.collider);
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("sto collidendo con " + collision.collider);
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("non sto piu collidendo con " + collision.collider);
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void Fly()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0.3f, 0f), ForceMode.Impulse);
        Puu.GetComponent<Puu>().isFlying();
    }

    public void CambiaVisualein3D()
    {
        vis3D = true;
        Debug.Log("Giocatore in 3D, vis3D vale "+vis3D);
        this.GetComponent<PlayerShooting>().setDistanza3D();
        
    }

    public void CambiaVisualein2D()
    {
        vis3D = false;
        this.GetComponent<PlayerShooting>().resetDistanza2D();
        
        if (transform.rotation != rotIniziale)
        {
            transform.rotation = rotIniziale;
            Debug.Log("sono nell if");
        }
    }

}
