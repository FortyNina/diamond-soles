﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePicker : MonoBehaviour
{
    public GridCreator gridCreator;
    int id;

    private void Start()
    {
        id = gridCreator.playerID;
        Debug.Log("WTF!");

        //Its an AI!
        if(id >= GameData.Instance.numberRealPlayers)
        {
           // StartCoroutine(DetermineAIChoice());
        }
    }

    private void OnDestroy()
    {
        Debug.Log("by bye cruel world");
    }


    public void SelectMine(string type)
    {
        Mine m = Mine.IronMine;
        if (type == "ironmine")
            m = Mine.IronMine;
        if (type == "jellymine")
            m = Mine.JellyMine;
        if (type == "coalmine")
            m = Mine.CoalMine;

        gridCreator.SelectMine(m);
        Destroy(gameObject);
    }

    private IEnumerator DetermineAIChoice()
    {
        yield return new WaitForSeconds((float)Random.Range((float).5f, (float)1.5f));
        Mine choice = AIManager.GetMineChoice(id);
        SelectMine(choice.ToString().ToLower());
    }



}
