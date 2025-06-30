using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static Vector2 Movement;
    public static bool IsDashing;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _dashAction;

    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _dashAction = _playerInput.actions["Dash"];
    }
    
    private void Update() {
        Movement = _moveAction.ReadValue<Vector2>();
        IsDashing = Keyboard.current.leftShiftKey.isPressed;
    }

    
}
