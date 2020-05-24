using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    // Start is called before the first frame update
    public bool pausa = false;
    [SerializeField] string NomeScena;
    private Canvas MenuMorte;
    public Canvas Zaino1;
    public Canvas Zaino2;

    public Image white;
    public Animator anim;
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    void Start()
    {
        MenuMorte = GameObject.FindGameObjectWithTag("MenuMorte").GetComponentInParent<Canvas>();
        MenuMorte.enabled = false;
        this.GetComponentInParent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (this.GetComponentInParent<Canvas>().enabled || MenuMorte.enabled || Zaino1.enabled || Zaino2.enabled)
        {
            
            pausa = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            
            pausa = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (!pausa && !MenuMorte.enabled && Input.GetKeyDown("escape"))
        {
            this.GetComponentInParent<Canvas>().enabled = true;

        }
        /*if (pausa) {
            if (Input.GetKeyDown(KeyCode.R))
            {
                pausa = false;
                this.GetComponent<Image>().enabled = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (Input.GetKeyDown(KeyCode.T)){
                SceneManager.LoadScene(NomeScena, LoadSceneMode.Single);
            }


        }*/
    }

    public string GetNomeScena()
    {
        return NomeScena;
    }

    public void setPausa(bool boolean)
    {
        pausa = boolean;
    }

    public void LoadScena()
    {
        SceneManager.LoadScene(NomeScena, LoadSceneMode.Single);
    }

    public void OpenMenu()
    {
        this.GetComponentInParent<Canvas>().enabled = false;
        MenuMorte.enabled = false;
        StartCoroutine(Fading());
        
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        anim1.SetBool("Fade", true);
        anim2.SetBool("Fade", true);
        anim3.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("MenuPrincipale", LoadSceneMode.Single);
        

    }
}
