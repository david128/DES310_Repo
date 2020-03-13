using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class Events : MonoBehaviour
{
    string path = "GameData/events.txt";

    List<GameEvent> gameEvents = new List<GameEvent>(); //list of all gameEvents
    List<EventRequirement> currentLevels = new List<EventRequirement>(); //list of all current levels to compare to event requirements.


    List<EventEffects> currentEventEffects = new List<EventEffects>();
    
    void setUpEvents()
    {
        currentEventEffects.Add(new EventEffects(0, 0, 0, ObjectFill.FillType.NONE));

    }
    

    
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

        }
        else if (req == EventRequirementName.FOOD)
        {

        }
        else if (req == EventRequirementName.SUSTAINABILITY)
        {

        }
        else if (req == EventRequirementName.TIME)
        {

        }
        return 100;
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
        
        string[] lines = File.ReadAllLines(path);
        int i = 0;
        
        bool eventDone = false;

        while ((i + 1)< lines.Length)
        {    
            GameEvent newGameEvent = new GameEvent();
            EventRequirement newReq = new EventRequirement();
            ValueMinOrMax minValue = new ValueMinOrMax(true, 0);
            ValueMinOrMax maxValue = new ValueMinOrMax(false, 0);
            ValueRange rangeValue = new ValueRange();
            EventEffects newEffects = new EventEffects(0, 0, 0,ObjectFill.FillType.NONE);
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
                    case ("EFFECTING"):
                        i += 1;
                        ObjectFill.FillType e;
                        if (ObjectFill.FillType.TryParse((lines[i]).ToUpper(), out e) != true) { Debug.LogError("Error: Effecting type does not match fill enum in events file"); } //check that this passes
                        newEffects.SetFillEffected(e);
                        break;

                    case ("GROWTH_REDUCTION"):
                        i += 1;
                        newEffects.SetGrowthReduction(float.Parse(lines[i]));
                        break;

                    case ("MONEY_REDUCTION"):
                        i += 1;

                        newEffects.SetMoneyReduction(float.Parse(lines[i]));
                        break;
                    case ("SUSTAINABILITY_REDUCTION"):
                        i += 1;
                        newEffects.SetSustainabillityReduction(float.Parse(lines[i]));
                        break;
                    case ("EFFECT_END"):
                        newGameEvent.AddEfffect(newEffects);
                        newEffects.ClearEffects();
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
    public EventEffects(float g, float s, float m, ObjectFill.FillType f)
    {
        growthReduction = g;
        sustainabillityReduction = s;
        moneyReduction = m;
        fillEffected = f;
    }

    ObjectFill.FillType fillEffected;
    float growthReduction;
    float sustainabillityReduction;
    float moneyReduction;

    public float GetGrowthReduction() { return growthReduction; }
    public float GetSustainabillityReduction() { return sustainabillityReduction; }
    public float GetMoneyReduction() { return moneyReduction; }
    public ObjectFill.FillType GetFillEffected() { return fillEffected; }

    public void SetGrowthReduction(float g) { growthReduction = g; }
    public void SetSustainabillityReduction(float s) { sustainabillityReduction = s; }
    public void SetMoneyReduction(float m) { moneyReduction = m; }
    public void SetFillEffected(ObjectFill.FillType f) { fillEffected= f; }

    public void AddEffects(EventEffects e)
    {
        growthReduction += e.growthReduction;
        sustainabillityReduction += e.sustainabillityReduction;
        moneyReduction += e.moneyReduction;
    }

    public void ClearEffects()//clear effects
    {
        growthReduction = 0.0f;
        sustainabillityReduction = 0.0f;
        moneyReduction = 0.0f;
        fillEffected = ObjectFill.FillType.NONE;
    }

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
