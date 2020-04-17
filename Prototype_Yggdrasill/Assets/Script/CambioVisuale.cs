using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioVisuale : MonoBehaviour
{
    //DA APPLICARE SUL PIANO POSTO TRA LE DUE SEQUENZE

    GameObject Camera;
    bool visuale3D;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>())
        {
            visuale3D = Camera.GetComponent<Camera>().getVisual();
            if (!visuale3D) visuale3D = true;
            Camera.GetComponent<Camera>().ChangeVisual(visuale3D);
        }
    }
}
