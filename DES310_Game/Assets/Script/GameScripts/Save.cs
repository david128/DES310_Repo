using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public GameObject gameManager;

    //saves current game data
    public void SaveGameData()
    {
        SaveGame.SaveGameData(gameManager.GetComponent<Currency>().GetMoney(), gameManager.GetComponent<FoodScript>().GetFood(), gameManager.GetComponent<GridScript>().GetGrid());
    }

    //loads data into variables to be used in game
    public void LoadGameData()
    {
        SaveData data = SaveGame.LoadGameData();

        if (data != null && SaveGame.LoadGameData() != null)
        {
            gameManager.GetComponent<Currency>().SetMoney(data.money);
            gameManager.GetComponent<FoodScript>().SetFood(data.food);

            Vector3[] pos;
            pos = new Vector3[25];

            int[] level = new int[25];
            int[] id = new int[25];
            string[] type = new string[25];
            string[] fill = new string[25];

            int count = 0;

            for (int i = 0; i < 25; i++)
            {
                type[i] = data.gridType[i];
                level[i] = data.gridLevel[i];
                id[i] = data.gridID[i];
                fill[i] = data.gridFill[i];
                pos[i].x = data.gridPos[count];
                pos[i].y = data.gridPos[count + 1];
                pos[i].z = data.gridPos[count + 2];

                count += 3;
            }

            gameManager.GetComponent<GridScript>().LoadGrid(id, pos, type, level, fill);
        }
        else
        {
            return;
        }
    }

    public void DeleteGameData()
    {
        SaveGame.DeleteGameData();
    }
}
