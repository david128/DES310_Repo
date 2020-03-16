using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int money;
    public float food;
    public string[] gridType;
    public int[] gridLevel;
    public int[] gridID;
    public string[] gridFill;
    public float[] gridPos;
    public float[] pos;

    public SaveData(int moneyData, float foodData, List<GameObject> grid)
    {
        money = moneyData;
        food = foodData;

        gridType = new string[25];
        gridLevel = new int[25];
        gridID = new int[25];
        gridFill = new string[25];
        gridPos = new float[75];

        pos = new float[3];
        int count = 0;

        for (int i = 0; i < 25; i++)
        {
            gridType[i] = grid[i].GetComponent<ObjectInfo>().GetObjectType().ToString();
            gridLevel[i] = grid[i].GetComponent<ObjectInfo>().GetObjectLevel();
            gridID[i] = grid[i].GetComponent<ObjectInfo>().GetObjectID();
            gridFill[i] = grid[i].GetComponent<ObjectFill>().GetFillType().ToString();
            gridPos[count] = grid[i].transform.localPosition.x;
            gridPos[count + 1] = grid[i].transform.localPosition.y;
            gridPos[count + 2] = grid[i].transform.localPosition.z;

            count += 3;
        }

        count = 0;
    }
}
