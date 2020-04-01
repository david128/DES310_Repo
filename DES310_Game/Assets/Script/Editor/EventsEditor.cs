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
    List<int> reqIndex = new List<int>();
    List<int> subReqIndex = new List<int>();
    List<float> reqValues = new List<float>();
    List<float> subReqValues = new List<float>();
    List<EventRequirementName> requirementNames = new List<EventRequirementName>();

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
        if (GUILayout.Button("Load File") == true)
        {
            prevIndex = 100;
            initialLoad = true;
            GameObject GameManager = GameObject.FindGameObjectWithTag("GameController");
            events = GameManager.GetComponent<Events>();
            events.HandleEventFile();
            gameEvents = events.GetEvents();
            index = 0;

            for (int i = 0; i < gameEvents.Count; i++)
            {
                eventNames.Add(gameEvents[i].getEVentName());
                 
            }
            name = gameEvents[index].getEVentName();
            desc = gameEvents[index].GetEventDescription();
            requirements = gameEvents[index].getEventRequirements();
            effects = gameEvents[index].getEffects();

            

        }

        prevIndex = index;
        index = EditorGUILayout.Popup(index, eventNames.ToArray());

        if (initialLoad)
        {
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
                        EventRequirementName selected  = (EventRequirementName) reqIndex[i];
                        EventRequirementName temp;
                        temp = EditorGUILayout.EnumPopup(selected);
                        EditorGUILayout.TextField("Min Value", requirements[i].GetValue().ToString());



                        using (new GUILayout.HorizontalScope())
                        {
                            GUILayout.Space(35f); // horizontal indent size of 35 (pixels)

                            GUILayout.Button("Delete", GUILayout.Width(50));
                        }

                        //Rect lastRect = GUILayoutUtility.GetLastRect();
                        //Rect buttonRect = new Rect(lastRect.x, lastRect.y + EditorGUIUtility.singleLineHeight, 100, 30);

                        //if (GUI.Button(buttonRect, "Delete"))
                        //{

                        //}
                    }
                    else
                    {
                        reqIndex[i] = (int)EditorGUILayout.EnumPopup(requirements[i].GetRequirementType());
                        EditorGUILayout.TextField("Max Value", requirements[i].GetValue().ToString());
                        GUILayout.Button("Delete");
                    }

                }
                else if (requirements[i] is string)
                {
                    if (requirements[i] == "AND")
                    {
                        EditorGUILayout.EnumPopup(Connection.AND);
                        reqIndex[i] = 0;
                    }
                    else
                    {
                        EditorGUILayout.EnumPopup(Connection.OR);
                        reqIndex[i] = 1;
                    }


                }
                else
                {

                    reqIndex[i] = 7;
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

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(35f); // horizontal indent size of 20 (pixels)

                GUILayout.Button("AddNew");
            }



            //effects
            GUILayout.Label("Effects", EditorStyles.boldLabel);
        }

        if (prevIndex != index)
        {
            //reload 
            name = gameEvents[index].getEVentName();
            desc = gameEvents[index].GetEventDescription();
            requirements = gameEvents[index].getEventRequirements();
            effects = gameEvents[index].getEffects();

            name = EditorGUILayout.TextField("Name", name);
            desc = EditorGUILayout.TextField("Description", desc);

            EditorGUI.indentLevel++; //lvl 1

            //requirements
            GUILayout.Label("Requirements", EditorStyles.boldLabel);

            for (int i = 0; i < requirements.Count; i++)
            {
                GUILayout.Label("", EditorStyles.label);




                reqIndex.Add(0);
                EditorGUI.indentLevel++; //lvl 2

                if (requirements[i] is ValueMinOrMax)
                {

                    if (requirements[i].GetMin() == true)
                    {

                        reqIndex[i] = (int)EditorGUILayout.EnumPopup(requirements[i].GetRequirementType());
                        EditorGUILayout.TextField("Min Value", requirements[i].GetValue().ToString());



                        using (new GUILayout.HorizontalScope())
                        {
                            GUILayout.Space(35f); // horizontal indent size of 20 (pixels)

                            GUILayout.Button("Delete", GUILayout.Width(50));
                        }

                        //Rect lastRect = GUILayoutUtility.GetLastRect();
                        //Rect buttonRect = new Rect(lastRect.x, lastRect.y + EditorGUIUtility.singleLineHeight, 100, 30);

                        //if (GUI.Button(buttonRect, "Delete"))
                        //{

                        //}
                    }
                    else
                    {
                        reqIndex[i] = (int)EditorGUILayout.EnumPopup(requirements[i].GetRequirementType());
                        EditorGUILayout.TextField("Max Value", requirements[i].GetValue().ToString());
                        GUILayout.Button("Delete");
                    }

                }
                else if (requirements[i] is string)
                {
                    if (requirements[i] == "AND")
                    {
                        EditorGUILayout.EnumPopup(Connection.AND);
                        reqIndex[i] = 0;
                    }
                    else
                    {
                        EditorGUILayout.EnumPopup(Connection.OR);
                        reqIndex[i] = 1;
                    }


                }
                else
                {

                    reqIndex[i] = 7;
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

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(35f); // horizontal indent size of 20 (pixels)

                GUILayout.Button("AddNew");
            }



            //effects
            GUILayout.Label("Effects", EditorStyles.boldLabel);
        }
        
        
    }


}

public enum Connection
{

    AND= 0,
    OR =1

}

