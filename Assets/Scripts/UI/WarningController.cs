using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class WarningController : MonoBehaviour
{

    [SerializeField] private GameObject _warningIcon;

    [SerializeField] private GameObject _notificationIcon;

    [SerializeField] private Image _overlay;


    public void SetWarningIcon(bool setting)
    {
        _warningIcon.SetActive(setting);
    }

    public void SetNotificationIcon(bool setting)
    {
        _notificationIcon.SetActive(setting);
    }

    public void SetOverlay(bool setting)
    {
        _overlay.gameObject.SetActive(setting);
    }
    
}
