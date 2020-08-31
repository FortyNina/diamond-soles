using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisplayManager : MonoBehaviour
{
    public RenderTexture[] texturePool;
    public AIDisplayWindow mainDisplay;
    public GameObject miniDisplayPrefab;
    public Transform miniDisplayParent;
    public UIDisplay ui;

    public Vector2 camOffset;

    private int _currentMainID = 0;
    public int CurrentMainID
    {
        get { return _currentMainID; }
    }

    private AIDisplayWindow[] _allDisplays;


    [SerializeField] private UnityEvent OnDisplayChanged;

    // Start is called before the first frame update
    void Start()
    {
        _allDisplays = new AIDisplayWindow[GameData.Instance.numPlayers];

        for (int i = 0; i < GameData.Instance.numPlayers; i++)
        {
            //Main Display
            //if(i == 0)
            //{
            //    _allDisplays[i] = mainDisplay.GetComponent<AIDisplayWindow>();
            //    _currentMainID = 0;
            //}

            ////Mini Displays
            //else
            {
                GameObject go = Instantiate(miniDisplayPrefab, new Vector2(0, 0), Quaternion.identity);
                go.transform.parent = miniDisplayParent;
                go.transform.localScale = new Vector2(1, 1);
                _allDisplays[i] = go.GetComponent<AIDisplayWindow>();
            }

            _allDisplays[i].displayManager = this;
            _allDisplays[i].UpdateRenderTexture(i);
        }
    }


    public void SwitchDisplaysWithRotate(int indexToShowMain)
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
        Camera.main.transform.position = new Vector3(GameData.Instance.gridLocations[indexToShowMain].x, Camera.main.transform.position.y, -10);

        _currentMainID = indexToShowMain;
        GameData.Instance.playerInFocus = _currentMainID;

        ui.UpdateFocusID(_currentMainID);
        OnDisplayChanged.Invoke();

    }

    public void SwitchDisplaysStatic(int indexToShowMain)
    {
        Camera.main.transform.position = new Vector3(GameData.Instance.gridLocations[indexToShowMain].x + camOffset.x, GameData.Instance.gridLocations[indexToShowMain].y + camOffset.y, -10);
        _currentMainID = indexToShowMain;
        GameData.Instance.playerInFocus = _currentMainID;

        ui.UpdateFocusID(_currentMainID);
        OnDisplayChanged.Invoke();
    }


}
