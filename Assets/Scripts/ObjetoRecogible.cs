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
    public float radioInteraccion = 2f;
    public TextMeshProUGUI textoUI;

    private Vector3 posicionInicial;
    private Transform jugador;
    private PesoMaleta maleta;

    [SerializeField] private JsonReadWriteSystem jsonSystem;

    void Start()
    {
        if (jsonSystem == null)
        {
            jsonSystem = FindObjectOfType<JsonReadWriteSystem>();
        }

        posicionInicial = transform.position;

        GameObject objJugador = GameObject.FindGameObjectWithTag("Maleta");
        if (objJugador != null)
            jugador = objJugador.transform;

        maleta = FindObjectOfType<PesoMaleta>();

        if (textoUI != null)
            textoUI.gameObject.SetActive(false); // desactivamos el GameObject completo
    }

    void Update()
    {
        // Movimiento flotante
        float nuevaY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotar) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);

        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= radioInteraccion)
        {
            // Activar texto y actualizar contenido
            if (textoUI != null)
            {
                textoUI.gameObject.SetActive(true);
                textoUI.text = "E: Recoger  |  X: Eliminar";
            }

            // Opción 1: Recoger con E
            if (Input.GetKeyDown(KeyCode.E) && maleta != null)
            {
                if (maleta.AgregarObjeto(peso))
                {
                    Destroy(gameObject);
                    if (textoUI != null)
                        textoUI.gameObject.SetActive(false);

                    if (jsonSystem != null)
                        jsonSystem.ContarPuntos(peso);
                }
            }

            // Opción 2: Eliminar con X
            if (Input.GetKeyDown(KeyCode.X))
            {
                Destroy(gameObject);
                if (textoUI != null)
                    textoUI.gameObject.SetActive(false);
            }
        }
        else
        {
            if (textoUI != null)
                textoUI.gameObject.SetActive(false);
        }
    }
}
