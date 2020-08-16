using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ContractSelectionScreen : MonoBehaviour
{

    [SerializeField] private GameObject _contractPrefab;
    [SerializeField] private Transform _contractParent;
    [SerializeField] private TextMeshProUGUI _playerTurnTitle;

    private List<ContractButton> _contractButtons = new List<ContractButton>();

    private List<int> _alreadyTakenTurn = new List<int>();
    private int _currentPlayer = 0;

    public UnityEvent OnAllPlayersDone;

    // Start is called before the first frame update
    void Start()
    {

        //display all contracts
        for(int i = 0; i < GameData.Instance.companies.Count; i++)
        {
            GameObject go = Instantiate(_contractPrefab, Vector3.zero, Quaternion.identity);
            go.transform.parent = _contractParent;
            Contract c = new Contract();
            go.transform.localScale = new Vector3(1, 1, 1);
            ContractButton cb = go.GetComponent<ContractButton>();
            cb.Init(c);
            int id = i;
            go.GetComponent<Button>().onClick.AddListener(delegate { ContractSelected(id); });
            _contractButtons.Add(cb);
        }


        _currentPlayer = GetNextRankedCompanyID();

    }

    // Update is called once per frame
    void Update()
    {
        _playerTurnTitle.text = "Player " + _currentPlayer + "'s turn";
    }

    private void ContractSelected(int contractID)
    {
        GameData.Instance.companies[_currentPlayer].contract = _contractButtons[contractID].contract;
        _contractButtons[contractID].taken = true;
        _contractButtons[contractID].gameObject.GetComponent<UIOpacityModifier>().TurnOffOpacity();
        SetNextPlayer();
    }

    private void SetNextPlayer()
    {
        if(_alreadyTakenTurn.Count >= GameData.Instance.companies.Count)
        {
            OnAllPlayersDone.Invoke();
        }
        else
        {
            _currentPlayer = GetNextRankedCompanyID();
            if (_currentPlayer != 0) StartCoroutine(AISelectContract());
        }
    }

    private int GetNextRankedCompanyID()
    {
        int index = 0;
        float maxRating = -1;
        for(int i = 0; i< GameData.Instance.companies.Count; i++)
        {
            if (_alreadyTakenTurn.Contains(i)) continue;
            if(GameData.Instance.companies[i].rating > maxRating)
            {
                maxRating = GameData.Instance.companies[i].rating;
                index = i;
            }
        }

        _alreadyTakenTurn.Add(index);
        return index; 
    }

    private IEnumerator AISelectContract()
    {
        List<Contract> availableContracts = new List<Contract>();
        for(int i = 0;i < _contractButtons.Count; i++)
        {
            if (!_contractButtons[i].taken) availableContracts.Add(_contractButtons[i].contract);
        }
        if(availableContracts.Count > 1) yield return new WaitForSeconds(Random.Range(2.5f, 3.5f));
        else yield return new WaitForSeconds(1f);


        Contract c = AIAuctionManager.ChooseContractFromList(availableContracts);

        for (int i = 0; i < _contractButtons.Count; i++)
        {
            if (_contractButtons[i].contract == c)
            {
                _contractButtons[i].GetComponent<Button>().onClick.Invoke();
            }

        }

    }

    public void EndContractSelect()
    {
        SceneManager.LoadScene("MinerSelection");
    }
}
