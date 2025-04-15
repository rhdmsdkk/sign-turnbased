using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public List<string> lines;
    public List<string> repeatLines;
}
