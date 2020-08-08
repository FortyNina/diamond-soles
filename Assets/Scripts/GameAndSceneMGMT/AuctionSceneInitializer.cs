using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionSceneInitializer : MonoBehaviour
{

    [SerializeField] private GameObject _auctionPlayerPrefab;
    [SerializeField] private Transform _auctionPlayerParent;
    [SerializeField] private AuctionManager _auctionManager;
    [SerializeField] private Transform _ceiling;
    [SerializeField] private Transform _floor;


    // Start is called before the first frame update
    void Start()
    {

        int totalNumPlayers = GameData.Instance.numAuctionAi + 1;
        AuctionPlayerController[] players = new AuctionPlayerController[totalNumPlayers];

        for(int i = 0; i< totalNumPlayers; i++)
        {

            GameObject go = Instantiate(_auctionPlayerPrefab, Vector3.zero, Quaternion.identity);
            go.transform.parent = _auctionPlayerParent;
            go.transform.localScale = new Vector3(1, 1, 1);


            players[i] = go.GetComponent<AuctionPlayerController>();
            players[i].playerID = i;
            if (i > 0) players[i].isAI = true;
            players[i].am = _auctionManager;
            players[i]._floor = _floor;
            players[i]._ceiling = _ceiling;


        }


        _auctionManager.BeginSequences(players);



    }

    
}
