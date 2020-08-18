using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractFulFillmentScreen : MonoBehaviour
{

    [SerializeField] private GameObject _contractStatusPrefab;
    [SerializeField] private Transform _contractStatusParent;

    private bool[] _done;

    // Start is called before the first frame update
    void Start()
    {
        _done = new bool[GameData.Instance.companies.Count];
        for(int i = 0; i< GameData.Instance.companies.Count; i++)
        {
            GameObject go = Instantiate(_contractStatusPrefab, Vector3.zero, Quaternion.identity);
            go.transform.parent = _contractStatusParent;
            go.transform.localScale = new Vector3(1, 1, 1);
            ContractStatus cs = go.GetComponent<ContractStatus>();
            cs.playerID = i;
            cs.Init();
            cs.cfs = this;
            go.GetComponent<CompanyStatsUIFiller>().playerID = i;
        }
    }

    private void Update()
    {
        bool allDone = true;
        for(int i = 0; i < _done.Length; i++)
        {
            if (!_done[i]) allDone = false;
        }
        if (allDone) StartCoroutine(EndFulfillScreen());
    }

    public void DecisionSubmitted(int i)
    {
        _done[i] = true;
    }

    private IEnumerator EndFulfillScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.GoToNextSceneInLoop();
    }


}
