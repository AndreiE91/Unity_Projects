
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
/*
    public static void SaveProgress(StuffToSave stuffToSave)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.lil";
        FileStream stream = new FileStream(path,FileMode.Create);

        PlayerData data = new PlayerData(stuffToSave);
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("Saved progress!");
    }

    public static PlayerData LoadProgress()
    {
        string path = Application.persistentDataPath + "/progress.lil";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("Loaded progress!");

            return data;
            
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    */
}
