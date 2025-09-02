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
        public float tiempoRestante = 0f;
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
        // Usar carpeta segura para Editor y Build
        filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        Debug.Log("[Json] Ruta JSON: " + filePath);

        // Si no existe, crear uno vacío de una vez
        if (!File.Exists(filePath))
        {
            playerList = new PlayerList(); // lista vacía
            string jsonVacio = JsonUtility.ToJson(playerList, true);
            File.WriteAllText(filePath, jsonVacio);
            Debug.Log("[Json] Creado JSON vacío.");
        }
        else
        {
            string json = File.ReadAllText(filePath);
            playerList = JsonUtility.FromJson<PlayerList>(json) ?? new PlayerList();
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

        if (click != null) click.Play();

        // Duplicados
        foreach (var player in playerList.players)
        {
            if (player.Name == newPlayer.Name || player.Email == newPlayer.Email)
            {
                Debug.LogWarning("[Json] Nombre o Email ya existen. No se guarda.");
                return;
            }
        }

        playerList.players.Add(newPlayer);

        string json = JsonUtility.ToJson(playerList, true);
        File.WriteAllText(filePath, json);
        Debug.Log("[Json] Guardado OK en: " + filePath + " | Total jugadores: " + playerList.players.Count);
    }

    public void LoadFromJson()
    {
        if (click != null)
        {
            click.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource click no asignado en JsonReadWriteSystem");
        }
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
        if (click != null) click.Play();

        string json = JsonUtility.ToJson(playerList, true);
        File.WriteAllText(filePath, json);
        Debug.Log("[Json] RESET. Archivo vacío en: " + filePath);
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
        LoadFromJson();

        if (playerList.players.Count == 0)
        {
            Debug.LogWarning("No hay jugadores registrados.");
            return null;
        }

        PlayerData ultimoJugador = playerList.players[playerList.players.Count - 1];


        ultimoJugador.puntos += puntos_sumar;
        ultimoJugador.puntos = (float)Math.Round(ultimoJugador.puntos, 2);

        string json = JsonUtility.ToJson(playerList, true);
        File.WriteAllText(filePath, json);

        return ultimoJugador;
    }

    public List<PlayerData> GetAllPlayers()
    {
        if (!File.Exists(filePath))
            return new List<PlayerData>();

        string json = File.ReadAllText(filePath);
        PlayerList data = JsonUtility.FromJson<PlayerList>(json);
        return data != null ? data.players : new List<PlayerData>();
    }


}