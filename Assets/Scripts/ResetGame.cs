using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private AudioSource click;

    public void menu()
    {
        // Cargar la primera escena del Build
        Destroy(EscenaManager.instance.gameObject);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Menu");
        click.Play();
    }

    public void cerrar()
    {
        click.Play();
        Application.Quit();
    }
}
