using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuuHelp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Canvas>().enabled && Input.GetKeyDown(KeyCode.E))
        {
            this.GetComponent<Canvas>().enabled = false;
        }
    }
}
