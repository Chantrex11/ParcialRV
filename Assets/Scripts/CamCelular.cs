using UnityEngine;

public class CamCelular : MonoBehaviour
{
    [Header("Movimiento hacia el objetivo")]
    public Transform objetivo;      
    public float velocidad = 2f;   
    public Vector3 offset = new Vector3(0, 3f, 0);

    private bool mover = true;

    void Start()
    {
        if (objetivo != null)
        {
            transform.position = objetivo.position + offset;
            transform.LookAt(objetivo.position);
        }
    }

    void Update()
    {
        if (mover && objetivo != null)
        {
            Vector3 destino = objetivo.position + new Vector3(0, 1.5f, 0);
            transform.position = Vector3.Lerp(transform.position, destino, Time.deltaTime * velocidad);
            transform.LookAt(objetivo.position);
            if (Vector3.Distance(transform.position, destino) < 0.05f)
            {
                mover = false;
            }
        }
    } 
}
