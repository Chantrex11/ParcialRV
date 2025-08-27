using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class JsonReadWriteSystem : MonoBehaviour
{
    public InputField nameInputField;
    public InputField emailInputField;
    public InputField ageInputField;
    public InputField cityInputField;

    public void SaveToJson()
    {
        Debug.Log("Save to Json");
        Debug.Log("Weapon Name: " + nameInputField.text);
        WeaponData data = new WeaponData();
        data.Name = nameInputField.text;
        data.email = emailInputField.text;
        data.Age = int.Parse(ageInputField.text);
        data.City = cityInputField.text;

        String json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/guardado/WeaponData.json", json);
    }

    public void LoadFromJson()
    {
        String json = File.ReadAllText(Application.dataPath + "/guardado/WeaponData.json");
        WeaponData data = JsonUtility.FromJson<WeaponData>(json);

        nameInputField.text = data.Name;
        emailInputField.text = data.email;
        ageInputField.text = data.Age.ToString();
        cityInputField.text = data.City;
    }

}
