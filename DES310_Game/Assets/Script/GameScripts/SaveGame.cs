using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame
{
    public static void SaveGameData(int moneyData, float foodData, List<GameObject> grid)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saveData.SaveData";

        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(moneyData, foodData, grid);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SaveData LoadGameData()
    {
        string path = Application.persistentDataPath + "/saveData.SaveData";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            //Cast as save data type
            SaveData data = formatter.Deserialize(stream) as SaveData;

            //close file stream
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}

