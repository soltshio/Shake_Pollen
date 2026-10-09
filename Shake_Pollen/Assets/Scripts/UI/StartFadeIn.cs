using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartFadeIn : MonoBehaviour
{
    [SerializeField]
    FadeInOutPanel _fadeInOutPanel;

    void Start()
    {
        _fadeInOutPanel.FadeTrigger(FadeInOutPanel.FadeEType.FadeIn);
    }
}
