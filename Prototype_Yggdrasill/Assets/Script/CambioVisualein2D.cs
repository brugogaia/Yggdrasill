using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioVisualein2D : MonoBehaviour
{
    //DA APPLICARE SUL PIANO POSTO TRA LE DUE SEQUENZE

    GameObject Camera;
    

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
            
            Debug.Log("TRIGGER TO 2D");
        }
    }
}
