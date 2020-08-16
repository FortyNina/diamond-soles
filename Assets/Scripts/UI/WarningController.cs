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
        if(setting) StartCoroutine(FadeOverlayInAndOut());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator FadeOverlayInAndOut()
    {
        _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, 1);
        float alpha = 1;
        int dir = -1;
        while (true)
        {
            yield return new WaitForSeconds(.2f);
            if (alpha < 0) dir = 1;
            if (alpha > 1) dir = -1;
            alpha += .1f * dir;
            _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, alpha);
        }
    }
    
}
