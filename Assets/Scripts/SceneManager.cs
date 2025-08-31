using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaManager : MonoBehaviour
{
    public static EscenaManager instance;
    [SerializeField] Animator transition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void tutorial()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void juego()
    {
        SceneManager.LoadScene("Juego", LoadSceneMode.Single);
    }

    public void menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public IEnumerator CargarNivel()
    {
        Time.timeScale = 1f; // Reanuda el juego
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transition.SetTrigger("Start");
    }

    public void salirJuego()
    {
        Application.Quit();
    }
}
