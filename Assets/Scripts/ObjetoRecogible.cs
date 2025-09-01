using UnityEngine;
using TMPro;

public class ObjetoRecogible : MonoBehaviour
{
    [Header("Configuración del objeto")]
    public float peso = 1f;

    [Header("Flotación")]
    public float amplitud = 0.5f;
    public float velocidadFlotar = 2f;

    [Header("Interacción")]
    public float radioInteraccion = 2f;
    public TextMeshProUGUI textoUI;

    private Vector3 posicionInicial;
    private Transform jugador;
    private PesoMaleta maleta;

    void Start()
    {
        posicionInicial = transform.position;
        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");
        if (objJugador != null)
            jugador = objJugador.transform;

        maleta = FindObjectOfType<PesoMaleta>();

        if (textoUI != null)
            textoUI.enabled = false;
    }

    void Update()
    {
        float nuevaY = posicionInicial.y + Mathf.Sin(Time.time * velocidadFlotar) * amplitud;
        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);

        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= radioInteraccion)
        {
            if (textoUI != null)
            {
                textoUI.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && maleta != null)
            {
                if (maleta.AgregarObjeto(peso))
                {
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
