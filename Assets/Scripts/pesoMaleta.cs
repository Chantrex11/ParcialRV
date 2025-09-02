using UnityEngine;
using TMPro;

public class PesoMaleta : MonoBehaviour
{
    [Header("Configuración de la maleta")]
    public float capacidadMaxima = 23f;
    public float pesoMaleta = 4f;

    [Header("Estado actual")]
    public float pesoObjetos = 0f;

    [Header("UI")]
    public TextMeshProUGUI textoPeso;

    [Header("Referencia a introMaleta")]
    public introMaleta introScript;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoError;
    public TextMeshProUGUI textoAdvertencia;


    private void Start()
    {
        ActualizarTexto();
        if (textoPeso != null)
            textoPeso.enabled = false;

    }

    private void Update()
    {
        if (introScript != null && textoPeso != null)
        {
            // Mostrar solo si la cámara de intro está activa
            textoPeso.enabled = introScript.camaraIntro.enabled;
        }
    }

    public bool AgregarObjeto(float pesoObjeto)
    {
        float nuevoPeso = pesoMaleta + pesoObjetos + pesoObjeto;

        if (nuevoPeso > capacidadMaxima)
        {
            if (audioSource != null && sonidoError != null)
                audioSource.PlayOneShot(sonidoError);
            return false;

        }
        pesoObjetos += pesoObjeto;
        ActualizarTexto();
        return true;
    }

    public float ObtenerPesoTotal()
    {
        return pesoMaleta + pesoObjetos;
    }

    private void ActualizarTexto()
    {
        if (textoPeso != null)
        {
            textoPeso.text = "Peso: " + ObtenerPesoTotal().ToString("F1") + " | " + capacidadMaxima + " kg";
        }
    }
    
    public void MostrarAdvertencia(string mensaje, float duracion = 2f)
    {
        if (textoAdvertencia != null)
        {
            textoAdvertencia.text = mensaje;
            textoAdvertencia.gameObject.SetActive(true);
            CancelInvoke("OcultarAdvertencia");
            Invoke("OcultarAdvertencia", duracion);
        }
    }

    private void OcultarAdvertencia()
    {
        textoAdvertencia.gameObject.SetActive(false);
    }
}
