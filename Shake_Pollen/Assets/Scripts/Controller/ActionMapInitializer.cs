using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapInitializer : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private string initialActionMap = "Player";

    public event Action OnCompleteInit;

    private void Start()
    {
        // PlayerInput が使用する全 Action Map を無効化
        foreach (var map in playerInput.actions.actionMaps)
        {
            map.Disable();
        }

        // 指定した Action Map に切り替える
        playerInput.SwitchCurrentActionMap(initialActionMap);

        OnCompleteInit?.Invoke();
    }
}
