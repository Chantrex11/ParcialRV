using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaManager : MonoBehaviour
{
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

    public void salirJuego()
    {
        Application.Quit();
    }
}
