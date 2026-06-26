using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header("Button")]
    public Button buttonKick;

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

        buttonKick.gameObject.SetActive(false);

        buttonKick.onClick.AddListener(onClick_ButtonKick);
    }

    public void AssignListener()
    {
        EventManager.instance.onCollision_PlayerAndBall.AddListener(DisplayButtonKick);
    }

    public void DisplayButtonKick(bool isActive, Ball hitBall)
    {
        buttonKick.gameObject.SetActive(isActive);
    }

    public void onClick_ButtonKick()
    {
        EventManager.instance.onClicked_ButtonKick.Invoke();
    }

}
