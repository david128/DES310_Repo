﻿using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class EventsEditor : EditorWindow
{
    public Events events = new Events();
    bool initialLoad = false;
    List<string> eventNames = new List<string>();
    List<GameEvent> gameEvents;
    List<dynamic> requirements = new List<dynamic>();
    List<EventEffects> effects = new List<EventEffects>();
    int delete = 100;
    int convert = 100;
    EventRequirementName convertTo;
    int[] deleteSub = { 100, 100 };
    int[] convertSub =  { 100 ,100};
    EventRequirementName convertSubTo;
    

    string name = "";
    string desc = "";

    int index = 0;

    int prevIndex = 0;

    Vector2 sPos;

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

            EditorGUILayout.BeginVertical();

            sPos = GUILayout.BeginScrollView(sPos, true, true, GUILayout.Width(100), GUILayout.Height(100));

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
                            QueueDelete(i);
                        }

                        GUILayout.EndHorizontal();

                    }
                    else
                    {
                        EditorGUILayout.EnumPopup(requirements[i].GetRequirementType());
                        EditorGUILayout.TextField("Max Value", requirements[i].GetValue().ToString());
                        if (GUILayout.Button("Delete Requirement", GUILayout.Width(200)))
                        {
                            QueueDelete(i);
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

            for (int i = 0; i < effects.Count; i++)
            {
                GUILayout.Label("", EditorStyles.label);

                if (effects[i].GetEventEffectType() != EventEffectType.DESTROY_RANDOM)
                {
                    EditorGUILayout.EnumPopup(effects[i].GetEventEffectType());
                    EditorGUILayout.EnumPopup(effects[i].GetFillEffected());
                    EditorGUILayout.TextField("Reduction", effects[i].GetReduction().ToString());
                }
                else
                {
                    EditorGUILayout.EnumPopup(effects[i].GetEventEffectType());
                    EditorGUILayout.EnumPopup(effects[i].GetFillEffected());
                }

            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        //set index to prev index so nothing is updated until event is chaanged
        prevIndex = index;
        index = EditorGUILayout.Popup(index, eventNames.ToArray());

        if (GUILayout.Button("Add New Event"))
        {
            GameEvent newGameEvent = new GameEvent();
            
            newGameEvent.setEventName("Event" + gameEvents.Count.ToString());
            newGameEvent.setEventDescription("Description of Event" + gameEvents.Count.ToString());
            gameEvents.Add(newGameEvent);
            
        }

        if (GUILayout.Button("Write Changes"))
        {
            if (initialLoad == true)
            {
                WriteChanges();
            }
            
        }

        //after init load display the following
        if (initialLoad)
        {
            if (GUILayout.Button("Delete Event"))
            {
                gameEvents.RemoveAt(index);
                index = 0;
                if (gameEvents.Count == 0)
                {
                    GameEvent newGameEvent = new GameEvent();

                    newGameEvent.setEventName("Event" + gameEvents.Count.ToString());
                    newGameEvent.setEventDescription("Description of Event" + gameEvents.Count.ToString());
                    gameEvents.Add(newGameEvent);
                    prevIndex = 200;
                }
            }

            eventNames.Clear();
            for (int i = 0; i < gameEvents.Count; i++)
            {

                eventNames.Add(gameEvents[i].getEVentName());

            }

            gameEvents[index].setEventName(EditorGUILayout.TextField("Name", gameEvents[index].getEVentName()));
            gameEvents[index].setEventDescription(EditorGUILayout.TextField("Description", gameEvents[index].GetEventDescription()));

            EditorGUILayout.BeginVertical();
            
            sPos = GUILayout.BeginScrollView(sPos, true, true);


            EditorGUI.indentLevel++; //lvl 1

            //requirements
            GUILayout.Label("Requirements", EditorStyles.boldLabel);
            if (requirements.Count > 0)
            {
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
                                QueueDelete(i);
                            }

                            if (selected != requirements[i].GetRequirementType())
                            {
                                QueueConvert(i, selected);
                            }
                        }
                        else
                        {

                            EventRequirementName selected = (EventRequirementName)EditorGUILayout.EnumPopup(requirements[i].GetRequirementType());
                            requirements[i].SetValue(float.Parse(EditorGUILayout.TextField("Max Value", requirements[i].GetValue().ToString())));


                            if (GUILayout.Button("Delete Requirement", GUILayout.Width(200)))
                            {
                                QueueDelete(i);
                            }

                            if (selected != requirements[i].GetRequirementType())
                            {
                                QueueConvert(i, selected);
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
                            selected = Connection.OR;
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
                    else if (requirements[i] is ObjectFill.FillType)
                    {
                        EditorGUILayout.EnumPopup(EventRequirementName.FILL);
                        EditorGUILayout.EnumPopup(requirements[i]);
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
                                EventRequirementName subSelected;
                                if (requirements[i][j].GetMin() == true)
                                {

                                    subSelected = EditorGUILayout.EnumPopup(requirements[i][j].GetRequirementType());
                                    requirements[i][j].SetValue(float.Parse(EditorGUILayout.TextField("Min Value", requirements[i][j].GetValue().ToString())));
                                }
                                else
                                {
                                    subSelected = EditorGUILayout.EnumPopup(requirements[i][j].GetRequirementType());
                                    requirements[i][j].SetValue(float.Parse(EditorGUILayout.TextField("Max Value", requirements[i][j].GetValue().ToString())));
                                }

                                if (subSelected != requirements[i][j].GetRequirementType())
                                {
                                    QueueConvertSub(i,j, subSelected);
                                }

                                if (GUILayout.Button("Delete SubRequirement", GUILayout.Width(250)))
                                {
                                    QueueDeleteSub(i, j);
                                }

                            }
                            else if (requirements[i][j] is ObjectFill.FillType)
                            {
                                EventRequirementName subSelected = (EventRequirementName)EditorGUILayout.EnumPopup(EventRequirementName.FILL);
                                requirements[i][j] = EditorGUILayout.EnumPopup(requirements[i][j]);

                                if (GUILayout.Button("Delete SubRequirement", GUILayout.Width(250)))
                                {
                                    QueueDeleteSub(i, j);
                                }


                                if (subSelected != EventRequirementName.FILL)
                                {
                                    QueueConvertSub(i, j, subSelected);
                                }
                            }
                            else if (requirements[i][j] is string)
                            {

                                Connection selectedSub;

                                if (requirements[i][j] == "AND")
                                {
                                    selectedSub = Connection.AND;
                                }
                                else
                                {
                                    selectedSub = Connection.OR;
                                }

                                selectedSub = (Connection)EditorGUILayout.EnumPopup(selectedSub);

                                if (selectedSub == Connection.AND)
                                {
                                    requirements[i][j] = "AND";
                                }
                                else
                                {
                                    requirements[i][j] = "OR";
                                }

                            }



                            EditorGUI.indentLevel--; //lvl 1

                        }

                        //if (GUILayout.Button("Add New SubRequirement"))
                        //{
                        //    gameEvents[index].ConnectNewSubRequirement(i);
                        //}

                        if (GUILayout.Button("Add New Sub Requirement"))
                        {
                            gameEvents[index].ConnectNewSubRequirement(i);
                        }

                        if (selected != EventRequirementName.SUB_REQ)
                        {
                            QueueConvert(i, selected);
                        }
                    }
                    EditorGUI.indentLevel--; //lvl 1

                }
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

            for (int i = 0; i < effects.Count; i++)
            {
                GUILayout.Label("", EditorStyles.label);

                if (effects[i].GetEventEffectType() != EventEffectType.DESTROY_RANDOM)
                {
                    effects[i].SetEventEffectType((EventEffectType)EditorGUILayout.EnumPopup(effects[i].GetEventEffectType()));
                    effects[i].SetFillEffected((ObjectFill.FillType)EditorGUILayout.EnumPopup(effects[i].GetFillEffected()));
                    effects[i].SetReduction(float.Parse(EditorGUILayout.TextField("Reduction", effects[i].GetReduction().ToString())));
                }
                else
                {
                    effects[i].SetEventEffectType((EventEffectType)EditorGUILayout.EnumPopup(effects[i].GetEventEffectType()));
                    effects[i].SetFillEffected((ObjectFill.FillType)EditorGUILayout.EnumPopup(effects[i].GetFillEffected()));
                    effects[i].SetReduction(0);
                }

                if (GUILayout.Button("Delete Effect"))
                {
                    effects.RemoveAt(i);
                }


            }

            if (GUILayout.Button("Add New Effect"))
            {
                EventEffects newEffect = new EventEffects(0, ObjectFill.FillType.NONE, EventEffectType.MONEY_EFFECT);
                effects.Add(newEffect);
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        if (delete != 100)
        {
            gameEvents[index].DeleteRequirement(delete);
            delete = 100;
        }

        if (deleteSub[0] != 100 )
        {
            gameEvents[index].DeleteSubRequirement(deleteSub[0], deleteSub[1]);
            deleteSub[0] = 100;
            deleteSub[1] = 100;
        }

        if (convert != 100)
        {
            gameEvents[index].ConvertRequirement(convert, convertTo);
            convert = 100;
        }

        if (convertSub[0] != 100)
        {
            gameEvents[index].ConvertSubRequirement(convertSub[0],convertSub[1], convertSubTo);
            convertSub [0] = 100;
            convertSub [1] = 100;
        }


        void WriteChanges()
        {
            string path = "Assets/Resources/files/events.txt";

            File.WriteAllText(path, "");


            
            for (int index = 0; index < gameEvents.Count; index++)
            {
                File.AppendAllText(path, "EVENTNAME\n");
                File.AppendAllText(path, gameEvents[index].getEVentName().ToString() + "\n");

                File.AppendAllText(path, "DESCRIPTION\n");
                File.AppendAllText(path, gameEvents[index].GetEventDescription().ToString() + "\n");

                List<dynamic> reqs = gameEvents[index].getEventRequirements();
                for (int i = 0; i < reqs.Count; i++)
                {
                    
                    
                    if (reqs[i] is ValueMinOrMax)
                    {

                        if (reqs[i].GetRequirementType() == EventRequirementName.TIME)
                        {
                            //write time min
                            File.AppendAllText(path, "TIME_MIN\n");

                        }
                        else if (reqs[i].GetRequirementType() == EventRequirementName.LEVEL)
                        {
                            //write lvl
                            File.AppendAllText(path, "LVL\n");
                        }
                        else
                        {
                            //write enum
                            File.AppendAllText(path, reqs[i].GetRequirementType().ToString() + "\n");
                        }

                        //write val
                        File.AppendAllText(path, reqs[i].GetValue().ToString() + "\n");

                    }
                    else if (reqs[i] is string)
                    {
                        // write value
                        File.AppendAllText(path, reqs[i] + "\n");
                    }
                    else
                    {
                        //sub req
                        File.AppendAllText(path, "SUB_REQ\n");

                        for (int j = 0; j < reqs[i].Count; j++)
                        {
                            if (reqs[i][j] is ValueMinOrMax)
                            {
                                if (reqs[i][j].GetRequirementType() == EventRequirementName.TIME)
                                {
                                    //write time min
                                    File.AppendAllText(path, "TIME_MIN\n");

                                }
                                else if (reqs[i][j].GetRequirementType() == EventRequirementName.LEVEL)
                                {
                                    //write lvl
                                    File.AppendAllText(path, "LVL\n");
                                }
                                else
                                {
                                    //write enum
                                    File.AppendAllText(path, reqs[i][j].GetRequirementType().ToString() + "\n");
                                }

                                File.AppendAllText(path, reqs[i][j].GetValue().ToString() + "\n");
                            }
                            else if (reqs[i][j] is string)
                            {
                                File.AppendAllText(path, reqs[i][j] + "\n");
                            }
                            else if (reqs[i][j] is ObjectFill.FillType)
                            {
                                File.AppendAllText(path, "FILL_TYPE\n");
                                File.AppendAllText(path, reqs[i][j] + "\n");
                            }
                            

                        }

                        //end sub req
                        File.AppendAllText(path, "END_SUB_REQ\n");

                    }

                }

                List<EventEffects> eff = gameEvents[index].getEffects();
                for (int i= 0; i < eff.Count; i++)
                {
                    File.AppendAllText(path, "EFFECT\n");
                    File.AppendAllText(path, "EFFECTING\n");
                    File.AppendAllText(path, eff[i].GetFillEffected().ToString() + "\n");
                    File.AppendAllText(path, eff[i].GetEventEffectType().ToString() + "\n");
                    
                    if (eff[i].GetEventEffectType() != EventEffectType.DESTROY_RANDOM)
                    {
                        File.AppendAllText(path, eff[i].GetReduction().ToString() + "\n");
                                                
                    }

                    File.AppendAllText(path, "EFFECT_END\n");

                }

                //write end and blank line
                File.AppendAllText(path, "END\n");
            }


            
        }
    }


    void QueueDelete(int i)
    {
        delete = i;
    }

    void QueueConvert(int i, EventRequirementName t)
    {
        convert = i;
        convertTo = t;
    }

    void QueueDeleteSub(int i, int j)
    {
        deleteSub[0] = i;
        deleteSub[1] = j;
    }

    void QueueConvertSub(int i, int j, EventRequirementName t)
    {

        convertSub[0] = i;
        convertSub[1] = j;
        convertSubTo = t;
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

