using UnityEngine;


public class InputScript : MonoBehaviour
{
    //Declare variables
    public static InputScript instance;
    public bool controlType;//true for mobile, false for pc
    public GameObject gameManager;

    public int selectedID;
    public bool selecting = true;
    CameraScript cameraMovement;

    private Touch initTouch = new Touch();

    private void Start()
    {
        cameraMovement = Camera.main.GetComponent<CameraScript>();
    }

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

    public bool GetControlType()
    {
        return controlType;
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

            if (Input.GetKey("w"))
            {
                cameraMovement.MoveUp(0.5f);
            }
            if (Input.GetKey("s"))
            {
                cameraMovement.MoveDown(0.5f);
            }
            if (Input.GetKey("d"))
            {
                cameraMovement.MoveLeft(0.5f);
            }
            if (Input.GetKey("a"))
            {
                cameraMovement.MoveRight(0.5f);
            }

            if (Input.GetKey("i"))
            {
                Camera.main.orthographicSize += .1f;
            }

            if (Input.GetKey("o"))
            {
                Camera.main.orthographicSize -= .1f;
            }

        }
        else //mobile
        {
            if (Input.touchCount > 0 )
            {

                Select(Input.GetTouch(0).position);

            }

            foreach(Touch touch in Input.touches)
            {
                if (touch.phase== TouchPhase.Began)
                {
                    initTouch = touch;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    //swipe
                    float deltaX = initTouch.position.x - touch.position.x;
                    float deltaY = initTouch.position.y - touch.position.y;
                    cameraMovement.MoveCamera(new Vector2(deltaX, deltaY));
                    //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + deltaX, Camera.main.transform.position.y + deltaY); 

                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    initTouch = new Touch();
                }
                
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

                Renderer rs = hit.collider.GetComponent<Renderer>();
                Material m = rs.material;

                if (hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.EMPTY)
                {
                    gameManager.GetComponent<MarketplaceSpawner>().SpawnMenu();
                    selecting = false;
                }

                Debug.Log("Selected " + selectedID.ToString());

            }
        }
    }

    public void AttemptBuild(ObjectInfo.ObjectType t, ObjectFill.FillType f)
    {
        GameObject target = gameManager.GetComponent<GridScript>().GetGridTile(selectedID); //get Target
        ObjectData targetData = gameManager.GetComponent<GameInfo>().GetTypeInfo(target.GetComponent<ObjectInfo>().GetObjectType()); //get data relating to target
        
        if (gameManager.GetComponent<Currency>().GetMoney() >= targetData.purchaseCost && target.GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.EMPTY)//check that user has enough menu and that object is empty
        {
            gameManager.GetComponent<Currency>().AddMoney(-targetData.purchaseCost);
            Debug.Log("Build on " + selectedID.ToString());
            gameManager.GetComponent<AssetChange>().Build(selectedID, t, f);
        }

    }

    public void AttemptUpgrade(int id)
    {
        GameObject target = gameManager.GetComponent<GridScript>().GetGridTile(id); //get Target
        ObjectData targetData = gameManager.GetComponent<GameInfo>().GetTypeInfo(target.GetComponent<ObjectInfo>().GetObjectType()); //get data relating to target

        int levelCost;

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
            gameManager.GetComponent<Currency>().AddMoney(-levelCost);
            Debug.Log("upgrade on " + id.ToString());
            gameManager.GetComponent<AssetChange>().Upgrade(id);
        }
    }

    public void AttmeptDemolish(int id)
    {
        GameObject target = gameManager.GetComponent<GridScript>().GetGridTile(id); //get Target
         
        Debug.Log("Demolish on " + id.ToString());
        gameManager.GetComponent<AssetChange>().Demolish(id);
    
    }


}





