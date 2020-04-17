using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsEditing : MonoBehaviour
{

    public Events events = new Events();
    public Dropdown EventsDropdown;
    public Text eventName;
    public Text eventDesc;
    List<GameEvent> gameEvents;

    public void LoadEventsFile()
    {
        List<Dropdown.OptionData> optionsData = new List<Dropdown.OptionData>();
        events.HandleEventFile();
        gameEvents= events.GetEvents();
        for (int i = 0; i < gameEvents.Count; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = gameEvents[i].getEVentName();
            optionsData.Add(option);
        }
        EventsDropdown.AddOptions(optionsData);
        

    }

    public void SelectEvent()
    {
        int val = EventsDropdown.value;
        if (val ==0)
        {
            //create new event
            eventName.text = ("Name:\n" );
            eventDesc.text = ("Desc:\n");
        }
        else
        {
            val = val - 1;
            eventName.text = ("Name: " + gameEvents[val].getEVentName());
            eventDesc.text = ("Desc: " + gameEvents[val].GetEventDescription());
        }


    }
    public void CreateNewEvent()
    {

    }

    public void WriteChanges()
    {

    }

    
    
    

}
