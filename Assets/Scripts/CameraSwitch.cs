using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Transform cam;           // arrastra la cámara principal
    public Transform posCelular;    // punto donde está el celular
    public Transform posMaleta;     // punto de la maleta acostada
    public Transform posGameplay;   // punto para tercera persona

    public float velocidad = 2f;

    private Transform objetivoActual;
    private bool mover = false;

    void Start()
    {
        // Arrancamos apuntando al celular
        objetivoActual = posCelular;
        cam.position = posCelular.position;
        cam.rotation = posCelular.rotation;
    }

    void Update()
    {
        if (mover && objetivoActual != null)
        {
            cam.position = Vector3.Lerp(cam.position, objetivoActual.position, Time.deltaTime * velocidad);
            cam.rotation = Quaternion.Lerp(cam.rotation, objetivoActual.rotation, Time.deltaTime * velocidad);

            // Si estamos suficientemente cerca, paramos el movimiento
            if (Vector3.Distance(cam.position, objetivoActual.position) < 0.05f)
            {
                mover = false;
            }
        }
    }

    // --- Funciones públicas para avanzar en el “flujo” ---

    public void IrAMaleta()
    {
        objetivoActual = posMaleta;
        mover = true;
    }

    public void IrAGameplay()
    {
        objetivoActual = posGameplay;
        mover = true;
    }
}
