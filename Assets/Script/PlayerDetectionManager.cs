using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionManager : MonoBehaviour
{
    [Header("Ball Detection")]
    public float detectBallRadius;
    public LayerMask ballLayerMask;
    public Vector3 height;
    public bool debugDectectArea = false;


    public void Update()
    {
        HandleBallDetection();
    
    }

    public void HandleBallDetection()
    {
        // Detect Ball
        Collider[] ballColliders = Physics.OverlapCapsule(this.transform.position, this.transform.position + height, detectBallRadius, ballLayerMask.value);

        if (ballColliders.Length > 0)
        {
            Ball ball = ballColliders[0].gameObject.GetComponent<Ball>();
            EventManager.instance.onCollision_PlayerAndBall.Invoke(true, ball);
        }
        else
        {
            EventManager.instance.onCollision_PlayerAndBall.Invoke(false, null);
        }


    }

    private void OnDrawGizmos()
    {
        if (debugDectectArea)
        {
            Vector3 point1 = transform.position;
            Vector3 point2 = transform.position + height;

            Gizmos.color = Color.green;

            // Hai đầu capsule
            Gizmos.DrawWireSphere(point1, detectBallRadius);
            Gizmos.DrawWireSphere(point2, detectBallRadius);

            // Các cạnh capsule
            Vector3 forward = transform.forward * detectBallRadius;
            Vector3 right = transform.right * detectBallRadius;

            Gizmos.DrawLine(point1 + right, point2 + right);
            Gizmos.DrawLine(point1 - right, point2 - right);

            Gizmos.DrawLine(point1 + forward, point2 + forward);
            Gizmos.DrawLine(point1 - forward, point2 - forward);
        }
    }

}
