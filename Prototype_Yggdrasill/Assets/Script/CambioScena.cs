using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambioScena : MonoBehaviour
{
    public Image white;
    public Animator anim;
    [SerializeField] Image UI_Image_Aiuta;
    private GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UI_Image_Aiuta.enabled = false;
            StartCoroutine(Fading());


        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            UI_Image_Aiuta.enabled = false;
            //Carichiamo scena labirinto con Alice invisibile che si sentono solo i passi
        }
    }


    IEnumerator Fading()
    {
        Debug.Log("sono nella carota");
        anim.SetBool("Fadee", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("Labyrinth", LoadSceneMode.Single);
        DontDestroyOnLoad(Canvas);

    }
}
