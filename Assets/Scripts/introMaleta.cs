using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class introMaleta : MonoBehaviour
{
    [Header("Movimiento en la banda")]
    public float velocidadBanda = 2f;      
    public Vector3 direccion = Vector3.right; 

    [Header("Control del jugador")]
    public ThirdPersonMovement scriptMovimientoJugador;

    [Header("Cámaras")]
    public Camera camaraCelular;    
    public Camera camaraIntro;      
    public Camera camaraGameplay;  

    [Header("Punto de inicio para control del jugador")]
    public Transform puntoInicioJugador;  

    [Header("Tiempo que la cámara del celular permanece activa")]
    public float tiempoCelular = 4f;

    [Header("Texto de instrucción")]
    public TMP_Text textoInstruccion; 
    public Image imagenInstruccion; 

    private bool activo = false;          
    private bool introTerminada = false;  
    private bool instruccionMostrada = false;

    void Start()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 90f);

        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = false;

        if (camaraCelular != null) camaraCelular.enabled = true;
        if (camaraIntro != null) camaraIntro.enabled = false;
        if (camaraGameplay != null) camaraGameplay.enabled = false;

        if (imagenInstruccion != null)
            imagenInstruccion.enabled = false;

        if (textoInstruccion != null)
        {
            textoInstruccion.text = "Presiona                             para continuar";
            textoInstruccion.enabled = false;
        }

        StartCoroutine(CambioCelularAIntro());
    }

    void Update()
    {
        if (!activo)
        {
            transform.Translate(direccion * velocidadBanda * Time.deltaTime, Space.World);
        }

        if (introTerminada && !instruccionMostrada)
        {
        if (textoInstruccion != null)
            textoInstruccion.enabled = true;

        if (imagenInstruccion != null)
            imagenInstruccion.enabled = true;

        instruccionMostrada = true; 
        }

        if (introTerminada && !activo && Input.GetKeyDown(KeyCode.Space))
        {
            if (textoInstruccion != null)
                textoInstruccion.enabled = false;

            if (imagenInstruccion != null)
                imagenInstruccion.enabled = false;

            transform.SetParent(null);

            if (puntoInicioJugador != null)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {

                    rb.isKinematic = true;
                }

                transform.position = puntoInicioJugador.position;
                transform.rotation = puntoInicioJugador.rotation;

                if (rb != null)
                {
                    rb.isKinematic = false;
                }

                Debug.Log("Maleta movida exactamente al Empty: " + puntoInicioJugador.position);
            }
            activo = true;

            if (scriptMovimientoJugador != null)
                scriptMovimientoJugador.enabled = true;

            if (camaraIntro != null) camaraIntro.enabled = false;
            if (camaraGameplay != null) camaraGameplay.enabled = true;
        }
    }

    IEnumerator CambioCelularAIntro()
    {
        yield return new WaitForSeconds(tiempoCelular);

        if (camaraCelular != null) camaraCelular.enabled = false;
        if (camaraIntro != null) camaraIntro.enabled = true;

        introTerminada = true;
    }
}
