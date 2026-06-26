using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header("Button")]
    public Button button_Kick;
    public Button button_AutoKick;

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

        button_Kick.gameObject.SetActive(false);

        button_Kick.onClick.AddListener(onClick_ButtonKick);
        button_AutoKick.onClick.AddListener(onClick_ButtonAutoKick);
    }

    public void AssignListener()
    {
        EventManager.instance.onCollision_PlayerAndBall.AddListener(DisplayButtonKick);
    }

    public void DisplayButtonKick(bool isActive, Ball hitBall)
    {
        button_Kick.gameObject.SetActive(isActive);
    }

    public void onClick_ButtonKick()
    {
        EventManager.instance.onClicked_ButtonKick.Invoke();
    }

    public void onClick_ButtonAutoKick()
    {
        EventManager.instance.onClicked_ButtonAutoKick.Invoke();
    }

}
