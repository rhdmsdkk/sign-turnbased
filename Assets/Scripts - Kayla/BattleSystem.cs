using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public SLRTest slrTest;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public GameObject combatButtons;
    public GameObject reviewPopUp;
    public GameObject attackPopUp;
    public Text signText;
    public event System.Action OnAttackInitiated;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        combatButtons.SetActive(false);
        reviewPopUp.SetActive(false);
        attackPopUp.SetActive(false);
        
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = playerGO.GetComponent<Unit>();
        playerUnit.unitName = "";

        // Create enemy only once at start
        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponent<Unit>();
        RandomizeEnemyName();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void RandomizeEnemyName()
    {
        if (slrTest != null && slrTest.levelSigns.Count > 0)
        {
            int randomIndex = Random.Range(0, slrTest.levelSigns.Count);
            enemyUnit.unitName = slrTest.levelSigns[randomIndex];
            enemyHUD.nameText.text = enemyUnit.unitName; // Update HUD display
        }
    }

    public void HandleCorrectSign()
    {
        if (state == BattleState.PLAYERTURN && attackPopUp.activeSelf)
        {
            StartCoroutine(CompleteAttack(true));
        }
    }

    IEnumerator PlayerAttack()
    {
        combatButtons.SetActive(false);
        attackPopUp.SetActive(true);
        signText.text = $"Sign the enemy's name: {enemyUnit.unitName}";

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        yield return StartCoroutine(CompleteAttack(false));
    }

    IEnumerator CompleteAttack(bool signedCorrectly)
    {
        signText.text = signedCorrectly 
            ? $"Correct! You signed: {enemyUnit.unitName}"
            : $"Skipped! Attacking anyway";

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHP);

        yield return new WaitForSeconds(2f);
        signText.text = "";

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            // Only change name, don't create new enemy
            RandomizeEnemyName();
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy's Turn!");
        combatButtons.SetActive(false);
        attackPopUp.SetActive(false);

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        combatButtons.SetActive(false);
        attackPopUp.SetActive(false);
    }

    void PlayerTurn()
    {
        Debug.Log("Player's turn!");
        combatButtons.SetActive(true);
    }

    IEnumerator PlayerReview()
    {
        reviewPopUp.SetActive(true);
        yield return new WaitUntil(() => !reviewPopUp.activeSelf);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        OnAttackInitiated?.Invoke();
        StartCoroutine(PlayerAttack());
    }

    public void OnReviewButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerReview());
    }

    public void CloseReviewPopup()
    {
        if (reviewPopUp != null)
        {
            reviewPopUp.SetActive(false);
        }
    }

    public void Skip()
    {
        if (attackPopUp != null)
        {
            attackPopUp.SetActive(false);
        }
        StartCoroutine(EnemyTurn());
    }
}