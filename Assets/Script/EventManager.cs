using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [Header("Events")]
    public UnityEvent<bool,Ball> onCollision_PlayerAndBall;
    public UnityEvent onClicked_ButtonKick;
    public UnityEvent onClicked_ButtonAutoKick;
    public UnityEvent onClikced_ButtonReset;
    public UnityEvent<Vector3> onBallHitGoal;


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
    }


}
