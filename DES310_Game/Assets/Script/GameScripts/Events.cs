using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class Events : MonoBehaviour
{
    GameObject gameManager;

    string path =  "/events.txt";

    List<GameEvent> gameEvents = new List<GameEvent>(); //list of all gameEvents
    List<EventRequirement> currentLevels = new List<EventRequirement>(); //list of all current levels to compare to event requirements.


    List<EventEffects> currentEventEffects = new List<EventEffects>();
    

    void AddNewEvent(GameEvent gameEvent) //add new event to list of events
    {
        gameEvents.Add(gameEvent);
    }   

    void FindCurrentLevels() //gets the current levels to be compared to requirements
    {
        //EventRequirement newRequirement = new EventRequirement();

        //newRequirement.SetRequirementType(EventRequirementName.FOOD);
        //newRequirement.SetMax(0); //replace with getter
        //newRequirement.SetMin(0); //replace with getter

        //currentLevels.Add(newRequirement);

        //newRequirement = new EventRequirement();
        //newRequirement.SetRequirementType(EventRequirementName.SUSTAINABILITY);
        //newRequirement.SetMax(0); //replace with getter
        //newRequirement.SetMin(0); //replace with getter

        //currentLevels.Add(newRequirement);

        //newRequirement = new EventRequirement();
        //newRequirement.SetRequirementType(EventRequirementName.TIME);
        //newRequirement.SetMax(0); //replace with getter
        //newRequirement.SetMin(0); //replace with getter

        //currentLevels.Add(newRequirement);

        //newRequirement = new EventRequirement();

        //newRequirement.SetRequirementType(EventRequirementName.FILL);
        //newRequirement.SetMax(0); //replace with getter
        //newRequirement.SetMin(0); //replace with getter

        //currentLevels.Add(newRequirement);
    }
    
    EventRequirement FindCurrentLevelOfType(EventRequirementName type) //finds the level for requirement as given
    {
        //for (int i = 0; i < currentLevels.Count; i++)
        //{
        //    if (currentLevels[i].getRequirementType() == type)
        //    {
        //        return (currentLevels[i]);
        //    }
        //}
        return (currentLevels[0]);
    }

    float FindValue(ObjectFill.FillType fill, EventRequirementName req, int minLevel, int maxLevel )
    {

        //make this get current values based on paramaters.
        if (req == EventRequirementName.COUNT)
        {
            
            if (fill == ObjectFill.FillType.NONE) //if fill is none then do not compare to anything else
            {
                float count = 0;
                List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();
                for (int i = 0; i < grid.Count; i++)
                {
                    ObjectInfo objectInfo = grid[i].GetComponent<ObjectInfo>();
                    if (objectInfo.GetObjectType() != ObjectInfo.ObjectType.EMPTY && objectInfo.level >= minLevel && objectInfo.level <= maxLevel)
                    {
                        count = count + 1.0f; //count all non empty grid tiles that are between the min and max level
                    }
                }

                return count;
            }
            else //else find with fill type passed
            {
                float count = 0;
                List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();
                for (int i = 0; i < grid.Count; i++)
                {
                    ObjectInfo objectInfo = grid[i].GetComponent<ObjectInfo>();
                    if (grid[i].GetComponent<ObjectFill>().GetFillType() == fill && objectInfo.level >= minLevel && objectInfo.level <= maxLevel)
                    {
                        count = count + 1.0f; //count all grid tiles that have the fill and are between the min and max level
                    }
                }

                return count;
            }
        }
        else if (req == EventRequirementName.FOOD)
        {
            if (fill == ObjectFill.FillType.NONE) //if fill is none then do not compare to anything else
            {
                float food = 0;
                List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();
                for (int i = 0; i < grid.Count; i++)
                {
                    ObjectInfo objectInfo = grid[i].GetComponent<ObjectInfo>();
                    if (objectInfo.GetObjectType() != ObjectInfo.ObjectType.EMPTY && objectInfo.level >= minLevel && objectInfo.level <= maxLevel)
                    {
                        if (objectInfo.objectType == ObjectInfo.ObjectType.FIELD)
                        {
                            //need to get component from child
                            food = food + grid[i].GetComponentInChildren<ObjectOutput>().foodOutput[objectInfo.level - 1]; //add up food from all grid tiles that are between the min and max level
                        }
                        else
                        {
                            food = food + grid[i].GetComponent<ObjectOutput>().foodOutput[objectInfo.level - 1]; //add up food from all grid tiles that are between the min and max level
                        }
                    }
                }

                return food;
            }
            else //else find with fill type passed
            {
                float food = 0;
                List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();
                for (int i = 0; i < grid.Count; i++)
                {
                    ObjectInfo objectInfo = grid[i].GetComponent<ObjectInfo>();
                    if (grid[i].GetComponent<ObjectFill>().GetFillType() == fill && objectInfo.level >= minLevel && objectInfo.level <= maxLevel)
                    {
                        if (objectInfo.objectType == ObjectInfo.ObjectType.FIELD)
                        {
                            //need to get component from child
                            food = food + grid[i].GetComponentInChildren<ObjectOutput>().foodOutput[objectInfo.level - 1]; //add up food from all grid tiles that have the fill and are between the min and max level
                        }
                        else
                        {
                            food = food + grid[i].GetComponent<ObjectOutput>().foodOutput[objectInfo.level - 1]; //add up food from all grid tiles that have the fill and are between the min and max level
                        }
                            
                    }
                }

                return food;
            }
        }
        else if (req == EventRequirementName.SUSTAINABILITY)
        {
            if (fill == ObjectFill.FillType.NONE) //if fill is none then do not compare to anything else
            {
                float sust = 0;
                List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();
                for (int i = 0; i < grid.Count; i++)
                {
                    ObjectInfo objectInfo = grid[i].GetComponent<ObjectInfo>();
                    if (objectInfo.GetObjectType() != ObjectInfo.ObjectType.EMPTY && objectInfo.level >= minLevel && objectInfo.level <= maxLevel)
                    {
                        if (objectInfo.objectType == ObjectInfo.ObjectType.FIELD)
                        {
                            //need to get component from child and add up appropriate pol from object
                            if (objectInfo.level == 1)
                            {
                                sust = sust + grid[i].GetComponentInChildren<ObjectPollution>().pol_lvl1;
                            }
                            else if (objectInfo.level == 1)
                            {
                                sust = sust + grid[i].GetComponentInChildren<ObjectPollution>().pol_lvl2;
                            }
                            else
                            {
                                sust = sust + grid[i].GetComponentInChildren<ObjectPollution>().pol_lvl3;
                            }
                        }
                        else
                        {
                            //add up appropriate pol from object
                            if (objectInfo.level == 1)
                            {
                                sust = sust + grid[i].GetComponent<ObjectPollution>().pol_lvl1;
                            }
                            else if (objectInfo.level == 1)
                            {
                                sust = sust + grid[i].GetComponent<ObjectPollution>().pol_lvl2;
                            }
                            else
                            {
                                sust = sust + grid[i].GetComponent<ObjectPollution>().pol_lvl3;
                            }
                        }

                    }
                }

                return sust;
            }
            else //else find with fill type passed
            {
                float sust = 0;
                List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();
                for (int i = 0; i < grid.Count; i++)
                {
                    ObjectInfo objectInfo = grid[i].GetComponent<ObjectInfo>();
                    if (grid[i].GetComponent<ObjectFill>().GetFillType() == fill && objectInfo.level >= minLevel && objectInfo.level <= maxLevel)
                    {
                        if (objectInfo.objectType == ObjectInfo.ObjectType.FIELD)
                        {
                            //need to get component from child and add up appropriate pol from object
                            if (objectInfo.level == 1)
                            {
                                sust = sust + grid[i].GetComponentInChildren<ObjectPollution>().pol_lvl1;
                            }
                            else if (objectInfo.level == 1)
                            {
                                sust = sust + grid[i].GetComponentInChildren<ObjectPollution>().pol_lvl2;
                            }
                            else
                            {
                                sust = sust + grid[i].GetComponentInChildren<ObjectPollution>().pol_lvl3;
                            }
                        }
                        else
                        {
                            //add up appropriate pol from object
                            if (objectInfo.level == 1)
                            {
                                sust = sust + grid[i].GetComponent<ObjectPollution>().pol_lvl1;
                            }
                            else if (objectInfo.level == 1)
                            {
                                sust = sust + grid[i].GetComponent<ObjectPollution>().pol_lvl2;
                            }
                            else
                            {
                                sust = sust + grid[i].GetComponent<ObjectPollution>().pol_lvl3;
                            }
                        }

                    }
                }

                return sust;
            }
        }
        else if (req == EventRequirementName.TIME)
        {
            return Time.time;
        }
        else 
        {
            return 0;
        }
        
    }

    public void checkTrigger() //loop through gameEvents here and check for req
    {
        
        bool triggered = false; // flag to check if an event has been triggered
        
        int count = 0;
        while (triggered == false && count < gameEvents.Count)
        {
            if (gameEvents[count].getTriggered() == false) //if the event has not been triggered then it will be checked for
            {

                List<dynamic> eventRequirements = gameEvents[count].getEventRequirements(); //requirements for this event
                triggered = true;//sets to true initially

                for (int i = 0; i < eventRequirements.Count; i++) //check each requirement
                {

                    if(eventRequirements[i] is ValueMinOrMax)
                    {
                        //req is a min/max Value
                        float comparison = FindValue(ObjectFill.FillType.NONE, eventRequirements[i].GetRequirementType(),1,3);
                        if (eventRequirements[i].GetMin() == true)
                        {
                            
                            
                            //min
                            if (comparison< eventRequirements[i].GetValue() )
                            {
                                triggered = false;
                            }

                        }
                        else
                        {
                            //max
                            if (comparison>eventRequirements[i].GetValue() )
                            {
                                triggered = false;
                            }
                        }
                        Debug.Log("min/max");
                    }
                    else if (eventRequirements[i] is string)
                    {
                        Debug.Log("and/or");
                        if (eventRequirements[i] == "or")
                        {
                            if (triggered == false)
                            {
                                //reset and check other condition
                                triggered = true;
                            }
                            else
                            {
                                //prev condition is true so dont need to check second
                                i = eventRequirements.Count;
                            }
                        }
                        else if (triggered == false)
                        {
                            //fitst condition has failed so no need to continue check
                            i = eventRequirements.Count;
                        }
                    }
                    else if (eventRequirements[i] is ValueRange)
                    {
                        Debug.Log("Range");

                    }
                    else if (eventRequirements[i] is ObjectFill.FillType)
                    {
                        //req is fill type

                        //wont have fill in non sub req so wont need this probs?
                        

                    }
                    else
                    {
                        ObjectFill.FillType currentFill = ObjectFill.FillType.NONE;
                        int minLevel = 1;
                        int maxLevel = 3;
                        for (int j = 0; j < eventRequirements[i].Count; j++)
                        {
                           
                            if (eventRequirements[i][j] is ValueMinOrMax)
                            {
                                //req is a min/max Value
                                float comparison = FindValue(currentFill, eventRequirements[i][j].GetRequirementType(), minLevel, maxLevel);
                                if (eventRequirements[i][j].GetMin() == true)
                                {
                                    //min
                                    if (eventRequirements[i][j].GetRequirementType() == EventRequirementName.LEVEL)
                                    {
                                        
                                        minLevel = (int)eventRequirements[i][j].GetValue();
                                    }
                                    else if (comparison <= eventRequirements[i][j].GetValue())
                                    {
                                        triggered = false;
                                    }

                                }
                                else
                                {
                                    //max
                                    if (eventRequirements[i][j].GetRequirementType() == EventRequirementName.LEVEL)
                                    {
                                        maxLevel = (int)eventRequirements[i][j].GetValue();
                                    }
                                    else if (comparison >= eventRequirements[i][j].GetValue())
                                    {
                                        triggered = false;
                                    }
                                }
                                //Debug.Log("min/max");
                            }
                            else if (eventRequirements[i][j] is string)
                            {
                                Debug.Log("and/or");
                                if (eventRequirements[i][j] == "or")
                                {
                                    if (triggered == false)
                                    {
                                        //reset and check other condition
                                        triggered = true;
                                    }
                                    else
                                    {
                                        //prev condition is true so dont need to check second
                                        i = eventRequirements.Count;
                                    }

                                }
                                else if (triggered == false)
                                {
                                    //first condition has failed so no need to continue check
                                    i = eventRequirements.Count;
                                }

                            }
                            else if (eventRequirements[i][j] is ValueRange)
                            {
                                Debug.Log("Range");

                            }
                            else if (eventRequirements[i][j] is ObjectFill.FillType)
                            {
                                //req is fill type
                                currentFill = eventRequirements[i][j];

                                
                                Debug.Log("fill");

                            }
                        }

                        Debug.Log("list");
                    }

                }

                if (triggered == true) //if triggered is still true then add effects of this event
                {
                    Debug.Log("EventOccurred " + gameEvents[count].getEVentName());
                    gameEvents[count].setTriggered(true);
                    //currentEventEffects.AddEffects(gameEvents[count].getEffects());
                }

            }

            count += 1;
        }
        
    }



    //read in events
    public void HandleEventFile()
    {
        TextAsset eventsAsset = Resources.Load("files/events") as TextAsset;


        string[] lines = eventsAsset.text.Split("\n"[0]);
        int i = 0;
        
        bool eventDone = false;

        while ((i + 1)< lines.Length)
        {    
            GameEvent newGameEvent = new GameEvent();
            EventRequirement newReq = new EventRequirement();
            ValueMinOrMax minValue = new ValueMinOrMax(true, 0);
            ValueMinOrMax maxValue = new ValueMinOrMax(false, 0);
            ValueRange rangeValue = new ValueRange();
            EventEffects newEffect = new EventEffects(0, ObjectFill.FillType.NONE, EventEffectType.MONEY); ;
            eventDone = false;
            bool subReq = false;
            List<dynamic> subReqierment = new List<dynamic>();

            while ((i + 1) <= lines.Length && eventDone != true)
            {
                switch (lines[i])
                {
                    case ("EVENTNAME"):
                        i += 1;
                        newGameEvent.setEventName(lines[i]);
                        break;

                    case ("DESCRIPTION"):
                        i += 1;
                        newGameEvent.setEventDescription(lines[i]);
                        break;
                    case ("SUB_REQ"):
                        i++;
                        subReqierment = new List<dynamic>();
                        subReq = true;
                        break;
                    case ("END_SUB_REQ"):
                        i++;
                        newGameEvent.AddRequirement(subReqierment);

                        subReq = false;
                        break;
                    case ("AND"):
                        i++;
                        if (subReq)
                        {
                            subReqierment.Add("AND");
                        }
                        else
                        {
                            newGameEvent.AddRequirement("AND");
                        }
                        break;
                    case ("OR"):
                        i++;
                        if (subReq)
                        {
                            subReqierment.Add("OR");
                        }
                        else
                        {
                            newGameEvent.AddRequirement("OR");
                        }
                        break;
                    case ("FOOD"):
                        minValue = new ValueMinOrMax(true, 0);
                        minValue.SetRequirementType(EventRequirementName.FOOD);
                        i += 1;
                        minValue.SetValue(float.Parse(lines[i]));
                        if (subReq)
                        {
                            subReqierment.Add(minValue);
                        }
                        else
                        {
                            newGameEvent.AddRequirement(minValue);
                        }
                        break;

                    case ("SUSTAINABILLITY"):
                        minValue = new ValueMinOrMax(true, 0);
                        minValue.SetRequirementType(EventRequirementName.SUSTAINABILITY);
                        i += 1;
                        minValue.SetValue(float.Parse(lines[i]));
                        if (subReq)
                        {
                            subReqierment.Add(minValue);
                        }
                        else
                        {
                            newGameEvent.AddRequirement(minValue);
                        }
                        break;

                    case ("TIME_MIN"):
                        minValue = new ValueMinOrMax(true, 0);
                        minValue.SetRequirementType(EventRequirementName.TIME);
                        i += 1;
                        minValue.SetValue(float.Parse(lines[i]));
                        if (subReq)
                        {
                            subReqierment.Add(minValue);
                        }
                        else
                        {
                            newGameEvent.AddRequirement(minValue);
                        }
                        break;
                    case ("TIME_RANGE"):
                        //newReq = new EventRequirement();
                        //newReq.SetRequirementType(EventRequirementName.TIME);
                        //i += 1;
                        //newReq.SetMin(float.Parse(lines[i]));
                        //i += 1;
                        //newReq.SetMax(float.Parse(lines[i]));
                        //newGameEvent.AddRequirement(newReq);
                        break;
                    case ("FILL_TYPE"):
                        i += 1;
                        ObjectFill.FillType f;
                        if (ObjectFill.FillType.TryParse((lines[i]).ToUpper(), out f) != true) { Debug.LogError("Error: fill type does not match fill enum in events file"); } //check that this passes
                        if (subReq)
                        {
                            subReqierment.Add(f);
                        }
                        else
                        {
                            newGameEvent.AddRequirement(f);
                        }
                        break;
                    case ("LVL"):
                        minValue = new ValueMinOrMax(true, 0);
                        minValue.SetRequirementType(EventRequirementName.LEVEL);
                        i += 1;
                        minValue.SetValue(float.Parse(lines[i]));
                        if (subReq)
                        {
                            subReqierment.Add(minValue);
                        }
                        else
                        {
                            newGameEvent.AddRequirement(minValue);
                        }
                        break;
                    case ("COUNT"):
                        minValue = new ValueMinOrMax(true, 0);
                        minValue.SetRequirementType(EventRequirementName.COUNT);
                        i += 1;
                        minValue.SetValue(float.Parse(lines[i]));
                        if (subReq)
                        {
                            subReqierment.Add(minValue);
                        }
                        else
                        {
                            newGameEvent.AddRequirement(minValue);
                        }
                        break;
                    case ("EFFECT"):
                        newEffect = new EventEffects(0, ObjectFill.FillType.NONE, EventEffectType.MONEY);
                        i += 1;
                        break;
                    case ("EFFECTING"):
                        i += 1;
                        ObjectFill.FillType et;
                        if (ObjectFill.FillType.TryParse((lines[i]).ToUpper(), out et) != true) { Debug.LogError("Error: effect fill type does not match fill enum in events file"); } //check that this
                        newEffect.SetFillEffected(et);
                        break;

                    case ("MONEY_EFFECT"):
                        EventEffectType me;
                        if (EventEffectType.TryParse((lines[i]).ToUpper(), out me) != true) { Debug.LogError("Error: effect type does not match fill enum in events file"); } //check that this
                        newEffect.SetEventEffectType(me);
                        i += 1;
                        newEffect.SetReduction(float.Parse(lines[i]));
                        i += 1;
                        break;
                    case ("FOOD_EFFECT"):
                        EventEffectType fe;
                        if (EventEffectType.TryParse((lines[i]).ToUpper(), out fe) != true) { Debug.LogError("Error: fill type does not match fill enum in events file"); } //check that this
                        newEffect.SetEventEffectType(fe);
                        i += 1;
                        newEffect.SetReduction(float.Parse(lines[i]));
                        break;

                    case ("DESTROY_EFFECT"):
                        EventEffectType de;
                        if (EventEffectType.TryParse((lines[i]).ToUpper(), out de) != true) { Debug.LogError("Error: fill type does not match fill enum in events file"); } //check that this
                        newEffect.SetEventEffectType(de);
                        break;
                    case ("EFFECT_END"):
                        i += 1;
                        break;

                    case ("END"):
                        AddNewEvent(newGameEvent);
                        eventDone = true;
                        i += 1;
                        break;

                    default:
                        i += 1;
                        break;

                }
            }
        }

       
       
    }

}





class GameEvent
{
    bool triggered = false; //if the event has been triggered
    List<dynamic> eventRequirements = new List<dynamic>(); //requirements for triggering
    List<EventEffects> effects = new List<EventEffects>();
    string eventName;
    string eventDescription;

    public bool getTriggered() { return triggered; }
    public List<dynamic> getEventRequirements() { return eventRequirements; }
    public List<EventEffects> getEffects() { return effects; }
    public string getEVentName() { return eventName; }
    public string GetEventDescription() { return eventDescription; }


    public void AddRequirement(dynamic eventRequirement) //add a new trigger requirement
    {
        eventRequirements.Add(eventRequirement);
    }

    public void AddEfffect(EventEffects eventEffects) //add a new effect of event
    {
        effects.Add( eventEffects);
    }



    public void setTriggered(bool t) //set effects of event
    {
        triggered = t;
    }

    public void setEventName(string name) //set effects of event
    {
        eventName= name;
    }
    public void setEventDescription(string description) //set effects of event
    {
        eventDescription = description;
    }
}

class EventRequirement
{
    public EventRequirement()
    {

    }
    

}


/// <summary>
/// Allows a value to be checked
/// pass true to make it a min, false for a max
/// </summary>
class ValueMinOrMax
{
    public ValueMinOrMax(bool m, float v)
    {
        min = m;
        value = v;
    }

    EventRequirementName requirementType; //type of requirement
    bool min; //true for min
    float value;
    public void SetValue(float v) { value= v; }
    public EventRequirementName GetRequirementType() { return requirementType; }
    public void SetRequirementType(EventRequirementName t) { requirementType = t; }
    public float GetValue() { return value; }
    public bool GetMin() { return min; }
    
}


/// <summary>
/// Allows a value range to be checked
/// pass min and max range and type
/// </summary>
class ValueRange
{
    public ValueRange(float minValue, float maxValue, EventRequirementName eventType)
    {
        requirementType = eventType;
        min = minValue;
        max = maxValue;
    }
    public ValueRange() { }

    EventRequirementName requirementType; //type of requirement
    float min; //max requirement
    float max; //min requirement


    public float getMin() { return min; }
    public float getMax() { return max; }
    public EventRequirementName GetRequirementType() { return requirementType; }
    public void SetRequirementType(EventRequirementName t) { requirementType = t; }
    public void SetMin(float m) { min = m; }
    public void SetMax(float m) { max = m; }
}





class EventEffects //effects of an effect
{
    public EventEffects(float r,  ObjectFill.FillType f, EventEffectType t)
    {
        reduction = r;
        fillEffected = f;
        effectType = t;
    }

    ObjectFill.FillType fillEffected;
    EventEffectType effectType;
    float reduction;

    public float GetReduction() { return reduction; }
    public EventEffectType GetEventEffectType() { return effectType; }
    public ObjectFill.FillType GetFillEffected() { return fillEffected; }

    public void SetReduction(float r) { reduction = r; }
    public void SetFillEffected(ObjectFill.FillType f) { fillEffected= f; }
    public void SetEventEffectType(EventEffectType t) { effectType = t; }


}

public enum EventEffectType
{
    MONEY =0,
    FOOD = 1,
    DESTROY_RANDOM =2
}

public enum EventRequirementName //different requirement types
{
    FOOD = 0,
    SUSTAINABILITY = 1,
    TIME = 2,
    FILL=3,
    LEVEL =4,
    COUNT=5
}
