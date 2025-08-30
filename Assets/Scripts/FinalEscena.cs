using UnityEngine;

public class FinalEscena : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(EscenaManager.instance.CargarNivel());
        }
    }
}
