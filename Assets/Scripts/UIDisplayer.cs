using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDisplayer : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] _playerFloorDisplay;


    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < _playerFloorDisplay.Length;i++)
        {
            _playerFloorDisplay[i].text = "Floor: " + GameData.Instance.ironFloors[i];
        }
    }
}
