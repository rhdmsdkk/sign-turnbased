using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
	public GameObject enemyPrefab;

	Unit playerUnit;
	Unit enemyUnit;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

    public GameObject combatButtons;
    public GameObject reviewPopUp;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
	{
        combatButtons.SetActive(false);
        reviewPopUp.SetActive(false);
		GameObject playerGO = Instantiate(playerPrefab);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab);
		enemyUnit = enemyGO.GetComponent<Unit>();

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

    IEnumerator PlayerAttack()
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP);

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

    IEnumerator EnemyTurn()
	{
        Debug.Log("Enemy's Turn!");
        combatButtons.SetActive(false);

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

    void EndBattle()
	{
		combatButtons.SetActive(false);
	}


    void PlayerTurn()
	{
        Debug.Log("Player turn!");
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

}
