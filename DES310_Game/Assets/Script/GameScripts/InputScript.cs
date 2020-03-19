using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class InputScript : MonoBehaviour
{
    /// <summary> Called as soon as the player touches the screen. The argument is the screen position. </summary>
    public event Action<Vector2> onStartTouch;
    /// <summary> Called as soon as the player stops touching the screen. The argument is the screen position. </summary>
    public event Action<Vector2> onEndTouch;
    /// <summary> Called if the player completed a quick tap motion. The argument is the screen position. </summary>
    public event Action<Vector2> onTap;
    /// <summary> Called if the player swiped the screen. The argument is the screen movement delta. </summary>
    public event Action<Vector2> onSwipe;
    /// <summary> Called if the player pinched the screen. The arguments are the distance between the fingers before and after. </summary>
    public event Action<float, float> onPinch;

    /// <summary> Has the player at least one finger on the screen? </summary>
    public bool isTouching { get; private set; }

    /// <summary> The point of contact if it exists in Screen space. </summary>
    public Vector2 touchPosition { get { return touch0LastPosition; } }

    public float maxDistanceForTap = 10.0f;
    public float maxDurationForTap = 0.4f;

    //Declare variables
    public static InputScript instance;
    public bool controlType;//true for mobile, false for pc
    public GameObject gameManager;

    public int selectedID;
    public bool selecting = true;
    CameraScript cameraMovement;

    public float zoomOutMin = 5;
    public float zoomOutMax = 17;

    Vector3 touchStart;
    Vector2 touch0StartPosition;
    Vector2 touch0LastPosition;
    float touch0StartTime;

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

        if (Input.GetMouseButtonDown(0))
        {
            Select(Input.mousePosition);
        }

        if (Input.GetKey("w") && Camera.main.transform.position.z <= 41 && Camera.main.transform.position.x <= 58)
        {
            cameraMovement.MoveUp(0.5f);
        }
        if (Input.GetKey("s") && Camera.main.transform.position.z >= -15 && Camera.main.transform.position.x >= -16)
        {
            cameraMovement.MoveDown(0.5f);
        }
        if (Input.GetKey("d") && Camera.main.transform.position.z >= -15 && Camera.main.transform.position.x <= 58)
        {
            cameraMovement.MoveLeft(0.5f);
        }
        if (Input.GetKey("a") && Camera.main.transform.position.z <= 41 && Camera.main.transform.position.x >= -16)
        {
            cameraMovement.MoveRight(0.5f);
        }

        if (Input.GetKey("i") && Camera.main.orthographicSize >= 5.0f)
        {
            Camera.main.orthographicSize -= .1f;
        }

        if (Input.GetKey("o") && Camera.main.orthographicSize <= 15.0f)
        {
            Camera.main.orthographicSize += .1f;
        }

        if (Input.GetKey("m"))
        {
            Camera.main.orthographicSize += .1f;
        }

        if (Input.GetKey("n"))
        {
            Camera.main.orthographicSize += .1f;
        }

        UpdateWithTouch();
    }

    void UpdateWithTouch()
    {
        int touchCount = Input.touches.Length;

        if (touchCount == 1)
        {
            Touch touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        touch0StartPosition = touch.position;
                        touch0StartTime = Time.deltaTime;
                        touch0LastPosition = touch0StartPosition;

                        isTouching = true;

                        if (onStartTouch != null) onStartTouch(touch0StartPosition);


                        break;
                    }
                case TouchPhase.Moved:
                    {
                        touch0LastPosition = touch.position;

                        if (touch.deltaPosition != Vector2.zero && isTouching)
                        {
                            OnSwipe(touch.deltaPosition);
                        }

                        break;
                    }
                case TouchPhase.Ended:
                    {
                        if (Time.deltaTime - touch0StartTime <= maxDurationForTap && Vector2.Distance(touch.position, touch0StartPosition) <= maxDistanceForTap && isTouching)
                        {
                            OnClick(touch.position);
                        }

                        if (onEndTouch != null) onEndTouch(touch.position);
                        isTouching = false;
                        controlType = true;
                        break;
                    }
                case TouchPhase.Stationary:
                case TouchPhase.Canceled:
                    break;
            }
        }
        else if (touchCount == 2)
        {
            Touch touch0 = Input.touches[0];
            Touch touch1 = Input.touches[1];

            if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended) return;

            isTouching = true;

            float previousDistance = Vector2.Distance(touch0.position - touch0.deltaPosition, touch1.position - touch1.deltaPosition);

            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            if (previousDistance != currentDistance)
            {
                OnPinch((touch0.position + touch1.position) / 2, previousDistance, currentDistance, (touch1.position - touch0.position).normalized);
            }
        }
        else
        {
            if (isTouching)
            {
                if (onEndTouch != null) onEndTouch(touch0LastPosition);
                isTouching = false;
            }

            controlType = true;
        }
    }

    void OnClick(Vector2 position)
    {
        if (onTap != null) //&& (ignoreUI || !IsPointerOverUIObject()))
        {
            onTap(position);
        }
    }

    void OnSwipe(Vector2 deltaPosition)
    {
        if (onSwipe != null)
        {
            onSwipe(deltaPosition);
        }

        if (controlType)
        {
            Camera.main.transform.position -= (Camera.main.ScreenToWorldPoint(deltaPosition) - Camera.main.ScreenToWorldPoint(Vector2.zero));
        }
    }

    void OnPinch(Vector2 center, float oldDistance, float newDistance, Vector2 touchDelta)
    {
        if (onPinch != null)
        {
            onPinch(oldDistance, newDistance);
        }

        if (controlType == true)
        {
            if (Camera.main.orthographic)
            {
                var currentPinchPosition = Camera.main.ScreenToWorldPoint(center);

                Camera.main.orthographicSize = Mathf.Max(0.1f, Camera.main.orthographicSize * oldDistance / newDistance);

                var newPinchPosition = Camera.main.ScreenToWorldPoint(center);

                Camera.main.transform.position -= newPinchPosition - currentPinchPosition;
            }
        }
    }

    void TouchZoom()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float curMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = curMagnitude - prevMagnitude;

            Zooming(difference * 0.01f);
        }
        else if (Input.touchCount == 1)
        {

        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Camera.main.transform.position += direction;
        }
    }

    void Zooming(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    //Finds what object is being selected
    void Select(Vector2 pos)
    {
        //Declare variables
        RaycastHit hit;

        //casts a ray from camera to mouse position
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (selecting == true)
        {
            //Checks if the ray connects with an object/asset
            if (Physics.Raycast(ray, out hit))
            {
                selectedID = hit.collider.gameObject.GetComponent<ObjectInfo>().GetObjectID(); //object we are clicking's ID

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