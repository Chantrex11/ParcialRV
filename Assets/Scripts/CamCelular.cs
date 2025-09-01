using UnityEngine;

public class CamCelular : MonoBehaviour
{
    [Header("Movimiento hacia el objetivo")]
    public Transform objetivo;       // El celular
    public float velocidad = 2f;     // Velocidad de acercamiento
    public Vector3 offset = new Vector3(0, 3f, 0); // Distancia inicial sobre el celular (altura)

    private bool mover = true;

    void Start()
    {
        if (objetivo != null)
        {
            // Posición inicial arriba del celular usando offset
            transform.position = objetivo.position + offset;

            // Mira hacia el celular
            transform.LookAt(objetivo.position);
        }
    }

    void Update()
    {
        if (mover && objetivo != null)
        {
            // Posición deseada directamente sobre el celular (ajustar altura si quieres)
            Vector3 destino = objetivo.position + new Vector3(0, 1.5f, 0); // altura final mirando desde arriba

            // Lerp para mover suavemente la cámara
            transform.position = Vector3.Lerp(transform.position, destino, Time.deltaTime * velocidad);

            // Apunta siempre hacia el celular
            transform.LookAt(objetivo.position);

            // Detener movimiento cuando esté suficientemente cerca
            if (Vector3.Distance(transform.position, destino) < 0.05f)
            {
                mover = false;
            }
        }
    } 
}
