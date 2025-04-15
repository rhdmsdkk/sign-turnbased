using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public DialogueScriptableObject dialogue;
    public UnityEngine.UI.Image interactIndicator;
    public GameObject dialoguePanel;

    private bool hasInteracted = false;
    private bool isPlayingDialogue = false;
    private PlayerMovement player;
    private TextMeshProUGUI dialogueDisplay;
    private List<string> lines;

    private int i = 0;

    private void Start()
    {
        interactIndicator.enabled = false;
        player = FindAnyObjectByType<PlayerMovement>();
        dialogueDisplay = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        lines = dialogue.lines;
    }

    private void Update()
    {
        if (isPlayingDialogue && Input.GetMouseButtonDown(0))
        {
            i += 1;
        }

        if (i >= lines.Count)
        {
            EndDialogue();
        } 
        else
        {
            dialogueDisplay.text = lines[i];
        }
    }

    public void PlayDialogue()
    {
        if (hasInteracted)
        {
            lines = dialogue.repeatLines;
        }

        dialogueDisplay.text = lines[i];

        player.DisableMovement();
        isPlayingDialogue = true;
        dialoguePanel.SetActive(true);
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isPlayingDialogue = false;
        player.EnableMovement();

        i = 0;
        hasInteracted = true;
    }

    #region Detection Range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactIndicator.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactIndicator.enabled = false;
        }
    }
    #endregion
}
