using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventsEditor : EditorWindow
{
    public Events events = new Events();
    bool initialLoad = false;
    List<string> eventNames = new List<string>();
    List<GameEvent> gameEvents;
    List<dynamic> requirements = new List<dynamic>();
    List<EventEffects> effects = new List<EventEffects>();
    

    string name = "";
    string desc = "";

    int index = 0;
    
    int prevIndex = 0;

    [MenuItem("Tools/Events Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EventsEditor));
    }

    public void OnGUI()
    {
        GUILayout.Label("Events", EditorStyles.boldLabel);

        //load file button
        if (GUILayout.Button("Load File") == true)
        {
            prevIndex = 100;
            initialLoad = true;
            GameObject GameManager = GameObject.FindGameObjectWithTag("GameController");
            events = GameManager.GetComponent<Events>();
            events.HandleEventFile();
            gameEvents = events.GetEvents();
            index = 0;

            eventNames.Clear();
            for (int i = 0; i < gameEvents.Count; i++)
            {
                
                eventNames.Add(gameEvents[i].getEVentName());
                 
            }
            name = gameEvents[index].getEVentName();
            desc = gameEvents[index].GetEventDescription();
            requirements = gameEvents[index].getEventRequirements();
            effects = gameEvents[index].getEffects();

            

        }

        //if event selection is changed then refresh.
        if (prevIndex != index)
        {
            //reload 
            name = gameEvents[index].getEVentName();
            desc = gameEvents[index].GetEventDescription();
            effects = gameEvents[index].getEffects();
            requirements = gameEvents[index].getEventRequirements();

            name = EditorGUILayout.TextField("Name", name);
            desc = EditorGUILayout.TextField("Description", desc);

            EditorGUI.indentLevel++; //lvl 1

            //requirements
            GUILayout.Label("Requirements", EditorStyles.boldLabel);

            for (int i = 0; i < requirements.Count; i++)
            {
                GUILayout.Label("", EditorStyles.label);

                
                EditorGUI.indentLevel++; //lvl 2

                if (requirements[i] is ValueMinOrMax)
                {

                    if (requirements[i].GetMin() == true)
                    {

                        EditorGUILayout.EnumPopup(requirements[i].GetRequirementType());
                        EditorGUILayout.TextField("Min Value", requirements[i].GetValue().ToString());

                        
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(35f); // horizontal indent size of 20 (pixels)


                        if (GUILayout.Button("Delete Requirement", GUILayout.Width(200)))
                        {
                            gameEvents[index].DeleteRequirement(i);
                        }
                        
                        GUILayout.EndHorizontal();

                    }
                    else
                    {
                        EditorGUILayout.EnumPopup(requirements[i].GetRequirementType());
                        EditorGUILayout.TextField("Max Value", requirements[i].GetValue().ToString());
                        if (GUILayout.Button("Delete Requirement", GUILayout.Width(200)))
                        {
                            gameEvents[index].DeleteRequirement(i);
                        }
                    }

                }
                else if (requirements[i] is string)
                {
                    if (requirements[i] == "AND")
                    {
                        EditorGUILayout.EnumPopup(Connection.AND);
                    }
                    else
                    {
                        EditorGUILayout.EnumPopup(Connection.OR);
                    }


                }
                else
                {

                    EditorGUILayout.EnumPopup(EventRequirementName.SUB_REQ);

                    for (int j = 0; j < requirements[i].Count; j++)
                    {
                        EditorGUI.indentLevel++; //lvl 2
                        GUILayout.Label("", EditorStyles.label);
                        if (requirements[i][j] is ValueMinOrMax)
                        {

                            if (requirements[i][j].GetMin() == true)
                            {

                                EditorGUILayout.EnumPopup(requirements[i][j].GetRequirementType());
                                EditorGUILayout.TextField("Min Value", requirements[i][j].GetValue().ToString());
                            }
                            else
                            {
                                EditorGUILayout.EnumPopup(requirements[i][j].GetRequirementType());
                                EditorGUILayout.TextField("Max Value", requirements[i][j].GetValue().ToString());
                            }

                        }
                        else if (requirements[i][j] is ObjectFill.FillType)
                        {
                            EditorGUILayout.EnumPopup(EventRequirementName.FILL);
                            EditorGUILayout.EnumPopup(requirements[i][j]);
                        }
                        else if (requirements[i][j] is string)
                        {

                            if (requirements[i][j] == "AND")
                            {
                                EditorGUILayout.EnumPopup(Connection.AND);
                            }
                            else
                            {
                                EditorGUILayout.EnumPopup(Connection.OR);
                            }

                        }

                        EditorGUI.indentLevel--; //lvl 1

                    }
                }
                EditorGUI.indentLevel--; //lvl 1

            }
            EditorGUI.indentLevel--; //lvl 0

           
                

                GUILayout.Button("Add New Requirement");
            



            //effects
            GUILayout.Label("Effects", EditorStyles.boldLabel);
        }

        //set index to prev index so nothing is updated until event is chaanged
        prevIndex = index;
        index = EditorGUILayout.Popup(index, eventNames.ToArray());

        //after init load display the following
        if (initialLoad)
        {
            gameEvents[index].setEventName(EditorGUILayout.TextField("Name", gameEvents[index].getEVentName()));
            gameEvents[index].setEventDescription(EditorGUILayout.TextField("Description", gameEvents[index].GetEventDescription()));
            

            EditorGUI.indentLevel++; //lvl 1

            //requirements
            GUILayout.Label("Requirements", EditorStyles.boldLabel);

            //display all requirements
            for (int i = 0; i < requirements.Count; i++)
            {
                GUILayout.Label("", EditorStyles.label);
                                               
                EditorGUI.indentLevel++; //lvl 2

                if (requirements[i] is ValueMinOrMax)
                {

                    if (requirements[i].GetMin() == true)
                    {
                        
                        EventRequirementName selected = (EventRequirementName)EditorGUILayout.EnumPopup(requirements[i].GetRequirementType());
                        requirements[i].SetValue(float.Parse(EditorGUILayout.TextField("Min Value", requirements[i].GetValue().ToString())));


                        if (GUILayout.Button("Delete Requirement", GUILayout.Width(200)))
                        {
                            gameEvents[index].DeleteRequirement(i);
                        }

                        if (selected!= requirements[i].GetRequirementType())
                        {
                            gameEvents[index].ConvertRequirement(i, selected);
                        }
                    }   
                    else
                    {

                        EventRequirementName selected = (EventRequirementName)EditorGUILayout.EnumPopup(requirements[i].GetRequirementType());
                        requirements[i].SetValue(float.Parse(EditorGUILayout.TextField("Max Value", requirements[i].GetValue().ToString())));


                        if (GUILayout.Button("Delete Requirement", GUILayout.Width(200)))
                        {
                            gameEvents[index].DeleteRequirement(i);
                        }

                        if (selected != requirements[i].GetRequirementType())
                        {
                            gameEvents[index].ConvertRequirement(i, selected);
                        }
                    }

                }
                else if (requirements[i] is string)
                {
                    Connection selected;

                    if (requirements[i] == "AND")
                    {
                        selected = Connection.AND;
                    }
                    else
                    {
                        selected = Connection.AND;
                    }
                   
                    selected = (Connection)EditorGUILayout.EnumPopup(selected);

                    if (selected == Connection.AND)
                    {
                        requirements[i] = "AND";
                    }
                    else
                    {
                        requirements[i] = "OR";
                    }
                   
                }
                else
                {

                    EventRequirementName selected = (EventRequirementName)EditorGUILayout.EnumPopup(EventRequirementName.SUB_REQ);
                    
                    

                    for (int j = 0; j < requirements[i].Count; j++)
                    {
                        EditorGUI.indentLevel++; //lvl 2
                        GUILayout.Label("", EditorStyles.label);
                        if (requirements[i][j] is ValueMinOrMax)
                        {

                            if (requirements[i][j].GetMin() == true)
                            {

                                EditorGUILayout.EnumPopup(requirements[i][j].GetRequirementType());
                                EditorGUILayout.TextField("Min Value", requirements[i][j].GetValue().ToString());
                            }
                            else
                            {
                                EditorGUILayout.EnumPopup(requirements[i][j].GetRequirementType());
                                EditorGUILayout.TextField("Max Value", requirements[i][j].GetValue().ToString());
                            }

                        }
                        else if (requirements[i][j] is ObjectFill.FillType)
                        {
                            EditorGUILayout.EnumPopup(EventRequirementName.FILL);
                            EditorGUILayout.EnumPopup(requirements[i][j]);
                        }
                        else if (requirements[i][j] is string)
                        {

                            if (requirements[i][j] == "AND")
                            {
                                EditorGUILayout.EnumPopup(Connection.AND);
                            }
                            else
                            {
                                EditorGUILayout.EnumPopup(Connection.OR);
                            }

                        }

                        if (GUILayout.Button("Delete SubRequirement", GUILayout.Width(200)));
                        {
                            //gameEvents[index].DeleteSubRequirement(i, j);
                        }
                        
                        EditorGUI.indentLevel--; //lvl 1

                    }

                    //if (GUILayout.Button("Add New SubRequirement"))
                    //{
                    //    gameEvents[index].ConnectNewSubRequirement(i);
                    //}

                    if (selected != EventRequirementName.SUB_REQ)
                    {
                        gameEvents[index].ConvertRequirement(i, selected);
                    }
                }
                EditorGUI.indentLevel--; //lvl 1

            }
            EditorGUI.indentLevel--; //lvl 0

            //GUILayout.BeginHorizontal();
            //GUILayout.Space(35f); // horizontal indent size of 20 (pixels)

            if (GUILayout.Button("Add New Requirement"))
            {
                gameEvents[index].ConnectNewRequirement();
            }
            
            //GUILayout.EndHorizontal();


            //effects
            GUILayout.Label("Effects", EditorStyles.boldLabel);
        }

       
        
        
    }


}

class subReqStorage 
{
    public int parent;
    public int index;
}

public enum Connection
{

    AND= 0,
    OR =1

}

