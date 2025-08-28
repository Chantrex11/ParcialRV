using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonReadWriteSystem : MonoBehaviour
{
    public InputField nameInputField;
    public InputField emailInputField;
    public InputField ageInputField;
    public InputField cityInputField;


    [Serializable]
    public class PlayerData
    {
        public string Name;
        public string Email;
        public int Age;
        public string City;
        public int puntos = 0;
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

        //  Validar duplicados por Nombre o Email
        foreach (var player in playerList.players)
        {
            if (player.Name == newPlayer.Name || player.Email == newPlayer.Email)
            {
                Debug.LogWarning("Ya existe un jugador con ese nombre o correo. No se guardará.");
                return; // No guardamos nada
            }
        }

        // Si no está repetido, lo agregamos
        playerList.players.Add(newPlayer);

        string json = JsonUtility.ToJson(playerList, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Jugador guardado con éxito.");
    }

    public void LoadFromJson()
    {
        if (!File.Exists(filePath)) return;

        string json = File.ReadAllText(filePath);
        playerList = JsonUtility.FromJson<PlayerList>(json);

        if (playerList.players.Count > 0)
        {
            PlayerData data = playerList.players[playerList.players.Count - 1];
            nameInputField.text = data.Name;
            emailInputField.text = data.Email;
            ageInputField.text = data.Age.ToString();
            cityInputField.text = data.City;
        }
    }

    public void ResetJson()
    {
        Debug.Log("Reseteando JSON...");
        playerList = new PlayerList();
        playerList.players = new List<PlayerData>(); // asegurar que se escriba la lista vacía

        string json = JsonUtility.ToJson(playerList, true);
        File.WriteAllText(filePath, json);

        Debug.Log("JSON reseteado: " + json);
    }
}