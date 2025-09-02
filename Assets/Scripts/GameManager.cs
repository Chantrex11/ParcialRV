using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string nombreGuardado;
    [SerializeField] private TextMeshProUGUI nombreJugadorText;
    [SerializeField] private JsonReadWriteSystem jsonSystem;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Suscribirnos al evento de carga de escena
           // SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
/*
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (jsonSystem.GetLastPlayer() != null)
        {
            nombreJugadorText.text = jsonSystem.GetLastPlayer().Name;
        }
    }
*/
}
