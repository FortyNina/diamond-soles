using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIDisplayWindow : MonoBehaviour
{
    public int playerID;

    [HideInInspector]
    public DisplayManager displayManager;

    private RenderTexture _currentTex;
    [SerializeField] private RawImage _rawIm;
    [SerializeField] private RawImage _highlight;

    private void OnEnable()
    {
        if (playerID == GameData.Instance.playerInFocus && _highlight != null)
            _highlight.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (playerID != GameData.Instance.playerInFocus && _highlight != null)
            _highlight.gameObject.SetActive(false);
    }

    public void UpdateRenderTexture(int newID)
    {
        playerID = newID;
        _currentTex = displayManager.texturePool[newID];
        _rawIm.texture = _currentTex;
    }

    public void RequestDisplaySwitch()
    {
        //displayManager.SwitchDisplays(playerID);
        displayManager.SwitchDisplaysStatic(playerID);
        _highlight.gameObject.SetActive(true);
    }
}
