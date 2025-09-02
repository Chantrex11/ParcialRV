using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour
{
    [Header("Referencias")]
    public JsonReadWriteSystem jsonSystem;      // Arrastra el script del JSON aquí
    public TextMeshProUGUI rankingText;         // Texto donde mostrarás el ranking
    public GameObject panelRanking;             // Panel que se activa cuando la maleta entra al trigger

    private void Start()
    {
        if (panelRanking != null)
            panelRanking.SetActive(false); // Ocultar el panel al inicio

        //panelRanking = GameObject.Find("PanelRanking");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Maleta"))
        {
            if (panelRanking != null)
                panelRanking.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            ActualizarRanking();
        }
    }

    public void ActualizarRanking()
    {
        if (jsonSystem == null || rankingText == null)
            return;

        List<JsonReadWriteSystem.PlayerData> lista = jsonSystem.GetAllPlayers();

        if (lista == null || lista.Count == 0)
        {
            rankingText.text = "No hay jugadores registrados aún.";
            return;
        }

        var ordenados = lista.OrderByDescending(p => p.puntos).ToList();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("");

        for (int i = 0; i < ordenados.Count; i++)
        {
            sb.AppendLine($"{i + 1}. {ordenados[i].Name} - {ordenados[i].puntos} pts");
        }

        rankingText.text = sb.ToString();
    }
}
