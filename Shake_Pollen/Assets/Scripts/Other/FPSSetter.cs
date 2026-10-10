using UnityEngine;

//FPSを固定する機能

public class FPSSetter : MonoBehaviour
{
    [SerializeField]
    int _targetFPS = 60;

    void Start()
    {
        Application.targetFrameRate = _targetFPS;
    }
}
