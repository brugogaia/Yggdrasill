using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMorte : MonoBehaviour
{
    [SerializeField] string NomeScena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Image>().enabled && Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(NomeScena, LoadSceneMode.Single);
        }
    }
}
