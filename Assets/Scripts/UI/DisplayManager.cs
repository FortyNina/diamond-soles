using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    public RenderTexture[] texturePool;
    public AIDisplayWindow mainDisplay;
    private AIDisplayWindow[] _allDisplays;
    public GameObject miniDisplayPrefab;
    public Transform miniDisplayParent;

    private int _currentMainID = 0;
    public int CurrentMainID
    {
        get { return _currentMainID; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _allDisplays = new AIDisplayWindow[GameData.Instance.numPlayers];

        for(int i = 0; i < GameData.Instance.numPlayers; i++)
        {
            //Main Display
            if(i == 0)
            {
                _allDisplays[i] = mainDisplay.GetComponent<AIDisplayWindow>();
                _currentMainID = 0;
            }

            //Mini Displays
            else
            {
                GameObject go = Instantiate(miniDisplayPrefab, new Vector2(0, 0), Quaternion.identity);
                go.transform.parent = miniDisplayParent;
                _allDisplays[i] = go.GetComponent<AIDisplayWindow>();
            }

            _allDisplays[i].displayManager = this;
            _allDisplays[i].UpdateRenderTexture(i);
        }
    }


    public void SwitchDisplays(int indexToShowMain)
    {
        if (indexToShowMain == _currentMainID)
        {
            return;
        }

        AIDisplayWindow oldWindow = null;
        for (int i = 0; i < _allDisplays.Length; i++)
        {
            if (_allDisplays[i].playerID == indexToShowMain)
                oldWindow = _allDisplays[i];
        }

        mainDisplay.UpdateRenderTexture(indexToShowMain);
        oldWindow.UpdateRenderTexture(_currentMainID);

        _currentMainID = indexToShowMain;
    }

   
}
