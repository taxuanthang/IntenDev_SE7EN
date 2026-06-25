using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [HideInInspector] InputSystem _inputSystem;

    [Header("Player Movement input")]
    [SerializeField] public float moveAmount;
    public Vector2 movement_Input;
    [SerializeField] public float vertical_Input;
    [SerializeField] public float horizontal_Input;


    public void Awake()
    {
        if (_inputSystem == null)
        {
            _inputSystem = new InputSystem();

            //Binding
            _inputSystem.Player.Move.performed += i => movement_Input = i.ReadValue<Vector2>();
        }

    }
    public void OnEnable()
    {
        _inputSystem.Enable();
    }

    public void OnDisable()
    {
        _inputSystem.Disable();
    }

    public void Update()
    {
        HandleAllInput();  
    }

    public void HandleAllInput()
    {
        HandlePlayerMovementInput();
    }

    public void HandlePlayerMovementInput ()
    {
        vertical_Input = movement_Input.y;
        horizontal_Input = movement_Input.x;

        moveAmount = Mathf.Clamp01(movement_Input.magnitude);

        if (moveAmount > 0f && moveAmount <= 0.5f)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount >= 0.5f && moveAmount <= 1f)
        {
            moveAmount = 1f;
        }

        if (player == null)
        {
            return;
        }

        if (moveAmount != 0f)
        {
            player.isMoving = true;
        }
        else
        {
            player.isMoving = false;
        }

        player.playerLocomotionManager.HandleMoveInput(horizontal_Input, vertical_Input);
    }


}
