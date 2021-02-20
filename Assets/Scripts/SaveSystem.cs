using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveScore()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = GameManager.instance.path;
        FileStream stream = new FileStream(path, FileMode.Create);

        ScoreData data = new ScoreData(GameManager.instance.data.highScore);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static ScoreData LoadData()
    {
        string path = GameManager.instance.path;
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ScoreData data = bf.Deserialize(stream) as ScoreData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Not found");
            return null;
        }
    }
}
