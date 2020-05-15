using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_acchiappa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Image>().enabled && Input.GetKeyDown(KeyCode.E))
        {
            this.GetComponent<Image>().enabled = false;
        }
    }
}
