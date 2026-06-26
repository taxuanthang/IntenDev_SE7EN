using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public List<Ball> ballsOnField;
    public GameObject[] goalFrameOnField;

    public Ball ballPlayerTouching;
    public Ball ballPlayerJustShoot;

    public PlayerManager player;

    public void Awake()
    {
        AssignListener();
    }


    public void AssignListener()
    {
        EventManager.instance.onCollision_PlayerAndBall.AddListener(UpdateBallTouching);
        EventManager.instance.onClicked_ButtonKick.AddListener(ShootBallPlayerTouching);
        EventManager.instance.onClicked_ButtonAutoKick.AddListener(ShootFarthestBallToTheNearestGoal);
    }

    public void ShootBallPlayerTouching()
    {
        ShootBallToTheNearestGoal(ballPlayerTouching);
    }

    public void ShootFarthestBallToTheNearestGoal()
    {
        Ball farthestBall = null;
        float farthestDistance = 0f;

        foreach (Ball ball in ballsOnField)
        {
            float distanceMeasure = Vector3.Distance(ball.transform.position, player.transform.position);
            if (farthestDistance < distanceMeasure)
            {
                farthestDistance = distanceMeasure;
                farthestBall = ball;
            }
        }

        ShootBallToTheNearestGoal(farthestBall);

    }

    public void ShootBallToTheNearestGoal(Ball ball)
    {
        GameObject nearestGoalDueToBall = GetNearestGoalFrameDueToBall(ball);

        ballPlayerJustShoot = ball;

        // Calculate Path
        Vector3 midPos = (ball.transform.position + nearestGoalDueToBall.transform.position) / 2;
        midPos = midPos / 1.1f;
        midPos.y = Random.RandomRange(4.6f, 5f);
        Vector3[] path =
            {
                ball.transform.position,
                midPos,
                nearestGoalDueToBall.transform.position
            };

        // ShootBall
        ball.transform.DOPath(
        path,
        1.5f,
        PathType.CatmullRom
        ).OnComplete(OnBallCompletePath);
    }

    private void OnBallCompletePath()
    {
        ballsOnField.Remove(ballPlayerJustShoot);
        Destroy(ballPlayerJustShoot.gameObject);
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
