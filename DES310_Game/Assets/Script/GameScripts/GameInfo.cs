using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData
{
    public int purchaseCost;
    public int levels;
    public int level2Cost;
    public int level3Cost;

}
public class GameInfo : MonoBehaviour
{
    ObjectData data = new ObjectData();

    public ObjectData GetTypeInfo(ObjectInfo.ObjectType type, ObjectFill.FillType fill) //ret
    {
        switch (type)
        {
            case ObjectInfo.ObjectType.BARN:
                data.purchaseCost = 0;
                data.levels = 3;
                data.level2Cost = 3000;
                data.level3Cost = 10000;
                break;

            case ObjectInfo.ObjectType.EMPTY:
                data.purchaseCost = 0;
                data.levels = 0;
                data.level2Cost = 0;
                data.level3Cost = 0;
                break;

            case ObjectInfo.ObjectType.FARMHOUSE:
                data.purchaseCost = 0;
                data.levels = 3;
                data.level2Cost = 1500;
                data.level3Cost = 8000;
                break;

            case ObjectInfo.ObjectType.FIELD:

                switch (fill)
                {
                    case ObjectFill.FillType.CARROT:
                        data.purchaseCost = 250;
                        data.levels = 3;
                        data.level2Cost = 600;
                        data.level3Cost = 1200;
                        break;
                    case ObjectFill.FillType.COCCOA:
                        data.purchaseCost = 1500;
                        data.levels = 3;
                        data.level2Cost = 3000;
                        data.level3Cost = 4500;
                        break;
                    case ObjectFill.FillType.CORN:
                        data.purchaseCost = 300;
                        data.levels = 3;
                        data.level2Cost = 800;
                        data.level3Cost = 2000;
                        break;
                    case ObjectFill.FillType.WHEAT:
                        data.purchaseCost = 450;
                        data.levels = 3;
                        data.level2Cost = 900;
                        data.level3Cost = 1800;
                        break;
                    case ObjectFill.FillType.TURNIP:
                        data.purchaseCost = 200;
                        data.levels = 3;
                        data.level2Cost = 400;
                        data.level3Cost = 800;
                        break;
                    case ObjectFill.FillType.SUGARCANE:
                        data.purchaseCost = 1000;
                        data.levels = 3;
                        data.level2Cost = 2000;
                        data.level3Cost = 5000;
                        break;
                    case ObjectFill.FillType.SUNFLOWER:
                        data.purchaseCost = 750;
                        data.levels = 3;
                        data.level2Cost = 1500;
                        data.level3Cost = 3500;
                        break;
                    case ObjectFill.FillType.POTATO:
                        data.purchaseCost = 150;
                        data.levels = 3;
                        data.level2Cost = 400;
                        data.level3Cost = 1000;
                        break;
                    default:
                        break;
                }

                break;

            case ObjectInfo.ObjectType.RICE:
                data.purchaseCost = 350;
                data.levels = 3;
                data.level2Cost = 700;
                data.level3Cost = 1250;
                break;

            case ObjectInfo.ObjectType.PIG_PEN:
                data.purchaseCost = 600;
                data.levels = 3;
                data.level2Cost = 1200;
                data.level3Cost = 2500;
                break;

            case ObjectInfo.ObjectType.COW_FIELD:
                data.purchaseCost = 1000;
                data.levels = 3;
                data.level2Cost = 2000;
                data.level3Cost = 5000;
                break;

            case ObjectInfo.ObjectType.CHICKEN_COOP:
                data.purchaseCost = 350;
                data.levels = 3;
                data.level2Cost = 700;
                data.level3Cost = 1500;
                break;

            case ObjectInfo.ObjectType.RESEARCH:
                data.purchaseCost = 350;
                data.levels = 3;
                data.level2Cost = 1250;
                data.level3Cost = 10000;
                break;

            case ObjectInfo.ObjectType.VERTICAL_FARM:
                data.purchaseCost = 3500;
                data.levels = 3;
                data.level2Cost = 5000;
                data.level3Cost = 7500;
                break;

            default:
                break;

        }

        return data;

    }
}

