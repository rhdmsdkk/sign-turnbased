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
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
	{
        combatButtons.SetActive(false);
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

    void PlayerTurn()
	{
        Debug.Log("Player turn!");
        combatButtons.SetActive(true);
	}

    void EnemyTurn()
    {
    Debug.Log("Enemy's Turn!");
        combatButtons.SetActive(false);
    }

}
