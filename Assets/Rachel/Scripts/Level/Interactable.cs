using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public DialogueScriptableObject dialogue;
    public UnityEngine.UI.Image interactIndicator;
    public bool autoTrigger;
    public bool promptSign;
    public string promptSignString;
    public GameObject dialoguePanel;
    public ReviewPanel reviewPanel;

    private bool hasInteracted = false;
    private bool isPlayingDialogue = false;
    private PlayerMovement player;
    private TextMeshProUGUI dialogueDisplay;
    private List<string> lines;

    private bool canInteract = false;
    public bool triggerNewAction;
    public UnityEvent onDialogueCompleted;

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
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (isPlayingDialogue)
            {
                i += 1;
            }
            else if (canInteract)
            {
                PlayDialogue();
            }
        }

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
        interactIndicator.enabled = false;

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
        interactIndicator.enabled = false;

        dialoguePanel.SetActive(false);
        isPlayingDialogue = false;
        player.EnableMovement();

        i = 0;
        hasInteracted = true;

        if (promptSign)
        {
            PromptSign();
        }

        if (autoTrigger)
        {
            gameObject.SetActive(false);
        }
    }

    public void TriggerNewAction()
    {
        if (triggerNewAction)
        {
            onDialogueCompleted.Invoke();
        }
    }


    private void PromptSign()
    {
        reviewPanel.callingInteractable = this;
        reviewPanel.gameObject.SetActive(true);
        reviewPanel.StartVideo(promptSignString);
    }

    #region Detection Range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (autoTrigger)
            {
                PlayDialogue();
            }
            else if (!hasInteracted)
            {
                interactIndicator.enabled = true;
                canInteract = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactIndicator.enabled = false;
            canInteract = false;
        }
    }
    #endregion
}
