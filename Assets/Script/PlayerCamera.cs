using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public static PlayerCamera instance;

    public CinemachineFreeLook playerFollowCamera;
    public CinemachineFreeLook ballFollowCamera;

    public int firstPriority = 1000;


    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        EventManager.instance.onBallHitGoal.AddListener(SetToPlayerFollowCamera);
        EventManager.instance.onClicked_ButtonKick.AddListener(SetToBallFollowCamera);
        EventManager.instance.onClicked_ButtonAutoKick.AddListener(SetToBallFollowCamera);
        RearragePriority();
    }

    public void SetToBallFollowCamera()
    {
        RearragePriority();
        ballFollowCamera.Priority = 1000;
    }

    public void SetToPlayerFollowCamera()
    {
        RearragePriority();
        playerFollowCamera.Priority = 1000;
    }

    public void RearragePriority()
    {
        ballFollowCamera.Priority = 0;
        playerFollowCamera.Priority = 1;
    }

    public void SetBallFollowed(Transform ball)
    {
        ballFollowCamera.Follow = ball;
        ballFollowCamera.LookAt = ball;
    }
}
