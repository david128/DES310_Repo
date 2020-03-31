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
                data.purchaseCost = 200;
                data.levels = 3;
                data.level2Cost = 3000;
                data.level3Cost = 20000;
                break;

            case ObjectInfo.ObjectType.EMPTY:
                data.purchaseCost = 300;
                data.levels = 0;
                data.level2Cost = 0;
                data.level3Cost = 0;
                break;

            case ObjectInfo.ObjectType.FARMHOUSE:
                data.purchaseCost = 200;
                data.levels = 3;
                data.level2Cost = 10000;
                data.level3Cost = 50000;
                break;

            case ObjectInfo.ObjectType.FIELD:

                switch (fill)
                {
                    case ObjectFill.FillType.CARROT:
                        data.purchaseCost = 300;
                        data.levels = 3;
                        data.level2Cost = 1000;
                        data.level3Cost = 5000;
                        break;
                    case ObjectFill.FillType.COCCOA:
                        data.purchaseCost = 300;
                        data.levels = 3;
                        data.level2Cost = 1000;
                        data.level3Cost = 5000;
                        break;
                    case ObjectFill.FillType.CORN:
                        data.purchaseCost = 300;
                        data.levels = 3;
                        data.level2Cost = 1000;
                        data.level3Cost = 5000;
                        break;
                    default:
                        break;
                }

                break;

            case ObjectInfo.ObjectType.RICE:
                data.purchaseCost = 350;
                data.levels = 3;
                data.level2Cost = 1250;
                data.level3Cost = 10000;
                break;

            case ObjectInfo.ObjectType.CHICKEN_COOP:
                data.purchaseCost = 350;
                data.levels = 3;
                data.level2Cost = 1250;
                data.level3Cost = 6500;
                break;

            case ObjectInfo.ObjectType.RESEARCH:
                data.purchaseCost = 350;
                data.levels = 3;
                data.level2Cost = 1250;
                data.level3Cost = 10000;
                break;

            case ObjectInfo.ObjectType.VERTICAL_FARM:
                data.purchaseCost = 900;
                data.levels = 3;
                data.level2Cost = 3000;
                data.level3Cost = 6000;
                break;

            default:
                break;

        }

        return data;

    }
}

