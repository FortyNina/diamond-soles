using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompanyNameData
{
    public  string[] adjectives;
    public  string[] subjects;
    public  string[] fullNames;
    public  string[] suffixes;

    public string GetRandomName()
    {
        string n = "";
        if (Random.Range(0, 10) < 7)
        {
            n += fullNames[Random.Range(0, fullNames.Length)] + " ";
        }
        else
        {
            n += adjectives[Random.Range(0, adjectives.Length)] +  " ";
            n += subjects[Random.Range(0, subjects.Length)] + " ";

        }

        n += suffixes[Random.Range(0, suffixes.Length)];
        return n;
    }


}
