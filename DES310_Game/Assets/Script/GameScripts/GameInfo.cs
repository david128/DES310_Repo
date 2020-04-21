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
                data.level2Cost = 1250;
                data.level3Cost = 2500;
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
                data.level2Cost = 1000;
                data.level3Cost = 2000;
                break;

            case ObjectInfo.ObjectType.FIELD:

                switch (fill)
                {
                    case ObjectFill.FillType.CARROT:
                        data.purchaseCost = 200;
                        data.levels = 3;
                        data.level2Cost = 150;
                        data.level3Cost = 200;
                        break;
                    case ObjectFill.FillType.COCCOA:
                        data.purchaseCost = 750;
                        data.levels = 3;
                        data.level2Cost = 500;
                        data.level3Cost = 600;
                        break;
                    case ObjectFill.FillType.CORN:
                        data.purchaseCost = 250;
                        data.levels = 3;
                        data.level2Cost = 150;
                        data.level3Cost = 225;
                        break;
                    case ObjectFill.FillType.WHEAT:
                        data.purchaseCost = 400;
                        data.levels = 3;
                        data.level2Cost = 450;
                        data.level3Cost = 500;
                        break;
                    case ObjectFill.FillType.TURNIP:
                        data.purchaseCost = 200;
                        data.levels = 3;
                        data.level2Cost = 50;
                        data.level3Cost = 100;
                        break;
                    case ObjectFill.FillType.SUGARCANE:
                        data.purchaseCost = 635;
                        data.levels = 3;
                        data.level2Cost = 450;
                        data.level3Cost = 550;
                        break;
                    case ObjectFill.FillType.SUNFLOWER:
                        data.purchaseCost = 500;
                        data.levels = 3;
                        data.level2Cost = 400;
                        data.level3Cost = 500;
                        break;
                    case ObjectFill.FillType.POTATO:
                        data.purchaseCost = 250;
                        data.levels = 3;
                        data.level2Cost = 100;
                        data.level3Cost = 150;
                        break;
                    default:
                        break;
                }

                break;

            case ObjectInfo.ObjectType.RICE:
                data.purchaseCost = 300;
                data.levels = 3;
                data.level2Cost = 350;
                data.level3Cost = 350;
                break;

            case ObjectInfo.ObjectType.PIG_PEN:
                data.purchaseCost = 425;
                data.levels = 3;
                data.level2Cost = 250;
                data.level3Cost = 350;
                break;

            case ObjectInfo.ObjectType.COW_FIELD:
                data.purchaseCost = 600;
                data.levels = 3;
                data.level2Cost = 275;
                data.level3Cost = 400;
                break;

            case ObjectInfo.ObjectType.CHICKEN_COOP:
                data.purchaseCost = 350;
                data.levels = 3;
                data.level2Cost = 200;
                data.level3Cost = 300;
                break;

            case ObjectInfo.ObjectType.RESEARCH:
                data.purchaseCost = 0;
                data.levels = 3;
                data.level2Cost = 1500;
                data.level3Cost = 3000;
                break;

            case ObjectInfo.ObjectType.VERTICAL_FARM:
                data.purchaseCost = 1200;
                data.levels = 3;
                data.level2Cost = 600;
                data.level3Cost = 700;
                break;

            case ObjectInfo.ObjectType.MEAT_LAB:
                data.purchaseCost = 1000;
                data.levels = 3;
                data.level2Cost = 600;
                data.level3Cost = 700;
                break;

            default:
                break;

        }

        return data;

    }
}

