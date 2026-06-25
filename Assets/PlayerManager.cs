using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public Rigidbody rigidbody;

    public PlayerLocomotionManager playerLocomotionManager;

    [Header("Flag")]
    public bool isMoving;

    public void Awake()
    {
        if (playerLocomotionManager == null) playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
    }


}
