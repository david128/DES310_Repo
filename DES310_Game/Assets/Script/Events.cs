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

    void FindCurrentLevels() //gets the current levels to be compared to requirements
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

    void checkTrigger() //loop through gameEvents here and check for req
    {
        bool triggered = false; // flag to check if an event has been triggered
        int count=0;
        while(triggered = false || count > gameEvents.Count)
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
                    currentEventEffects.AddEffects(gameEvents[count].getEffects());
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
    SUSTAINABILLITY = 1,
    TIME = 2
}
