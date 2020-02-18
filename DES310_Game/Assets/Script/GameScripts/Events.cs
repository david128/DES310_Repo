using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class Events : MonoBehaviour
{
    string path = "Assets/Resources/events.txt";

    List<GameEvent> gameEvents = new List<GameEvent>(); //list of all gameEvents
    List<EventRequirement> currentLevels = new List<EventRequirement>(); //list of all current levels to compare to event requirements.
    EventEffects currentEventEffects = new EventEffects(0,0,0);

    void SetUpGameEvents()
    {
        GameEvent event1 = new GameEvent();
        event1.addRequirement(new EventRequirement(EventRequirementName.FOOD, 150, 150));
        event1.addRequirement(new EventRequirement(EventRequirementName.SUSTAINABILITY, 150, 150));
        event1.addRequirement(new EventRequirement(EventRequirementName.TIME, 150, 150));

        event1.setEffects(new EventEffects(0,0,0));

        AddNewEvent(event1);
    }
     
    void AddNewEvent(GameEvent gameEvent) //add new event to list of events
    {
        gameEvents.Add(gameEvent);
    }   

    void FindCurrentLevels() //gets the current levels to be compared to requirements
    {
        EventRequirement newRequirement = new EventRequirement();

        newRequirement.SetEventType(EventRequirementName.FOOD);
        newRequirement.SetMax(0); //replace with getter
        newRequirement.SetMin(0); //replace with getter

        currentLevels.Add(newRequirement);

        newRequirement.SetEventType(EventRequirementName.SUSTAINABILITY);
        newRequirement.SetMax(0); //replace with getter
        newRequirement.SetMin(0); //replace with getter

        currentLevels.Add(newRequirement);

        newRequirement.SetEventType(EventRequirementName.TIME);
        newRequirement.SetMax(0); //replace with getter
        newRequirement.SetMin(0); //replace with getter

        currentLevels.Add(newRequirement);
    }
    
    EventRequirement FindCurrentLevelOfType(EventRequirementName type) //finds the level for requirement as given
    {
        for (int i = 0; i < currentLevels.Count; i++)
        {
            if (currentLevels[i].getType() == type)
            {
                return (currentLevels[i]);
            }
        }
        return (currentLevels[0]);
    }

    public void checkTrigger() //loop through gameEvents here and check for req
    {
        FindCurrentLevels();
        bool triggered = false; // flag to check if an event has been triggered
        int count=0;
        while(triggered == false && count < gameEvents.Count)
        {
            if (gameEvents[count].getTriggered() == false) //if the event has not been triggered then it will be checked for
            {

                List<EventRequirement> eventRequirements = gameEvents[count].getEventRequirements(); //requirements for this event
                triggered = true;//sets to true initially

                for (int i = 0; i < eventRequirements.Count; i++) //check each requirement
                {
                    
                    EventRequirement comparison = FindCurrentLevelOfType(eventRequirements[i].getType()); //finds the level to be compared to

                    if (eventRequirements[i].getMin() >= comparison.getMin() && eventRequirements[i].getMax() <= comparison.getMax()) //if requirements are not met the set to false
                    {
                        triggered = false;
                    }

                }

                if (triggered == true) //if triggered is still true then add effects of this event
                {
                    Debug.Log("EventOccurred " + gameEvents[count].getEVentName());
                    gameEvents[count].setTriggered(true);
                    currentEventEffects.AddEffects(gameEvents[count].getEffects());
                }

            }

            count += 1;
        }
    }

    public enum textIdentifier
    {
        EVENTNAME,
        FOOD,
        SUSTAINABILITY,
        TIME,
        MONEY_EFFECT,
        SUST_EFFECT,
        FOOD_EFFECT          
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
            EventEffects newEffects = new EventEffects(0, 0, 0);
            eventDone = false;

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

                    case ("FOOD"):
                        newReq.SetEventType(EventRequirementName.FOOD);
                        i += 1;
                        newReq.SetMin(float.Parse(lines[i]));
                        i += 1;
                        newReq.SetMax(float.Parse(lines[i]));
                        newGameEvent.addRequirement(newReq);
                        break;

                    case ("SUSTAINABILLITY"):
                        newReq.SetEventType(EventRequirementName.SUSTAINABILITY);
                        i += 1;
                        newReq.SetMin(float.Parse(lines[i]));
                        i += 1;
                        newReq.SetMax(float.Parse(lines[i]));
                        newGameEvent.addRequirement(newReq);
                        break;

                    case ("TIME"):
                        newReq.SetEventType(EventRequirementName.TIME);
                        i += 1;
                        newReq.SetMin(float.Parse(lines[i]));
                        i += 1;
                        newReq.SetMax(float.Parse(lines[i]));
                        newGameEvent.addRequirement(newReq);
                        break;

                    case ("GROWTH_REDUCTION"):
                        i += 1;
                        newEffects.SetGrowthReduction(float.Parse(lines[i]));
                        break;

                    case ("MONEY_REDUCTION"):
                        i += 1;

                        newEffects.SetGrowthReduction(float.Parse(lines[i]));
                        break;
                    case ("SUSTAINABILITY_REDUCTION"):
                        i += 1;

                        newEffects.SetGrowthReduction(float.Parse(lines[i]));
                        break;

                    case ("END"):
                        newGameEvent.setEffects(newEffects);
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
    List<EventRequirement> eventRequirements = new List<EventRequirement>(); //requirements for triggering
    EventEffects effects;
    string eventName;
    string eventDescription;

    public bool getTriggered() { return triggered; }
    public List<EventRequirement> getEventRequirements() { return eventRequirements; }
    public EventEffects getEffects() { return effects; }
    public string getEVentName() { return eventName; }
    public string GetEventDescription() { return eventDescription; }


    public void addRequirement(EventRequirement eventRequirement) //add a new trigger requirement
    {
        eventRequirements.Add(eventRequirement);
    }

    public void setEffects(EventEffects eventEffects) //set effects of event
    {
        effects = eventEffects;
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
    public EventRequirement(EventRequirementName eventType, float minValue, float maxValue)
    {
        type = eventType;
        min = minValue;
        max = maxValue;
    }
    public EventRequirement() { }

    EventRequirementName type; //type of requirement
    float min; //max requirement
    float max; //min requirement

    public EventRequirementName getType() { return type; }
    public float getMin() { return min; }
    public float getMax() { return max; }

    public void SetEventType(EventRequirementName t) { type = t; }
    public void SetMin(float m) { min = m; }
    public void SetMax(float m) { max = m; }
}

class EventEffects //effects of an effect
{
    public EventEffects(float g, float s, float m)
    {
        growthReduction = g;
        sustainabillityReduction = s;
        moneyReduction = m;
    }

    float growthReduction;
    float sustainabillityReduction;
    float moneyReduction;

    public float GetGrowthReduction() { return growthReduction; }
    public float GetSustainabillityReduction() { return sustainabillityReduction; }
    public float GetMoneyReduction() { return moneyReduction; }

    public void SetGrowthReduction(float g) { growthReduction = g; }
    public void SetSustainabillityReduction(float s) { sustainabillityReduction = s; }
    public void SetMoneyReduction(float m) { moneyReduction = m; }

    public void AddEffects(EventEffects e)
    {
        growthReduction += e.growthReduction;
        sustainabillityReduction += e.sustainabillityReduction;
        moneyReduction += e.moneyReduction;
    }

}

public enum EventRequirementName //different requirement types
{
    FOOD = 0,
    SUSTAINABILITY = 1,
    TIME = 2
}
