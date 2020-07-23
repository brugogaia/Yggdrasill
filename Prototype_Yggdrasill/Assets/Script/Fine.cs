using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fine : MonoBehaviour
{
    public Image white;
    public Animator anim;

    private float waitTime = 5f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= waitTime) OpenMenu();
    }


    public void OpenMenu()
    {
        Debug.Log("openMenu");
        this.GetComponentInParent<Canvas>().enabled = false;
        StartCoroutine(Fading("MenuPrincipale"));

    }

    IEnumerator Fading(string nomeScena)
    {
        anim.SetBool("Fade", true);
        //if(anim1!=null) anim1.SetBool("Fade", true);
        //if(anim2!=null) anim2.SetBool("Fade", true);
        //anim3.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene(nomeScena, LoadSceneMode.Single);


    }
}
