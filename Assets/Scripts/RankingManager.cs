using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour
{
    [Header("Referencias")]
    public JsonReadWriteSystem jsonSystem;     
    public TextMeshProUGUI rankingText;         
    public GameObject panelRanking;        

    public PesoMaleta pesoMaletaScript;         
    public introMaleta introMaletaScript;         

    private void Start()
    {
        if (panelRanking != null)
            panelRanking.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Maleta"))
        {
            if (panelRanking != null)
                panelRanking.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

            if (pesoMaletaScript != null && introMaletaScript != null && jsonSystem != null)
            {
                float puntajeFinal = pesoMaletaScript.ObtenerPesoTotal() + introMaletaScript.tiempoTranscurrido;
                jsonSystem.ContarPuntos(puntajeFinal);
            }
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
