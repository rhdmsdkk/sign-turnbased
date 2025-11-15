using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Calls the Level Manager
 * 
 */
public class LevelHandler : MonoBehaviour
{
    public void LoadAct(int level)
    {
        LevelManager.instance.LoadAct(level);
    }

    public void LoadEpisode(int chapter)
    {
        LevelManager.instance.LoadEpisode(chapter);
    }
}
