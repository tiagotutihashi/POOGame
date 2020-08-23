﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTarget : MonoBehaviour {

    public List<PlayerStats> players = new List<PlayerStats>();

    public BattleManager battleManager;

    public UseItem selected;

    public Button backButton;

    public Transform[] enemyPosition;

    public Button[] targetButtons;

    public BattleMenu battleMenu;

    public GameObject itemMenu;

    IEnumerator AttackAnimation(int target) {

        //Using item
        //HP
        battleManager.characterStatus[battleMenu.playerId].hp += selected.hp;
        if (battleManager.characterStatus[battleMenu.playerId].hp > battleManager.characterStatus[battleMenu.playerId].maxHp) {
            battleManager.characterStatus[battleMenu.playerId].hp = battleManager.characterStatus[battleMenu.playerId].maxHp;
        }
        battleManager.charactersUi[battleMenu.playerId].hpBar.SetValue(battleManager.characterStatus[battleMenu.playerId].hp);
        battleManager.charactersUi[battleMenu.playerId].hpText.text = battleManager.characterStatus[battleMenu.playerId].hp.ToString() + "/" + battleManager.characterStatus[battleMenu.playerId].maxHp;

        //Mana
        battleManager.characterStatus[battleMenu.playerId].mana += selected.mana;
        if (battleManager.characterStatus[battleMenu.playerId].mana > battleManager.characterStatus[battleMenu.playerId].maxMana) {
            battleManager.characterStatus[battleMenu.playerId].mana = battleManager.characterStatus[battleMenu.playerId].maxMana;
        }
        battleManager.charactersUi[battleMenu.playerId].mnBar.SetValue(battleManager.characterStatus[battleMenu.playerId].mana);
        battleManager.charactersUi[battleMenu.playerId].mnText.text = battleManager.characterStatus[battleMenu.playerId].mana.ToString() + "/" + battleManager.characterStatus[battleMenu.playerId].maxMana;

        //Decrease amount
        GameManager.instance.DecreaseUseItems(selected.itemName);

        //Set player action for the turn
        battleMenu.action = true;

        //Disable menus
        itemMenu.SetActive(true);
        gameObject.SetActive(false);
        GetComponentInParent<ItemMenu>().gameObject.SetActive(false);
        battleMenu.GetComponent<TabGroup>().mainMenu.SetActive(true);
        battleMenu.gameObject.SetActive(false);

        yield return null;

    }

    public void UseItem(int enemy) {

        StartCoroutine(AttackAnimation(enemy));

    }

    public void BackButton() {

        itemMenu.SetActive(true);
        gameObject.SetActive(false);

    }

    void Start() {

        players.AddRange(GameManager.instance.player);

        targetButtons[0].gameObject.SetActive(false);
        targetButtons[1].gameObject.SetActive(false);

        for (int i = 0; i <= players.Count - 1; i++) {

            targetButtons[i].gameObject.SetActive(true);
            targetButtons[i].GetComponentInChildren<Text>().text = players[i].charName;

            if ((selected.mana > 0 && (players[i].mana == players[i].maxMana)) || (selected.hp > 0 && (players[i].hp == players[i].maxHp))) {
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].interactable = false;
                targetButtons[i].GetComponentInChildren<Text>().color = Color.red;
            }

        }

        backButton.transform.SetAsLastSibling();

    }

}
