using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambioScenaFinale : MonoBehaviour
{

    public Image white;
    public Animator anim;
    private GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "CollPlayer")
        {
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        Debug.Log("sono nella carota");
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => white.color.a == 1);
        SceneManager.LoadScene("Fine", LoadSceneMode.Single);
        Destroy(Canvas);
    }
}
