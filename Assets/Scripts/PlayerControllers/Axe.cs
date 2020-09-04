using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public MinerResourceManager mrm;

    private int _playerID;
    public int PlayerID
    {
        get { return _playerID; }
        set { _playerID = value;  }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IBreakable>() != null)
        {
            if (collision.gameObject.GetComponent<BreakableRock>() != null) {
                BreakableRock br = collision.gameObject.GetComponent<BreakableRock>();
                MineRecorder.UpdateMineTileHealth(br.floorNumber, br.mineType, -1, br.index);
                if (MineRecorder.GetMineTileHealth(br.floorNumber, br.mineType, br.index) <= 0)
                {
                    if (GameData.Instance.playerOreSupplies[PlayerID].ContainsKey(br.tileType))
                    {
                        mrm.CollectResource(br.tileType, br.myTile.oreAmount);
                    }
                    br.Break();
                }

            }
            
        }
    }


}
