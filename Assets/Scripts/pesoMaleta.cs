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
    public introMaleta introScript; // Arrastra tu objeto con el script introMaleta

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
        if (pesoMaleta + pesoObjetos + pesoObjeto <= capacidadMaxima)
        {
            pesoObjetos += pesoObjeto;
            ActualizarTexto();
            return true;
        }
        else
        {
            return false; 
        }
    }

    public float ObtenerPesoTotal()
    {
        return pesoMaleta + pesoObjetos;
    }

    private void ActualizarTexto()
    {
        if (textoPeso != null)
        {
            textoPeso.text = "Peso: " + ObtenerPesoTotal().ToString("F1") + " / " + capacidadMaxima + " kg";
        }
    }
}
