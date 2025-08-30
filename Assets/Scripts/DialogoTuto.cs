using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoTuto : MonoBehaviour
{
    [SerializeField] private GameObject luzDialogo;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject dialogoPanel;
    [SerializeField] private TextMeshProUGUI dialogoTexto;
    [SerializeField] private float tiempoEscritura;
    [SerializeField] private AudioClip sonidoEscritura;
    [SerializeField, TextArea(4, 6)] private String[] lineasDialogo;
    

    private bool isPlayerRange;
    private bool isDialogoActive;
   private int lineIndex;
   private AudioSource audio;


    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = sonidoEscritura;
    }

    void Update()
    {
        if (isPlayerRange && Input.GetButtonDown("Fire1"))
        {
            if (!isDialogoActive)
            {
                empezarDialogo();
            }
            else
            {
                if (dialogoTexto.text == lineasDialogo[lineIndex])
                {
                    siguienteDialogo();
                }
                else
                {
                    StopAllCoroutines();
                    dialogoTexto.text = lineasDialogo[lineIndex];
                }
            }
        }
    }

    private void empezarDialogo()
    {
        isDialogoActive = true;
        dialogoPanel.SetActive(true);
        luzDialogo.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f; // Pausa el juego
        StartCoroutine(EscribirTexto());
    }

    private IEnumerator EscribirTexto()
    {
        dialogoTexto.text = "";
        foreach (char letra in lineasDialogo[lineIndex])
        {
            audio.Play();
            dialogoTexto.text += letra;
            yield return new WaitForSecondsRealtime(tiempoEscritura);
        }
    }

    private void siguienteDialogo()
    {
        lineIndex++;
        if (lineIndex < lineasDialogo.Length)
        {
            StartCoroutine(EscribirTexto());
        }
        else
        {
            isDialogoActive = false;
            dialogoPanel.SetActive(false);
            luzDialogo.SetActive(true);
            Time.timeScale = 1f; // Reanuda el juego
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luzDialogo.SetActive(false);
            isPlayerRange = true;
            Canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luzDialogo.SetActive(true);
            isPlayerRange = false;
            Canvas.SetActive(false);
        }
    }
}
