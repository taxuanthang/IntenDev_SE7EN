using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public List<Ball> ballsOnField;
    public GameObject[] goalFrameOnField;

    [HideInInspector] public Ball ballPlayerTouching;
    [HideInInspector]  public Ball ballPlayerJustShoot;

    public PlayerManager player;

    [Header("Flags")]
    public bool isBallFlying = false;

    [Header("Settings")]
    public Vector3 deltaGoalHeight = new Vector3(0,1f,0f);
    public Vector2 randomRange = new Vector2(4.6f, 5f);

    public static FieldManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

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

        if (farthestBall != null) ShootBallToTheNearestGoal(farthestBall);

    }

    public void ShootBallToTheNearestGoal(Ball ball)
    {
        isBallFlying = true;
        PlayerCamera.instance.SetBallFollowed(ball.transform);

        GameObject nearestGoalDueToBall = GetNearestGoalFrameDueToBall(ball);

        ballPlayerJustShoot = ball;

        // Calculate Path
        Vector3 midPos = (ball.transform.position + nearestGoalDueToBall.transform.position) / 2;
        midPos = midPos / 1.1f;
        midPos.y = Random.RandomRange(randomRange.x, randomRange.y);
        Vector3[] path =
            {
                ball.transform.position,
                midPos,
                nearestGoalDueToBall.transform.position + deltaGoalHeight
            };

        // ShootBall
        ball.transform.DOPath(
        path,
        1.5f,
        PathType.CatmullRom
        ).OnComplete(OnBallCompletePath);
    }

    private async void OnBallCompletePath()
    {
        Vector3 hitPosition = ballPlayerJustShoot.transform.position;

        ballsOnField.Remove(ballPlayerJustShoot);
        Destroy(ballPlayerJustShoot.gameObject);

        EventManager.instance.onBallHitGoal.Invoke(hitPosition);

        await Task.Delay(2000);
        isBallFlying = false;
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
