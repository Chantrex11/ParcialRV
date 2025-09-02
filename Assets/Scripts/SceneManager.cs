using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EscenaManager : MonoBehaviour
{
    public static EscenaManager instance;
    [SerializeField] Animator transition;
    //[SerializeField] private Canvas nombreJugador;
    [SerializeField] private AudioSource click;

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
        click.Play();
    }

    public void tutorial()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        click.Play();
    }

    public void juego()
    {
        SceneManager.LoadScene("Juego", LoadSceneMode.Single);
        click.Play();

    }

    public void menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        click.Play();
    }

    public void EjecutarCargarNivel()
    {
        StartCoroutine(CargarNivel());
        click.Play();
    }

    public IEnumerator CargarNivel()
    {
        
        Time.timeScale = 1f; // Reanuda el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        //nombreJugador.gameObject.SetActive(true);
    }

    public void salirJuego()
    {
        click.Play();
        Application.Quit();

    }
}
