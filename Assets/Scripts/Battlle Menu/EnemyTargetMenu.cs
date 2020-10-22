using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTargetMenu : MonoBehaviour {

    public Button defaultButton;

    public List<string> enemies = new List<string>();

    public BattleManager battleManager;

    public AttackStats selected;

    public Button backButton;

    public Transform[] enemyPosition;

    public Button[] targetButtons;

    public BattleMenu battleMenu;

    public GameObject attackMenu;

    IEnumerator AttackAnimation(int target) {

        //Calc damage
        int damage = (battleManager.characterStatus[battleMenu.playerId].attack * selected.power) / battleManager.enemyDefense[target];

        // Using the attack mana cost
        battleManager.characterStatus[battleMenu.playerId].mana -= selected.cost;
        battleManager.charactersUi[battleMenu.playerId].mnBar.SetValue(battleManager.characterStatus[battleMenu.playerId].mana);
        battleManager.charactersUi[battleMenu.playerId].mnText.text = battleManager.characterStatus[battleMenu.playerId].mana.ToString() + "/" + battleManager.characterStatus[battleMenu.playerId].maxMana;

        // Damage to enemy
        battleManager.enemyHp[target] -= damage;
        if (battleManager.inspected) {
            battleManager.charactersUi[target + 2].hpBar.SetValue(battleManager.enemyHp[target]);
            battleManager.charactersUi[target + 2].hpText.text = battleManager.enemyHp[target].ToString() + "/" + battleManager.enemyMaxHp[target].ToString();
        }

        if(battleManager.enemyHp[target] <= 0) {
            battleManager.enemyHp[target] = 0;
            enemyPosition[target].gameObject.SetActive(false);
        }

        //Set player action for the turn
        battleMenu.action = true;

        //Disable menus
        attackMenu.SetActive(true);
        gameObject.SetActive(false);
        GetComponentInParent<AttackMenu>().gameObject.SetActive(false);
        battleMenu.GetComponent<TabGroup>().mainMenu.SetActive(true);
        battleMenu.gameObject.SetActive(false);

        yield return null;

    }

    public void MakeAttack(int enemy) {

        StartCoroutine(AttackAnimation(enemy));

        GameManager.instance.terminarManager.AddMethod("EnemyTargetMenu.MakeAttack()");

    }

    public void BackButton() {

        attackMenu.SetActive(true);
        gameObject.SetActive(false);

    }

    public void LoadEnemiesButtons() {

        enemies = battleManager.enemyName;

        targetButtons[0].gameObject.SetActive(false);
        targetButtons[1].gameObject.SetActive(false);

        for (int i = 0; i <= enemies.Count - 1; i++) {

            targetButtons[i].gameObject.SetActive(true);
            targetButtons[i].GetComponentInChildren<Text>().text = enemies[i];
            targetButtons[i].interactable = true;

            if (battleManager.enemyHp[i] <= 0) {
                targetButtons[i].interactable = false;
            }

        }

        backButton.transform.SetAsLastSibling();

    }

}
