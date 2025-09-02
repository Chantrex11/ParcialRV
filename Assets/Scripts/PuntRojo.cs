using UnityEngine;
using UnityEngine.UI;

public class PuntRojo : MonoBehaviour
{

    public float min = 0.5f;
    public float max = 1.5f;
    public float velocidad = 2f;

    void Update()
    {
        float intensidad = Mathf.Lerp(min, max, (Mathf.Sin(Time.time * velocidad) + 1f) / 2f);

        if (TryGetComponent<SpriteRenderer>(out var sr))
            sr.color = Color.white * intensidad;
        else if (TryGetComponent<Image>(out var img))
        {
            Color c = img.color;
            c.a = intensidad;
            img.color = c;
        }
    }
}

