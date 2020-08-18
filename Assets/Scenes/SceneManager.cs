using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static string[] _loopScenes =
    {
        "ContractSelect",
        "MinerSelection",
        "MiningPhase",
        "AuctionPhase",
        "ContractFulFillment"
    };

    private static int _loopIndex = 0;

    public static void GoToNextSceneInLoop()
    {
        _loopIndex++;
        if (_loopIndex >= _loopScenes.Length) _loopIndex = 0;
        Load(_loopScenes[_loopIndex]);
    }

    public static void RestartSceneLoopAnd()
    {
        _loopIndex = 0;
        Load(_loopScenes[_loopIndex]);
    }

    private static void Load(string s)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(s);
    }


}
