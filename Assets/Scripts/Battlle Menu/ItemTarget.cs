﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTarget : MonoBehaviour {

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
        battleManager.characterStatus[target].hp += selected.hp;
        if (battleManager.characterStatus[target].hp > battleManager.characterStatus[target].maxHp) {
            battleManager.characterStatus[target].hp = battleManager.characterStatus[target].maxHp;
        }
        battleManager.charactersUi[target].hpBar.SetValue(battleManager.characterStatus[target].hp);
        battleManager.charactersUi[target].hpText.text = battleManager.characterStatus[target].hp.ToString() + "/" + battleManager.characterStatus[target].maxHp;

        //Mana
        battleManager.characterStatus[target].mana += selected.mana;
        if (battleManager.characterStatus[target].mana > battleManager.characterStatus[target].maxMana) {
            battleManager.characterStatus[target].mana = battleManager.characterStatus[target].maxMana;
        }
        battleManager.charactersUi[target].mnBar.SetValue(battleManager.characterStatus[target].mana);
        battleManager.charactersUi[target].mnText.text = battleManager.characterStatus[target].mana.ToString() + "/" + battleManager.characterStatus[target].maxMana;

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
        GameManager.instance.terminarManager.AddMethod("ItemTarget.UseItem()");

    }

    public void BackButton() {

        itemMenu.SetActive(true);
        gameObject.SetActive(false);

    }

    public void LoadItems() {

        targetButtons[0].gameObject.SetActive(false);
        targetButtons[1].gameObject.SetActive(false);

        for (int i = 0; i <= GameManager.instance.player.Length - 1; i++) {
            targetButtons[i].gameObject.SetActive(true);
            targetButtons[i].interactable = true;
            targetButtons[i].GetComponentInChildren<Text>().text = GameManager.instance.player[i].charName;
            targetButtons[i].GetComponentInChildren<Text>().color = Color.black;

            if ((selected.mana > 0 && (GameManager.instance.player[i].mana == GameManager.instance.player[i].maxMana)) || (selected.hp > 0 && (GameManager.instance.player[i].hp == GameManager.instance.player[i].maxHp))) {
                targetButtons[i].gameObject.SetActive(true);
                targetButtons[i].interactable = false;
                targetButtons[i].GetComponentInChildren<Text>().color = Color.red;
            }

        }

        backButton.transform.SetAsLastSibling();
    }

}
