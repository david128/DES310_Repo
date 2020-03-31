using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventsEditing : MonoBehaviour
{

    public Events events = new Events();
    public Dropdown EventsDropdown;

    public void LoadEventsFile()
    {
        List<Dropdown.OptionData> optionsData = new List<Dropdown.OptionData>();
        events.HandleEventFile();
        List<GameEvent> gameEvents = events.GetEvents();
        for (int i = 0; i < gameEvents.Count; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = gameEvents[i].getEVentName();
            optionsData.Add(option);
        }
        EventsDropdown.AddOptions(optionsData);
        

    }

    public void CreateNewEvent()
    {

    }

    public void WriteChanges()
    {

    }

    
    
    

}
