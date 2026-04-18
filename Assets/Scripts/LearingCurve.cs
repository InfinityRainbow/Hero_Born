using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearingCurve : MonoBehaviour
{
    // Start is called before the first frame update
    public int CurrentAge = 30;
    public int AddedAge = 1;
    public int CurrentGold = 68;
    public string CharacterAction = "Heal";
    //int[] TopPlayerScores = new int[5] { 5,7,5,1,6};
    //int[] TopPlayerSS = { 5, 3, 6, 2, 4 };
    int[,] TopPlayerScores = { { 3, 2 }, { 5, 3 } };


    public void FindPartyMembers()
    {
        List<string> QuestPartyMembers = new List<string>()
    {
        "Grim the Barbarian",
        "Merlin the Wise",
        "Sterling the Knight"

    };
        QuestPartyMembers.Add("Craven the Necromancer");
        QuestPartyMembers.Insert(0, "Well Done");
        Debug.LogFormat("Party Members:{0}", QuestPartyMembers.Count);
        int listLength = QuestPartyMembers.Count;
        QuestPartyMembers.RemoveAt(1);
        listLength--;
        for (int i = 0; i < listLength; i++)
        {
            Debug.LogFormat("index:{0}-{1}", i, QuestPartyMembers[i]);
            if (QuestPartyMembers[i] == "Merlin the Wise")
            {
                Debug.Log("Glad you are here Merlin!");
            }
        }
    }


    public void Thievery()
    {
        if (CurrentGold > 50)
        {
            Debug.Log(" you are rolling in it!");

        }
        else if (CurrentGold < 15)
        {
            Debug.Log("Not much to steal..");

        }
        else
        {
            Debug.Log("Looks like your purse is in the sweet spot.");
        }
    }


    public void PrintCharacterAction()
    {
        switch (CharacterAction)
        {
            case "Heal":
                Debug.Log("Potion sent.");
                break;
            case "Attack":
                Debug.Log("To arms!");
                break;
            default:
                Debug.Log("shields up.");
                break;



        }
    }



    Dictionary<string, int> ItemInventory = new Dictionary<string, int>()
    {
        {"Potion",5 },
        {"Antidote",7 },
        {"Asprin",1}
    };


    public void ValidateDictionaryFunction(Dictionary<string, int> a)//验证字典功能
    {
        Debug.LogFormat("Items:{0}", a.Count);
        if (a.ContainsKey("Asprin"))//根据键是否存在返回一个布尔值
        {
            a["Asprin"] = 3;
            if (a["Asprin"] == 3)
            {
                a.Remove("Potion");
            }
        }
        Debug.LogFormat("Current Asprin Value:{0}", a["Asprin"]);

        Debug.LogFormat("Items:{0}", a.Count);
        foreach (KeyValuePair<string, int> kvp in a)
        {
            Debug.LogFormat("Item:{0}-{1}", kvp.Key, kvp.Value);
        }
    }
    void Start()
    {
        // Debug.Log(30 + 1);
        //Debug.Log(currentage + 1);
        // ComputeAge();
        //Debug.Log($"CurrentGold actual value: {CurrentGold}");
        //Debug.Log($"CharacterAction actual value: '{CharacterAction}'");
        //Debug.Log($"The Top1 Palyer Score Is:{TopPlayerScores[0, 0]}");

        ValidateDictionaryFunction(ItemInventory);
        Thievery();
        PrintCharacterAction();
        FindPartyMembers();

    }
    /// <summary>
    /// 
    /// man what can i say
    /// manba out 
    /// </summary>



    //void ComputeAge()
    //{
    //    Debug.Log(CurrentAge + AddedAge);
    //}
    // Update is called once per frame
    void Update()
    {

    }
}
//ctrl K U以打开