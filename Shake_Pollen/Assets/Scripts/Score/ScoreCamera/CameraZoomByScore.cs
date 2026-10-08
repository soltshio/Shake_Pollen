using System;
using Unity.Cinemachine;
using UnityEngine;

//スコアの高さによってカメラをズームさせる

public class CameraZoomByScore : MonoBehaviour
{
    [SerializeField]
    ShakePtManager _shakePtManager;

    [SerializeField]
    CinemachineMixingCamera _cinemachineMixingInGameCamera;

    [SerializeField]
    float _minScore;

    [SerializeField]
    float _maxScore;

    const int _minZoomCameraNum = 0;
    const int _maxZoomCameraNum = 1;

    void Start()
    {
        _cinemachineMixingInGameCamera.SetWeight(_minZoomCameraNum, 1f);
        _cinemachineMixingInGameCamera.SetWeight(_maxZoomCameraNum, 0f);
    }

    void Update()
    {
        float point = _shakePtManager.Point;

        float rate = Mathf.InverseLerp(_minScore, _maxScore, point);

        if (!MathfExtension.IsInRange(rate, 0, 1f)) return;

        _cinemachineMixingInGameCamera.SetWeight(_minZoomCameraNum, 1f - rate);
        _cinemachineMixingInGameCamera.SetWeight(_maxZoomCameraNum, rate);
    }
}
