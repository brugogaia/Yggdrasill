﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    // Start is called before the first frame update
    public bool pausa = false;
    [SerializeField] string NomeScena;
    private Image MenuMorte;
    void Start()
    {
        MenuMorte = GameObject.FindGameObjectWithTag("MenuMorte").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pausa && !MenuMorte.enabled && Input.GetKeyDown("escape"))
        {
            this.GetComponent<Image>().enabled = true;
            pausa = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (pausa) {
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


        }
    }

    public string GetNomeScena()
    {
        return NomeScena;
    }

    public void setPausa(bool boolean)
    {
        pausa = boolean;
    }
}
