using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Transform cam;           
    public Transform posCelular;    
    public Transform posMaleta;     
    public Transform posGameplay;   

    public float velocidad = 2f;

    private Transform objetivoActual;
    private bool mover = false;

    void Start()
    {
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

            if (Vector3.Distance(cam.position, objetivoActual.position) < 0.05f)
            {
                mover = false;
            }
        }
    }
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
