using Fragsurf.Movement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveOptions (ApplyButton Options)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerOptions";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerOptions data = new PlayerOptions(Options);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveScores(SurfCharacter Scores)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerScores";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerScores data = new PlayerScores(Scores);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerOptions LoadOptions()
    {
        string path = Application.persistentDataPath + "/PlayerOptions";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerOptions data = formatter.Deserialize(stream) as PlayerOptions;
            stream.Close();
            return data;

        }else
        {
            Debug.LogError("You dont have any saved data");
            return null;
        }

    }
    public static PlayerScores LoadScores()
    {
        string path = Application.persistentDataPath + "/playerScores";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerScores data = formatter.Deserialize(stream) as PlayerScores;
            stream.Close();
            return data;

        }
        else
        {
            Debug.LogError("You dont have any saved data");
            return null;
        }

    }




}
