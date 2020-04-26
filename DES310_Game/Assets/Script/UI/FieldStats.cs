using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class FieldStats : MonoBehaviour
{
    //Public variables to be assigned in inspector
    public GameObject gameManager;

    public TextMeshProUGUI fieldCountText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI foodText;

    public ObjectInfo.ObjectType tileType;
    public ObjectFill.FillType fillType;

    //variables used only in script
    int[] money;
    int moneyPer;
    int totalMoneyOut;

    int[] food;
    int foodPer;
    int totalFoodOut;

    int tileCount;

    //Gathers farm stats by looping through grid
    public void GatherStats()
    {
        List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();

        tileCount = 0;
        totalFoodOut = 0;
        totalMoneyOut = 0;

        //for each grid square
        for (int i = 0; i < grid.Count; i++)
        {
            if(grid[i].GetComponent<ObjectInfo>().GetObjectType() == tileType && grid[i].GetComponent<ObjectFill>().GetFillType() == fillType)
            {
                tileCount++;

                if(grid[i].TryGetComponent<ObjectOutput>(out ObjectOutput output))
                {
                    money = output.GetTileMoneyOutput();
                    food = output.GetTileFoodOutput();
                }
                else
                {
                    money = grid[i].GetComponentInChildren<ObjectOutput>().GetTileMoneyOutput();
                    food = grid[i].GetComponentInChildren<ObjectOutput>().GetTileFoodOutput();
                }

                moneyPer = money[grid[i].GetComponent<ObjectInfo>().GetObjectLevel() - 1];
                foodPer = food[grid[i].GetComponent<ObjectInfo>().GetObjectLevel() - 1];

                totalMoneyOut += moneyPer;
                totalFoodOut += foodPer;
            }
            else
            {

            }

        }

        fieldCountText.text = tileCount.ToString();
        moneyText.text = totalMoneyOut.ToString();
        foodText.text = totalFoodOut.ToString();

        gameManager.GetComponent<GameLoop>().SaveFieldStats(tileType, fillType, tileCount, totalMoneyOut, totalFoodOut);
    }
}
