using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, GameControls.IPlayerActions
{
    private GameControls _controls;
    private GameControls.PlayerActions _playerActions;

    
    [SerializeField] private BuildingManager buildingManager;

    private void Awake()
    {
        _controls = new GameControls();
        _playerActions = _controls.Player;
        _playerActions.AddCallbacks(this);
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            buildingManager.OnMouseClickBehaviour();
        }
    }

    public void OnMousePositionChanged(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            buildingManager.GetMousePosition(context.ReadValue<Vector2>());
        }
    }

    private void OnDestroy()
    {
        _controls.Dispose();
    }

    private void OnEnable()
    {
        _playerActions.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }
}
