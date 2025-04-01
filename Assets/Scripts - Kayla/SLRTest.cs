using System.Collections.Generic;
using Common;
using Engine;
using UnityEngine;

public class SLRTest : MonoBehaviour
{
    public SimpleExecutionEngine engine;
    public BattleSystem battleSystem;
    public BattleHUD enemyHUD;

    private bool isInitialized = false;
    private bool isActive = false;
    private int frame = 0;
    private bool signVerified = false;

    // Public list of available signs (now accessible by BattleSystem)
    public List<string> levelSigns = new List<string> {
        "yellow",
        "dance"
    };

    private void OnEnable()
    {
        battleSystem.OnAttackInitiated += ActivateSLR;
    }

    private void OnDisable()
    {
        battleSystem.OnAttackInitiated -= ActivateSLR;
    }

    private void ActivateSLR()
    {
        if (!isInitialized)
        {
            InitializeSLR();
        }
        isActive = true;
        signVerified = false;
        frame = 0;
    }

    private void InitializeSLR()
    {
        engine.recognizer.outputFilters.Clear();
        engine.recognizer.outputFilters.Add(new Thresholder<string>(0.5f));
        engine.recognizer.outputFilters.Add(new FocusSublistFilter<string>(levelSigns));
        
        engine.recognizer.AddCallback("print", sign => {
            Debug.Log("Got Sign: " + sign);
            
            if (sign.ToLower() == enemyHUD.nameText.text.ToLower())
            {
                signVerified = true;
                battleSystem.HandleCorrectSign();
            }
        });
        
        isInitialized = true;
    }

    void Update()
    {
        if (battleSystem.state != BattleState.PLAYERTURN)
        {
            isActive = false;
            return;
        }

        if (isActive && !signVerified)
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
}