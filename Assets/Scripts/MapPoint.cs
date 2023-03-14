using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left, secret;

    public bool isLevel, isLocked;

    public string levelToLoad, levelToCheck, otherLevelToCheck, levelName, bossLevel;

    public int gemsCollected, totalGems;

    public float bestTime, targetTime;

    public GameObject gemBadge, timeBadge;

    public bool secretEntrance, bridge;

    void Start()
    {

        if (isLevel && levelToLoad != null)
        {
            if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
            }

            if (PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetInt(levelToLoad + "_time");
            }

            if (gemsCollected >= totalGems)
            {
                gemBadge.SetActive(true);
            }

            if (bestTime <= targetTime && bestTime != 0)
            {
                timeBadge.SetActive(true);
            }

            isLocked = true;

            if (levelToCheck != null || otherLevelToCheck != null)
            {
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        isLocked = false;
                    }
                }
            }

            if (levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }

        if (secretEntrance)
        {
            if (PlayerPrefs.HasKey(bossLevel + "_unlocked"))
            {
                if (PlayerPrefs.GetInt(bossLevel + "_unlocked") == 1)
                {
                    up = secret;
                    bridge = true;
                }
            }
        }
    }

    void Update()
    {

    }
}
