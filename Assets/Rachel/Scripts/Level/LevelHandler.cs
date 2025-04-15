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
    public void LoadLevel(int level)
    {
        LevelManager.instance.LoadLevel(level);
    }

    public void LoadChapter(int chapter)
    {
        LevelManager.instance.LoadChapter(chapter);
    }
}
