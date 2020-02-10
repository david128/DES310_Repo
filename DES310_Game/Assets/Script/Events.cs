using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    List<GameEvent> gameEvents = new List<GameEvent>(); //list of all gameEvents
    List<EventRequirement> currentLevels = new List<EventRequirement>(); //list of all current levels to compare to event requirements.
    EventEffects currentEventEffects = new EventEffects();

    

    void AddNewEvent(GameEvent gameEvent) //add new event to list of events
    {
        gameEvents.Add(gameEvent);
    }   

    void findCurrentLevels()
    {
        EventRequirement newRequirement = new EventRequirement();

        newRequirement.SetEventType(EventRequirementName.FOOD);
        newRequirement.SetMax(0); //replace with getter
        newRequirement.SetMin(0); //replace with getter

        currentLevels.Add(newRequirement);

        newRequirement.SetEventType(EventRequirementName.SUSTAINABILLITY);
        newRequirement.SetMax(0); //replace with getter
        newRequirement.SetMin(0); //replace with getter

        currentLevels.Add(newRequirement);

        newRequirement.SetEventType(EventRequirementName.TIME);
        newRequirement.SetMax(0); //replace with getter
        newRequirement.SetMin(0); //replace with getter

        currentLevels.Add(newRequirement);

    }
    
    void findCurrentLevelOfType(EventRequirementName type)
    {
        if(event)
    }

    void checkTrigger() //loop through gameEvents here and check for req
    {
        bool triggered = false;
        int count=0;
        while(triggered = false || count > gameEvents.Count)
        {
            if (gameEvents[count].getTriggered() == false)
            {

                List<EventRequirement> eventRequirements = gameEvents[count].getEventRequirements(); //requirements for this event
                for (int i = 0; i < eventRequirements.Count; i++)
                {
                    if ()
                    {

                    }
                }
            }

            count += 1;
        }
    }

}




class GameEvent
{
    bool triggered = false; //if the event has been triggered
    List<EventRequirement> eventRequirements = new List<EventRequirement>(); //requirements for triggering
    EventEffects effects;


    public bool getTriggered() { return triggered; }
    public List<EventRequirement> getEventRequirements() { return eventRequirements; }
    public EventEffects getEffects() { return effects; }

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
}

class EventRequirement
{
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
    float growthReduction;
    float sustainabillityReduction;
    float moneyReduction;

}





public enum EventRequirementName //different requirement types
{
    FOOD = 0,
    SUSTAINABILLITY = 1,
    TIME = 2
}
