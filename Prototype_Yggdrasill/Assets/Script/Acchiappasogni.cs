using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Acchiappasogni : MonoBehaviour
{
    public GameObject Alice;
    private bool attivo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<MeshRenderer>().enabled) attivo = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == Alice.GetComponent<Collider>() && attivo)
        {
            Alice.GetComponent<Alice>().setPresa(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == Alice.GetComponent<Collider>() && attivo)
        {
            Alice.GetComponent<Alice>().setPresa(true);
        }
    }
}
