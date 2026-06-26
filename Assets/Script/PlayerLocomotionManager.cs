using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLocomotionManager : MonoBehaviour
{
    PlayerManager player;

    [Header("Movement")]
    public Vector3 moveDirection;
    public float runningSpeed;
    public float moveAmount;
    public float horizontalInput;
    public float verticalInput;




    public void Awake()
    {
        if (player == null) player = GetComponent<PlayerManager>();
    }

    public void HandleMoveInput(float horizontal_Input,float vertical_Input)
    {
        this.horizontalInput = horizontal_Input;
        this.verticalInput = vertical_Input;
        moveAmount = Mathf.Clamp01(new Vector2(horizontal_Input,vertical_Input).magnitude);


        moveDirection = PlayerCamera.instance.transform.up * verticalInput;
        moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalInput;

        moveDirection.y = 0;
        moveDirection.Normalize();

        player.rigidbody.MovePosition(player.rigidbody.position + moveDirection * runningSpeed * Time.deltaTime);

        
    }

    public void HandleRotation(float directionX, float directionY)
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = PlayerCamera.instance.playerFollowCamera.transform.up * directionY;
        targetDirection = targetDirection + PlayerCamera.instance.playerFollowCamera.transform.right * directionX;
        targetDirection.y = 0;
        targetDirection.Normalize();
        if(targetDirection == Vector3.zero)
            targetDirection = transform.forward;
        Quaternion tr = Quaternion.LookRotation(targetDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, player.playerLocomotionManager.moveAmount * Time.deltaTime * 20 );
        transform.rotation = targetRotation;
    }

}
