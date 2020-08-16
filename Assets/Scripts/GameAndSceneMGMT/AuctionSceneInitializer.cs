using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionSceneInitializer : MonoBehaviour
{
    [SerializeField] private Color[] _playerColors;

    [SerializeField] private GameObject _auctionPlayerPrefab;
    [SerializeField] private Transform _auctionPlayerParent;
    [SerializeField] private AuctionManager _auctionManager;
    [SerializeField] private Transform _ceiling;
    [SerializeField] private Transform _floor;

   
    // Start is called before the first frame update
    void Start()
    {

        AuctionPlayerController[] players = new AuctionPlayerController[GameData.Instance.companies.Count];

        for(int i = 0; i< GameData.Instance.companies.Count; i++)
        {

            if (i != 0) GameData.Instance.companies[i].SimulateMiningPhase(); // simulate the mining phase for the AI companies

            GameObject go = Instantiate(_auctionPlayerPrefab, Vector3.zero, Quaternion.identity);
            go.transform.parent = _auctionPlayerParent;
            go.transform.localScale = new Vector3(1, 1, 1);


            players[i] = go.GetComponent<AuctionPlayerController>();
            players[i].playerID = i;
            if (i > 0) players[i].isAI = true;
            players[i].am = _auctionManager;
            players[i]._floor = _floor;
            players[i]._ceiling = _ceiling;
            players[i].PlayerColor = _playerColors[i];
        }


        _auctionManager.BeginSequences(players);



    }

    
}
