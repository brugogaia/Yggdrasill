using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OrologioLoadScene : MonoBehaviour
{
    public Canvas canvas;
    public Image white;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canvas.enabled && Input.GetKeyDown(KeyCode.W))
        {
            canvas.enabled = false;
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("RientroLabirinto", LoadSceneMode.Single);
        //DontDestroyOnLoad(Canvas);

    }
}
