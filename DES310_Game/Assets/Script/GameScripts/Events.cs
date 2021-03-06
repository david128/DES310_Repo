﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;



public class Events : MonoBehaviour
{
    public GameObject gameManager;

    //event Ui used in display
    public GameObject eventUI;

    //path to file
    string path =  "/events.txt";


    List<GameEvent> gameEvents = new List<GameEvent>(); //list of all gameEvents
    List<EventEffects> currentEventEffects = new List<EventEffects>();
    
    
    public List<GameEvent> GetEvents()//get events
    {
        return gameEvents;
    }

    void AddNewEvent(GameEvent gameEvent) //add new event to list of events
    {
        gameEvents.Add(gameEvent);
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
        else if (req == EventRequirementName.RANDOM_CHANCE)
        {
            return (Random.Range(1, 100));
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
            Debug.Log(gameEvents[count].getEVentName().ToString());
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
                                        j = eventRequirements[i].Count;
                                    }

                                }
                                else if (triggered == false)
                                {
                                    //first condition has failed so no need to continue check
                                    j = eventRequirements[i].Count;
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

                    eventUI.GetComponent<EventUIChange>().ChangeText(gameEvents[count].getEVentName(), gameEvents[count].GetEventDescription());
                    eventUI.GetComponent<EventUIChange>().Enable();
                    gameEvents[count].setTriggered(true);
                    TiggerEffects(count);
                    
                }

            }

            count += 1;
        }
        
    }



    //read in events
    public void HandleEventFile()
    {
        TextAsset eventsAsset = Resources.Load("files/events") as TextAsset;
        gameEvents.Clear(); //clear stored events

        string[] lines = eventsAsset.text.Split("\n"[0]);
        int i = 0;
        
        bool eventDone = false;

        while ((i + 1)< lines.Length)
        {    
            //temp variavles used to add to list
            GameEvent newGameEvent = new GameEvent();           
            ValueMinOrMax minValue = new ValueMinOrMax(true, 0);
            ValueMinOrMax maxValue = new ValueMinOrMax(false, 0);
            ValueRange rangeValue = new ValueRange();
            EventEffects newEffect = new EventEffects(0, ObjectFill.FillType.NONE, EventEffectType.MONEY_EFFECT); ;
            eventDone = false;
            bool subReq = false;
            List<dynamic> subReqierment = new List<dynamic>();

            //for each line read to variable
            while ((i + 1) <= lines.Length && eventDone != true)
            {
                //if ends with line ending, remove
                if (lines[i].EndsWith("\r"))
                {
                    lines[i]= lines[i].Remove(lines[i].Length - 1); ;


                }
                switch (lines[i])
                {
                    //load event name
                    case ("EVENTNAME"):
                        i += 1;
                        newGameEvent.setEventName(lines[i]);
                        break;
                    //load event desc
                    case ("DESCRIPTION"):
                        i += 1;
                        newGameEvent.setEventDescription(lines[i]);
                        break;
                    //load new SubReq
                    case ("SUB_REQ"):
                        i++;
                        subReqierment = new List<dynamic>();
                        subReq = true;
                        break;
                    //add sub req to req
                    case ("END_SUB_REQ"):
                        i++;
                        newGameEvent.AddRequirement(subReqierment);
                        subReq = false;
                        break;
                    //add connector
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
                    //add connector
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
                    //add food value
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
                    //add sust value
                    case ("SUSTAINABILITY"):
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
                    //add time value
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
                    //add random chance value
                    case ("RANDOM_CHANCE"):
                        minValue = new ValueMinOrMax(true, 0);
                        minValue.SetRequirementType(EventRequirementName.RANDOM_CHANCE);
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
                    //add fill type
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
                    //add level req
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
                    //add count req
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
                    //start new effect
                    case ("EFFECT"):
                        newEffect = new EventEffects(0, ObjectFill.FillType.NONE, EventEffectType.MONEY_EFFECT);
                        i += 1;
                        break;
                    //add effecting
                    case ("EFFECTING"):
                        i += 1;
                        ObjectFill.FillType et;
                        if (ObjectFill.FillType.TryParse((lines[i]).ToUpper(), out et) != true) { Debug.LogError("Error: effect fill type does not match fill enum in events file"); } //check that this
                        newEffect.SetFillEffected(et);
                        break;
                    //set to money effect
                    case ("MONEY_EFFECT"):
                        EventEffectType me;
                        if (EventEffectType.TryParse((lines[i]).ToUpper(), out me) != true) { Debug.LogError("Error: effect type does not match fill enum in events file"); } //check that this
                        newEffect.SetEventEffectType(me);
                        i += 1;
                        newEffect.SetReduction(float.Parse(lines[i]));
                        i += 1;
                        break;
                    //set to food effect
                    case ("FOOD_EFFECT"):
                        EventEffectType fe;
                        if (EventEffectType.TryParse((lines[i]).ToUpper(), out fe) != true) { Debug.LogError("Error: fill type does not match fill enum in events file"); } //check that this
                        newEffect.SetEventEffectType(fe);
                        i += 1;
                        newEffect.SetReduction(float.Parse(lines[i]));
                        i += 1;
                        break;
                    //set to destroy effect
                    case ("DESTROY_EFFECT"):
                        EventEffectType de;
                        if (EventEffectType.TryParse((lines[i]).ToUpper(), out de) != true) { Debug.LogError("Error: fill type does not match fill enum in events file"); } //check that this
                        newEffect.SetEventEffectType(de);
                        i += 1;
                        break;
                    //add effect 
                    case ("EFFECT_END"):
                        newGameEvent.AddEfffect(newEffect);
                        i += 1;
                        break;
                    //end of event, add to list
                    case ("END"):
                        AddNewEvent(newGameEvent);
                        eventDone = true;
                        i += 1;
                        break;
                    //next line
                    default:
                        i += 1;
                        break;

                }
            }
        }

       
       
    }

    public void TiggerEffects(int index)
    {
        List<EventEffects> effects = gameEvents[index].getEffects();

        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].GetEventEffectType() == EventEffectType.DESTROY_RANDOM)
            {
                List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();
                List<int> ids = new List<int>();

                //if none then destroy any random one, if specific destroy random one of certain crops
                if (effects[i].GetFillEffected() != ObjectFill.FillType.NONE)
                {
                    for (int j = 0; j < grid.Count; j++)
                    {
                        if (grid[j].GetComponent<ObjectFill>().GetFillType() == effects[i].GetFillEffected())
                        {
                            ids.Add(j);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < grid.Count; j++)
                    {
                        if (grid[j].GetComponent<ObjectFill>().GetFillType() != ObjectFill.FillType.NONE)
                        {
                            ids.Add(j);
                        }
                    }
                }

                //demolish randomly chosen
                gameManager.GetComponent<InputScript>().AttemptDemolish(ids[Random.Range(0, ids.Count)]);
                                
            }
            else if (effects[i].GetEventEffectType() == EventEffectType.FOOD_EFFECT)
            {
                List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();
                float red = effects[i].GetReduction();

                if (effects[i].GetFillEffected() == ObjectFill.FillType.NONE)
                {
                    //effecting all
                    //reduce all current fields
                    for (int j = 0; j < grid.Count; j++)
                    {
                        if (grid[j].GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.FIELD)
                        {
                            grid[j].GetComponentInChildren<ObjectOutput>().reduceFood(red);
                        }
                        else if (grid[j].GetComponent<ObjectFill>().GetFillType() != ObjectFill.FillType.NONE)
                        {
                            grid[j].GetComponent<ObjectOutput>().reduceFood(red);
                        }
                    }

                }
                else
                {
                    //efecting specific

                    //reduce all current fields
                    for (int j = 0; j < grid.Count; j++)
                    {

                        if (grid[j].GetComponent<ObjectFill>().GetFillType() == effects[i].GetFillEffected())
                        {
                            if (grid[j].GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.FIELD)
                            {
                                grid[j].GetComponentInChildren<ObjectOutput>().reduceFood(red);
                            }
                            else
                            {
                                grid[j].GetComponent<ObjectOutput>().reduceFood(red);
                            }
                        }


                    }
                        

                }
            }
            else //money effect
            {

                List<GameObject> grid = gameManager.GetComponent<GridScript>().GetGrid();
                float red = effects[i].GetReduction();

                if (effects[i].GetFillEffected() == ObjectFill.FillType.NONE)
                {
                    //effecting all
                    //reduce all current fields
                    for (int j = 0; j < grid.Count; j++)
                    {
                        if (grid[j].GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.FIELD)
                        {
                            grid[j].GetComponentInChildren<ObjectOutput>().reduceMoney(red);
                        }
                        else if (grid[j].GetComponent<ObjectFill>().GetFillType() != ObjectFill.FillType.NONE)
                        {
                            grid[j].GetComponent<ObjectOutput>().reduceMoney(red);
                        }
                    }

                }
                else
                {
                    //efecting specific

                    //reduce all current fields
                    for (int j = 0; j < grid.Count; j++)
                    {

                        if (grid[j].GetComponent<ObjectFill>().GetFillType() == effects[i].GetFillEffected())
                        {
                            if (grid[j].GetComponent<ObjectInfo>().GetObjectType() == ObjectInfo.ObjectType.FIELD)
                            {
                                grid[j].GetComponentInChildren<ObjectOutput>().reduceMoney(red);
                            }
                            else
                            {
                                grid[j].GetComponent<ObjectOutput>().reduceMoney(red);
                            }
                        }


                    }


                }
            }
        }
    }

}



public class GameEvent
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

    public void ConvertRequirement(int i, EventRequirementName convertTo)//convert requirement at i to passed requirement type
    {
        if (convertTo != EventRequirementName.FILL)
        {
            if (convertTo == EventRequirementName.FILL)
            {
                ObjectFill.FillType newFill = ObjectFill.FillType.NONE;
                eventRequirements.RemoveAt(i);
                eventRequirements.Insert(i, newFill);
            }
            else if (convertTo == EventRequirementName.SUB_REQ)
            {
                List<dynamic> newSub = new List<dynamic>();
                ObjectFill.FillType newFill = ObjectFill.FillType.NONE;
                newSub.Add(newFill);
                eventRequirements.RemoveAt(i);
                eventRequirements.Insert(i, newSub);

            }
            else
            {

                ValueMinOrMax newVal = new ValueMinOrMax(true, 0);
                newVal.SetRequirementType(convertTo);
                eventRequirements.RemoveAt(i);
                eventRequirements.Insert(i, newVal);

            }

        }


    }

    public void ConvertSubRequirement(int i, int j, EventRequirementName convertTo)//convert requirement at i,j to passed requirement type
    {
        if (convertTo == EventRequirementName.FILL)
        {
            ObjectFill.FillType newFill = ObjectFill.FillType.NONE;
            eventRequirements[i].RemoveAt(j);
            eventRequirements[i].Insert(j, newFill);
        }
        else
        {

            ValueMinOrMax newVal = new ValueMinOrMax(true, 0);
            newVal.SetRequirementType(convertTo);
            eventRequirements[i].RemoveAt(j);
            eventRequirements[i].Insert(j, newVal);

        }


    }

    public void ConnectNewRequirement()//connect with and 
    {
        if (eventRequirements.Count != 0)
        {
           //only add connection if not first req
            AddRequirement("AND");
           
        }

        ValueMinOrMax newVal = new ValueMinOrMax(true, 0);
        newVal.SetRequirementType(EventRequirementName.COUNT);
        AddRequirement(newVal);

    }

    public void DeleteRequirement(int i)
    {
        if (i != eventRequirements.Count - 1)
        {
            eventRequirements.RemoveAt(i);//remove req
            eventRequirements.RemoveAt(i);//remove connector
        }
        else if (eventRequirements.Count == 1)
        {
            eventRequirements.RemoveAt(i);//remove req
        }
        else
        {
            eventRequirements.RemoveAt(i);//remove req
            eventRequirements.RemoveAt(i - 1);//remove connector
        }
        

    }

    public void ConnectNewSubRequirement(int i)//connect new sub requirement at i 
    {
        eventRequirements[i].Add("AND");
        ValueMinOrMax newVal = new ValueMinOrMax(true, 0);
        newVal.SetRequirementType(EventRequirementName.COUNT);
        eventRequirements[i].Add(newVal);
    }

    public void DeleteSubRequirement(int i, int j)//delete requirement at i ,j
{
        if (eventRequirements[i].Count == 1)
        {
            DeleteRequirement(i);            
        }
        else if (j != eventRequirements[i].Count - 1)
        {
            eventRequirements[i].RemoveAt(j);//remove req
            eventRequirements[i].RemoveAt(j);//remove connector
        }
        else
        {
            eventRequirements[i].RemoveAt(j);//remove req
            eventRequirements[i].RemoveAt(j-1);//remove connector
        }

    }
}




/// <summary>
/// Allows a value to be checked
/// pass true to make it a min, false for a max
/// </summary>
public class ValueMinOrMax
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
public class ValueRange
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





public class EventEffects //effects of an effect
{
    //set inital values
    public EventEffects(float r,  ObjectFill.FillType f, EventEffectType t)
    {
        reduction = r;
        fillEffected = f;
        effectType = t;
    }

    //effects variables
    ObjectFill.FillType fillEffected;
    EventEffectType effectType;
    float reduction;

    //getters 
    public float GetReduction() { return reduction; }
    public EventEffectType GetEventEffectType() { return effectType; }
    public ObjectFill.FillType GetFillEffected() { return fillEffected; }

    //setters
    public void SetReduction(float r) { reduction = r; }
    public void SetFillEffected(ObjectFill.FillType f) { fillEffected= f; }
    public void SetEventEffectType(EventEffectType t) { effectType = t; }


}

public enum EventEffectType //effect types
{
    MONEY_EFFECT =0,
    FOOD_EFFECT = 1,
    DESTROY_RANDOM =2
}

public enum EventRequirementName //different requirement types
{
    FOOD = 0,
    SUSTAINABILITY = 1,
    TIME = 2,
    FILL=3,
    LEVEL =4,
    COUNT=5,
    RANDOM_CHANCE = 6,
    SUB_REQ = 7
}
