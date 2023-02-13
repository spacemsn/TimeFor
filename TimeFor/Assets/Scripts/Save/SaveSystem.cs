using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void SavePlayer(CharacterStatus status)
    {
        PlayerData data = new PlayerData(status);
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.dataPath + "/saveStatusPlayer.json", json);
    }

    public static PlayerData loadPlayer()
    {
        string json = File.ReadAllText(Application.dataPath + "/saveStatusPlayer.json");
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);

        return data;
    }
}
