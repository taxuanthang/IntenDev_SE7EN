using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public Rigidbody rigidbody;

    public PlayerLocomotionManager playerLocomotionManager;
    public PlayerAnimationManager playerAnimationManager;

    [Header("Flag")]
    public bool isMoving;

    public void Awake()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
        if (playerLocomotionManager == null) playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        if (playerAnimationManager == null) playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }


}
