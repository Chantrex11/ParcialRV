using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonReadWriteSystem : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private InputField emailInputField;
    [SerializeField] private InputField ageInputField;
    [SerializeField] private InputField cityInputField;
    [SerializeField] private AudioSource click;

    [Serializable]

    public class PlayerData
    {
        public string Name;
        public string Email;
        public int Age;
        public string City;
        public float puntos = 4f;
    }

    [Serializable]
    public class PlayerList
    {
        public List<PlayerData> players = new List<PlayerData>();
    }

    private string filePath;
    private PlayerList playerList = new PlayerList();

    void Start()
    {
        LoadFromJson();
    }

    void Awake()
    {

        filePath = Application.dataPath + "/guardado/PlayerData.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            playerList = JsonUtility.FromJson<PlayerList>(json);

            if (playerList == null)
                playerList = new PlayerList();
        }
        else
        {
            playerList = new PlayerList();
        }
    }

    public void SaveToJson()
    {
        Debug.Log("Guardando...");

        PlayerData newPlayer = new PlayerData();
        newPlayer.Name = nameInputField.text;
        newPlayer.Email = emailInputField.text;
        newPlayer.Age = int.Parse(ageInputField.text);
        newPlayer.City = cityInputField.text;

        click.Play(); // Reproducir sonido de clic

        //  Validar duplicados por Nombre o Email
        foreach (var player in playerList.players)
        {
            if (player.Name == newPlayer.Name || player.Email == newPlayer.Email)
            {
                return; // No guardamos nada
            }
        }

        // Si no está repetido, lo agregamos
        playerList.players.Add(newPlayer);

        string json = JsonUtility.ToJson(playerList, true);
        File.WriteAllText(filePath, json);

    }

    public void LoadFromJson()
    {
        click.Play();
        if (!File.Exists(filePath)) return;

        string json = File.ReadAllText(filePath);
        playerList = JsonUtility.FromJson<PlayerList>(json);

        /*
        if (playerList.players.Count > 0)
        {
            PlayerData data = playerList.players[playerList.players.Count - 1];
            nameInputField.text = data.Name;
            emailInputField.text = data.Email;
            ageInputField.text = data.Age.ToString();
            cityInputField.text = data.City;
        }
        */
    }

    public void ResetJson()
    {

        playerList = new PlayerList();
        playerList.players = new List<PlayerData>(); // asegurar que se escriba la lista vacía
        click.Play();
        string json = JsonUtility.ToJson(playerList, true);
        File.WriteAllText(filePath, json);

    }

    public PlayerData GetLastPlayer()
    {
        LoadFromJson();
        click.Play();
        if (playerList.players.Count == 0)
        {
            return null;
        }

        return playerList.players[playerList.players.Count - 1];
    }


    public PlayerData ContarPuntos(float puntos_sumar)
    {
        // Cargamos el JSON
        LoadFromJson();

        if (playerList.players.Count == 0)
        {
            Debug.LogWarning("No hay jugadores registrados.");
            return null;
        }

        // Tomamos el último jugador (puedes cambiar esto si quieres otro criterio)
        PlayerData ultimoJugador = playerList.players[playerList.players.Count - 1];

        // Sumamos puntos
        ultimoJugador.puntos += puntos_sumar;
        ultimoJugador.puntos = (float)Math.Round(ultimoJugador.puntos, 2);

        // Guardamos de nuevo en el JSON
        string json = JsonUtility.ToJson(playerList, true);
        File.WriteAllText(filePath, json);

        return ultimoJugador;
    }

}