using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public Ball[] ballsOnField;
    public GameObject[] goalFrameOnField;

    public Ball ballPlayerTouching;
    public Ball ballPlayerJustShoot;


    public void Awake()
    {
        AssignListener();
    }


    public void AssignListener()
    {
        EventManager.instance.onCollision_PlayerAndBall.AddListener(UpdateBallTouching);
        EventManager.instance.onClicked_ButtonKick.AddListener(ShootBallToTheNearestGoal);
    }    

    public void ShootBallToTheNearestGoal()
    {
        GameObject nearestGoalDueToBall = GetNearestGoalFrameDueToBall(ballPlayerTouching);

        ballPlayerJustShoot = ballPlayerTouching;

        // Calculate Path
        Vector3 midPos = (ballPlayerTouching.transform.position + nearestGoalDueToBall.transform.position) / 2;
        midPos = midPos / 1.1f;
        midPos.y = Random.RandomRange(4.6f, 5f);
        Vector3[] path =
            {
                ballPlayerTouching.transform.position,
                midPos,
                nearestGoalDueToBall.transform.position
            };

        // ShootBall
        ballPlayerTouching.transform.DOPath(
        path,
        1.5f,
        PathType.CatmullRom
        ).OnComplete(OnBallCompletePath);
    }

    private void OnBallCompletePath()
    {
        //Destroy(ballPlayerJustShoot.gameObject);
    }

    public GameObject GetNearestGoalFrameDueToBall(Ball kickedBall)
    {
        float nearestDistance = float.MaxValue;
        GameObject nearestGoal = null;
        foreach(GameObject goal in goalFrameOnField)
        {
            float distanceMeasure = Vector3.Distance(goal.transform.position, kickedBall.transform.position);
            if(nearestDistance > distanceMeasure)
            {
                nearestDistance = distanceMeasure;
                nearestGoal = goal;
            }    
        }

        return nearestGoal;
    }   
    
    public void GetFarthestBallDueToPlayer()
    {

    }

    public void UpdateBallTouching(bool isTouching, Ball Ball)
    {
        ballPlayerTouching = isTouching ? Ball : null;
    }    
}
