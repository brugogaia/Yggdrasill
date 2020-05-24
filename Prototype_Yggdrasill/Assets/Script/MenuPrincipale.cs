using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipale : MonoBehaviour
{
    public string NomeScena;
    public Canvas MenuPrinc;
    public Canvas Options;
    public Canvas Credits;

    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;
    public Animator anim5;
    public Image white;
    // Start is called before the first frame update
    void Start()
    {
        MenuPrinc.enabled = true;
        Options.enabled = false;
        Credits.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScena()
    {
        StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        anim1.SetBool("Fade", true);
        anim2.SetBool("Fade", true);
        anim3.SetBool("Fade", true);
        anim4.SetBool("Fade", true);
        anim5.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene(NomeScena, LoadSceneMode.Single);

    }

    public void OpenOptions()
    {
        MenuPrinc.enabled = false;
        Options.enabled = true;
    }

    public void CloseOptions()
    {
        MenuPrinc.enabled = true;
        Options.enabled = false;
    }

    public void OpenCredits()
    {
        MenuPrinc.enabled = false;
        Credits.enabled = true;
    }

    public void CloseCredits()
    {
        MenuPrinc.enabled = true;
        Credits.enabled = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

}
