using UnityEngine;


public class InputScript : MonoBehaviour
{
    //Declare variables
    public static InputScript instance;
    public bool controlType;//true for mobile, false for pc
    public GameObject gameManager;

    public int selectedID;
    public bool selecting = true;

    //Selecting Variables
    public void AllowSelecting()
    {
        selecting = true;
    }

    public bool GetAllowSelecting() { return selecting; }

    public void SetAllowSelecting(bool s) { selecting = s; }

    public int GetSelectedID()
    {
        return selectedID;
    }

    //get Input to be called in main game loop
    public void GetInput()
    {
        //Checks which input type is being used
        if (controlType == false)//pc
        {
            if (Input.GetMouseButtonDown(0))
            {
                Select(Input.mousePosition);
            }

            if (Input.GetKeyDown("u"))
            {
                AttemptUpgrade(selectedID);
            }
            if (Input.GetKeyDown("b"))
            {
                AttemptBuild(selectedID);
            }
            if (Input.GetKeyDown("h"))
            {
                AttmeptDemolish(selectedID);
            }

        }
        else //mobile
        {
            if (Input.touchCount > 0)
            {

                Select(Input.GetTouch(0).position);
            }
        }
    }

    //Finds what object is being selected
    void Select(Vector2 pos)
    {
        //Declare variables
        RaycastHit hit;

        //casts a ray from camera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (selecting == true)
        {
            //Checks if the ray connects with an object/asset
            if (Physics.Raycast(ray, out hit))
            {

                selectedID = hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectID(); //object we are clicking's ID

                Debug.Log("Selected " + selectedID.ToString());

            }
        }
    }

    public void AttemptBuild(int id)
    {
        GameObject target = gameManager.GetComponent<GridScript>().GetGridTile(id); //get Target
        ObjectData targetData = gameManager.GetComponent<GameInfo>().GetTypeInfo(target.GetComponent<ObjectInfo>().GetObjectType()); //get data relating to target

        if (gameManager.GetComponent<Currency>().GetMoney() >= targetData.purchaseCost && target.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.EMPTY)//check that user has enough menu and that object is empty
        {
            Debug.Log("Build on " + id.ToString());
            gameManager.GetComponent<AssetChange>().Build(id, ObjectInfo.ObjectType.FIELD);
        }

    }

    public void AttemptUpgrade(int id)
    {
        GameObject target = gameManager.GetComponent<GridScript>().GetGridTile(id); //get Target
        ObjectData targetData = gameManager.GetComponent<GameInfo>().GetTypeInfo(target.GetComponent<ObjectInfo>().GetObjectType()); //get data relating to target
        float levelCost;

        if (target.GetComponent<ObjectInfo>().GetObjectLevel() == 1)//get level upgrade cost
        {
            levelCost = targetData.level2Cost;
        }
        else
        {
            levelCost = targetData.level3Cost;
        }

        //Check that have enough money and that maxLevel of asset has not been reached
        if (gameManager.GetComponent<Currency>().GetMoney() >= levelCost && target.GetComponent<ObjectInfo>().GetObjectLevel() != targetData.levels) 
        {
            //display button
            //gameManager.GetComponent<ButtonFunctions>().Enable();
            Debug.Log("upgrade on " + id.ToString());
            gameManager.GetComponent<AssetChange>().Upgrade(id);
        }
    }

    public void AttmeptDemolish(int id)
    {
        GameObject target = gameManager.GetComponent<GridScript>().GetGridTile(id); //get Target
         
        if (target.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.FIELD)//check that object is of type FIELD and then demolish
        {
            Debug.Log("Demolish on " + id.ToString());
            gameManager.GetComponent<AssetChange>().Demolish(id);
        }

    }


}





