using UnityEngine;

public class FinalEscena : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Time.timeScale = 0f; // Pausa el juego
            StartCoroutine(EscenaManager.instance.CargarNivel());
        }
    }
}
