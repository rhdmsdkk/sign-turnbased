using Common;
using Engine;
using System.Collections.Generic;
using UnityEngine;

public class ReviewPanel : MonoBehaviour
{
    public GameObject review;
    public GameObject slrEngine;
    public ReviewVideo video;

    [HideInInspector]
    public Interactable callingInteractable;

    private string word;
    private SimpleExecutionEngine engine;
    private int frame = 0;
    private bool finalInteraction = false;

    public void Start()
    {
        engine = slrEngine.GetComponent<SimpleExecutionEngine>();
        slrEngine.SetActive(false);
        gameObject.SetActive(false);
    }

    public void StartVideo(string word)
    {
        this.word = word;
        review.SetActive(true);
        video.PlayReviewVideo(this.word);
    }

    public void StartVideo(string word, bool finalInteraction)
    {
        StartVideo(word);
        this.finalInteraction = finalInteraction;
    }

    public void EndReview()
    {
        slrEngine.SetActive(false);
        gameObject.SetActive(false);

        if (callingInteractable != null)
        {
            callingInteractable.TriggerNewAction();

            callingInteractable = null;
        }

        if (finalInteraction)
        {
            LevelManager.instance.LoadCurrentEpisode();
        }
    }


    public void StartPlayerSign()
    {
        review.SetActive(false);
        slrEngine.SetActive(true);

        engine.recognizer.outputFilters.Clear();
        engine.recognizer.outputFilters.Add(new Thresholder<string>(0.5f));
        engine.recognizer.outputFilters.Add(new FocusSublistFilter<string>(new List<string> { word }));

        engine.recognizer.AddCallback("print", sign => {
            Debug.Log("Got Sign: " + sign);

            if (sign.ToLower() == word)
            {
                EndReview();
            }
        });

        frame = 0;
    }

    public void Update()
    {
        if (frame >= 120)
        {
            frame = 0;
            engine.buffer.TriggerCallbacks();
        }
        else
        {
            frame++;
        }
    }
}