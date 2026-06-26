using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject FireworkEffectPrefabs;

    public void Awake()
    {
        EventManager.instance.onBallHitGoal.AddListener(PlayFireworkEffect);
    }

    public void PlayFireworkEffect(Vector3 position)
    {
        PlayEffect(FireworkEffectPrefabs, position, Quaternion.identity);
    }

    public void PlayEffect(GameObject effectPrefab, Vector3 position, Quaternion rotation)
    {
        GameObject effectInstance = Instantiate(effectPrefab, position, rotation);
        Destroy(effectInstance, 2f); // Destroy the effect after 2 seconds
    }
}
