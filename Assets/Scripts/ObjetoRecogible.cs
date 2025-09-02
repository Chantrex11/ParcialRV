using UnityEngine;
using TMPro;

public class ObjetoRecogible : MonoBehaviour
{
    [Header("Configuración del objeto")]
    public float peso = 1f;

    [Header("Flotación")]
    public float amplitud = 0.1f;
    public float velocidadFlotar = 4f;

    [Header("Interacción")]
    public float radioInteraccion = 2f;   // Distancia para poder coger
    public TextMeshProUGUI textoUI;

    private Vector3 posicionInicial;
    private Transform jugador;
    private PesoMaleta maleta;

    [SerializeField] private JsonReadWriteSystem jsonSystem;

    void Start()
    {
        if (jsonSystem == null)
            jsonSystem = FindObjectOfType<JsonReadWriteSystem>();

        posicionInicial = transform.position;

        // Buscar jugador (Maleta)
        GameObject objJugador = GameObject.FindGameObjectWithTag("Maleta");
        if (objJugador != null)
            jugador = objJugador.transform;

        maleta = FindObjectOfType<PesoMaleta>();

        if (textoUI != null)
            textoUI.enabled = false;
    }

    void Update()
    {
        // Movimiento flotante decorativo
        float nuevaY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotar) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);

        if (jugador == null) return;

        // Calculamos la distancia al jugador
        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= radioInteraccion)
        {
            if (textoUI != null)
                textoUI.enabled = true;

            // Si presiona E, recoger objeto
            if (Input.GetKeyDown(KeyCode.E) && maleta != null)
            {
                if (maleta.AgregarObjeto(peso))
                {
                    jsonSystem.ContarPuntos(peso);
                    Destroy(gameObject);
                    if (textoUI != null)
                        textoUI.enabled = false;
                }
            }
        }
        else
        {
            if (textoUI != null)
                textoUI.enabled = false;
        }
    }
}
