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

    private void Start()
    {
        ActualizarTexto();
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjetoRecogible objeto = other.GetComponent<ObjetoRecogible>();
        if (objeto != null)
        {
            AgregarObjeto(objeto.peso);
            Destroy(other.gameObject);
        }
    }

    public void AgregarObjeto(float pesoObjeto)
    {
        if (pesoMaleta + pesoObjetos + pesoObjeto <= capacidadMaxima)
        {
            pesoObjetos += pesoObjeto;
            Debug.Log("Objeto agregado. Peso total actual: " + ObtenerPesoTotal() + " kg");
        }
        else
        {
            Debug.Log("No se puede agregar. Se supera el límite de " + capacidadMaxima + " kg");
        }
        ActualizarTexto();  
    }

    public float ObtenerPesoTotal()
    {
        return pesoMaleta + pesoObjetos;
    }

    public float ObtenerEspacioDisponible()
    {
        return capacidadMaxima - ObtenerPesoTotal();
    }

    private void ActualizarTexto()
    {
        if (textoPeso != null)
        {
            textoPeso.text = "Peso: " + ObtenerPesoTotal().ToString("F1") + " / " + capacidadMaxima + " kg";
        }
    }
}
