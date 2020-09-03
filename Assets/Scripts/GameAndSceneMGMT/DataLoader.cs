using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSON;

public class DataLoader : MonoBehaviour
{
    private void Awake()
    {
        CompanyNameData set;
        try { set = JSONReader.ScanToObjectFromResources<CompanyNameData>("JSON/miningCompanyNames"); }
        catch { set = JSONReader.ScanToObjectFromStreamingAssets<CompanyNameData>("JSON/miningCompanyNames"); }
        GameData.Instance.nameData = set;
        Debug.Log(set.adjectives.Length);
    }
}
