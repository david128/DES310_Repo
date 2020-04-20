using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame
{
    //saves game data 
    public static void SaveGameData(int moneyData, float foodData, List<GameObject> gridData, float quotaTimerData, int quotaData, float sustainabilityLevelData, string distributerChoiceData, int totalMoneyEarnedData, int totalMoneySpentData, float totalFoodData)
    { 
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saveData.SaveData";

        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(moneyData, foodData, gridData, quotaTimerData, quotaData, sustainabilityLevelData, distributerChoiceData, totalMoneyEarnedData, totalMoneySpentData, totalFoodData);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    //load game data
    public static SaveData LoadGameData()
    {
        string path = Application.persistentDataPath + "/saveData.SaveData";

        if (File.Exists(path))
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

    public static void DeleteGameData()
    {
        string path = Application.persistentDataPath + "/saveData.SaveData";

        if (File.Exists(path))
        {
            File.Delete(path);

            Debug.LogError("File has been removed at " + path);
        }
        else
        {
            Debug.LogError("No save file to overwrite in " + path);
            return;
        }
    }
}

