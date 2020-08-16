using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Attach to a UI object with a CanvasGroup component. This script will
/// modify the opacity for this gameobject, as well as all of its children.
/// </summary>

[RequireComponent(typeof(CanvasGroup))]

public class UIOpacityModifier : MonoBehaviour
{

    [SerializeField] private UnityEvent OnOpacityChanged;
    [SerializeField] private UnityEvent OnOpacityTurnedOff;
    [SerializeField] private UnityEvent OnOpacityTurnedOn;

    private CanvasGroup _canvasGroup;


    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// MODIFIES the opacity. A new opacity must be provided.
    /// All children are affected
    /// </summary>
    public void ChangeOpacity(float newOpacity)
    {
        Debug.Log("Change opacity");
        OnOpacityChanged.Invoke();
        _canvasGroup.alpha = newOpacity;
    }

    // <summary>
    /// Turn the opacity to 0. A new opacity must be provided.
    /// All children are affected
    /// </summary>
    public void TurnOffOpacity()
    {
        OnOpacityTurnedOff.Invoke();
        _canvasGroup.alpha = 0;
    }


    // <summary>
    /// Turn the opacity to full. A new opacity must be provided.
    /// All children are affected
    /// </summary>
    public void TurnOnOpacity()
    {
        OnOpacityTurnedOn.Invoke();
        _canvasGroup.alpha = 1;
    }
}