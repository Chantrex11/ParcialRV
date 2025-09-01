using UnityEngine;
using System.Collections;

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

    private bool activo = false;          
    private bool introTerminada = false;  

    void Start()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 90f);

        if (scriptMovimientoJugador != null)
            scriptMovimientoJugador.enabled = false;

        // Configuración inicial de cámaras
        if (camaraCelular != null) camaraCelular.enabled = true;
        if (camaraIntro != null) camaraIntro.enabled = false;
        if (camaraGameplay != null) camaraGameplay.enabled = false;

        StartCoroutine(CambioCelularAIntro());
    }

    void Update()
    {
        if (!activo)
        {
            transform.Translate(direccion * velocidadBanda * Time.deltaTime, Space.World);
        }

        if (introTerminada && !activo && Input.GetKeyDown(KeyCode.Space))
        {
            transform.SetParent(null);

            if (puntoInicioJugador != null)
            {
                Debug.Log("Moviendo maleta al punto de inicio: " + puntoInicioJugador.position);
                transform.position = puntoInicioJugador.position;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
            }
            else
            {
                Debug.LogWarning("No se asignó puntoInicioJugador, solo girando maleta.");
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
