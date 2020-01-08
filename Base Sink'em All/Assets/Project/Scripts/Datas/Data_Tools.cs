using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjetPirate.Data
{
    [Serializable]
    public struct myVector3
    {
        public myVector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    [Serializable]
    public struct myVector4
    {
        public myVector4(float _x, float _y, float _z, float _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }
    }

    public static class Data_Tools
    {
        public static int[] StringToIntASCII(string pString)
        {
            char[] charValue = pString.ToCharArray();
            int[] value = new int[charValue.Length];

            string strCharValue = "";
            string strIntValue = "";

            for (int i = 0; i < charValue.Length; i++)
            {
                //Debug.Log(charValue[i]);
                strCharValue += charValue[i];
                value[i] = charValue[i];
            }
            //Debug.Log("char : " + strCharValue);
            //Debug.Log("----------------------------  " + charValue.Length);

            for (int i = 0; i < value.Length; i++)
            {
                //Debug.Log(value[i]);
                strIntValue += value[i];
                strIntValue += " ";

            }
            //Debug.Log("char : " + strIntValue);

            return value;
        }

        /* when we get a number in a string, and we want the exact same number in a int */
        public static int StringToIntExact(string pString)
        {
            int yessaille = Int32.Parse(pString);
            return yessaille;
        }

        public static string IntToString(int[] pInt)
        {
            char[] charValue = new char[pInt.Length];
            for (int i = 0; i < charValue.Length; i++)
            {
                // Debug.Log(charValue[i]);
            }

            return pInt.ToString();
        }

        public static Data_Quests DecryptingQuest(int IDquest)
        {
            Data_Quests quest = new Data_Quests();
            string path = MonoData_Tools.pathHDDQuestes + IDquest ;
            if (File.Exists(path))
            {
                int count = 0;
                StreamReader stream = new StreamReader(path);
                string _text;
                while ((_text = stream.ReadLine()) != null)
                {
                    if (count == 0)
                        quest.ID = Int32.Parse(_text);
                    else if (count == 1)
                        quest.ItemNecessary = FindNameObjectByID(Int32.Parse(_text));
                    else if (count == 2)
                        quest.ItemCount = Int32.Parse(_text);
                    else if (count == 3)
                    {
                        Debug.LogWarning("TExtQuest: " + _text);
                        quest.TextQuest = FindDialogueByID(Int32.Parse(_text));
                    }
                    count++;
                }
                stream.Close();
                return quest;
            }
            else
                Debug.LogWarning("Quest doesn't exist in path " + path);
            return null;
        }

        public static Data_Dock DecriptingDock(int idDock)
        {
            Data_Dock dock = null;
            string path = MonoData_Tools.pathHDDDock + idDock;
            if (File.Exists(path))
            {
                StreamReader stream = new StreamReader(path);
                string _text;
                _text = stream.ReadLine();
                stream.Close();
            }

            return dock;
        }

        public static Data_Quests FindQuestByID(int IDquest)
        {
            Data_Quests quest = null;

            string path = MonoData_Tools.pathHDDQuestes + IDquest;
            if (File.Exists(path))
            {
                quest.ID = IDquest;
                int count = 0;
                StreamReader stream = new StreamReader(path);
                string _text;
                while ((_text = stream.ReadLine()) != null)
                {
                    if (count == 0)
                        quest.Type = IDquest;
                    else if (count == 1)
                        quest.ItemNecessary = FindNameObjectByID(Int32.Parse(_text));

                    count++;
                }
            }
            return quest;
        }

        public static Data_Pnj FindPnj(string pName)
        {
            Data_Pnj pnj = new Data_Pnj();
            string path = MonoData_Tools.pathHDDPnj + pName;
            if (File.Exists(path))
            {
                int count = 0;
                StreamReader stream = new StreamReader(path);
                string _text;
                while ((_text = stream.ReadLine()) != null)
                {
                    if (count == 0)
                    {
                        //Debug.Log("TEXT -----> " + _text);
                        //Debug.Log("TEXT Extract -----> " + StringToIntExact(_text));
                        if (StringToIntExact(_text) == 0)
                            pnj.HaveQuest = false;
                        else
                            pnj.HaveQuest = true;

                    }
                    else if (count == 1)
                    {
                        if (pnj.HaveQuest)
                        {
                            Debug.LogWarning("QuestId: " + _text);
                            pnj.QuestId = StringToIntExact(_text);
                            pnj.Quete = DecryptingQuest(pnj.QuestId);
                            pnj.DialogueQuete = pnj.Quete.TextQuest;
                        }
                    }
                    else if (count == 2)
                    {
                        if (pnj.HaveQuest)
                            pnj.DialogueQuete = pnj.Quete.TextQuest;
                    }
                    else if (count == 3)
                    {
                        pnj.Dialogue = FindDialogueByID(Int32.Parse(_text));
                    }
                    else if (count == 4)
                        Debug.Log("On va chercher le sprite");

                    count++;
                }
                Debug.Log("Quest: " + pnj.QuestId.ToString());
                Debug.Log("Dialogue: " + pnj.Dialogue);
                Debug.Log("DialogueQuete: " + pnj.DialogueQuete);
                stream.Close();
            }
            else
                Debug.LogWarning("File dosen't exist at " + path);

            return pnj;
        }

        public static string FindIdObjectByName(string objectName)
        {
            string path = MonoData_Tools.pathHDDbject + "Objects";
            ///Si le fichier existe bien alors
            if (File.Exists(path))
            {
                StreamReader stream = new StreamReader(path);
                string _text;
                while ((_text = stream.ReadLine()) != null)
                {
                    /// WoddenBoard 0002
                    char[] charText = new char[_text.Length];
                    charText = _text.ToCharArray();

                    string tmpString = null;

                    for (int i = 0; i < charText.Length; i++)
                    {

                        if (tmpString == objectName)
                        {
                            _text.Remove(0, i + 1);
                            return _text;
                        }
                        else
                        {
                            tmpString += charText[i].ToString();
                        }
                    }
                }

                return null;
            }
            else
                return null;
        }

        public static string FindNameObjectByID(int objectID)
        {
            string path = MonoData_Tools.pathHDDbject +"/Objects";
            ///Si le fichier existe bien alors
            if (File.Exists(path))
            {
                StreamReader stream = new StreamReader(path);
                string _text;
                while ((_text = stream.ReadLine()) != null)
                {
                    char[] charText = new char[_text.Length];
                    charText = _text.ToCharArray();

                    string currentText = null;
                    string tmpName = null;
                    for (int i = 0; i < charText.Length; i++)
                    {
                        currentText += charText[i].ToString();

                        if (charText[i].ToString() == " ")
                        {
                            tmpName = currentText;
                            currentText = currentText.Remove(0, i + 1);
                        }

                        if (currentText == objectID.ToString())
                        {
                            return tmpName;

                        }
                    }
                }
                return null;
            }
            else
                return null;
        }

        public static string FindDialogueByID(int IdDialogue)
        {
            string dialogue = null;
            string path = MonoData_Tools.pathHDDDialogues + IdDialogue;
            if (File.Exists(path))
            {
                StreamReader stream = new StreamReader(path);
                dialogue = stream.ReadToEnd();
            }
            return dialogue;
        }
    }
}
