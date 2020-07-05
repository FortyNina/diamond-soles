using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionController : MonoBehaviour
{

    [SerializeField]
    private UIDisplayer _uiDisplay;

    private enum AuctionState { Idle, IronAuctionDecision, IronAuctionBuying };

    private AuctionState state = AuctionState.Idle;


    private void Update()
    {
    }

    public void StartNewAuction()
    {
        state = AuctionState.IronAuctionDecision;
    }





}
