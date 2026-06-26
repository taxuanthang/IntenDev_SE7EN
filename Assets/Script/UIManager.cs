using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header("Button")]
    public GameObject buttonKick;

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

        buttonKick.SetActive(false);
    }

    public void AssignListener()
    {
        EventManager.instance.onPlayerCollisionBall.AddListener(DisplayButtonKick);
    }

    public void DisplayButtonKick(bool isActive, GameObject hitBall)
    {
        buttonKick.SetActive(isActive);
    }

}
