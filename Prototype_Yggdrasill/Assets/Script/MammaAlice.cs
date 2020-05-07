using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MammaAlice : MonoBehaviour
{
    public Transform player;
    [SerializeField] Image UI_Image;
    private bool arrivato = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UI_Image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (arrivato && Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Labyrinth", LoadSceneMode.Single);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == player.GetComponent<Collider>())
        {
            UI_Image.enabled = true;
            arrivato = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            UI_Image.enabled = false;
            arrivato = false;
        }
    }
}
