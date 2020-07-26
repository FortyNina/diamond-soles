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
    private RawImage _rawIm;

    private void OnEnable()
    {
        _rawIm = GetComponent<RawImage>();
    }

    public void UpdateRenderTexture(int newID)
    {
        Debug.Log("Updating????");
        playerID = newID;
        _currentTex = displayManager.texturePool[newID];
        _rawIm.texture = _currentTex;
    }

    public void RequestDisplaySwitch()
    {
        displayManager.SwitchDisplays(playerID);
    }
}
