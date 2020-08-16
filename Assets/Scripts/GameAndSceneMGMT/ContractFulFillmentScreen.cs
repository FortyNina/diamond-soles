using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractFulFillmentScreen : MonoBehaviour
{

    [SerializeField] private GameObject _contractStatusPrefab;
    [SerializeField] private Transform _contractStatusParent;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< GameData.Instance.companies.Count; i++)
        {
            GameObject go = Instantiate(_contractStatusPrefab, Vector3.zero, Quaternion.identity);
            go.transform.parent = _contractStatusParent;
            go.transform.localScale = new Vector3(1, 1, 1);
            ContractStatus cs = go.GetComponent<ContractStatus>();
            cs.playerID = i;
            cs.Init();
            go.GetComponent<CompanyStatsUIFiller>().playerID = i;
        }
    }

   
}
