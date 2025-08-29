using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoTuto : MonoBehaviour
{
    [SerializeField] private GameObject luzDialogo;
    [SerializeField] private GameObject dialogoPanel;
    [SerializeField] private TextMeshProUGUI dialogoTexto;
    [SerializeField, TextArea(4, 6)] private String[] lineasDialogo;


    private float tiempoEscritura = 0.05f;
    private bool isPlayerRange;
    private bool isDialogoActive;
    private int lineIndex;

    void Update()
    {
        if (isPlayerRange && Input.GetButtonDown("Fire1"))
        {
            if (!isDialogoActive)
            {
                empezarDialogo(); 
            }
            
        }
    }

    private void empezarDialogo()
    {
        isDialogoActive = true;
        dialogoPanel.SetActive(true);
        luzDialogo.SetActive(false);
        lineIndex = 0;
        StartCoroutine(EscribirTexto());
    }

    private IEnumerator EscribirTexto()
    {
        dialogoTexto.text = "";
        foreach (char letra in lineasDialogo[lineIndex])
        {
            dialogoTexto.text += letra;
            yield return new WaitForSeconds(tiempoEscritura);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luzDialogo.SetActive(true);
            isPlayerRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            luzDialogo.SetActive(false);
            isPlayerRange = false;
        }
    }
}
