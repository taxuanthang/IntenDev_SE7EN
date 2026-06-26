using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadingManager : MonoBehaviour
{
    public void Awake()
    {
        EventManager.instance.onClikced_ButtonReset.AddListener(LoadGameScene);
    }

    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
