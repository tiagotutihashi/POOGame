using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterListMenu : MonoBehaviour {

    public Button[] playerContainer;

    public PlayerStats playerToShow;

    public CharacterDetailsMenu menu;

    public void SetItem(PlayerStats item) {
        playerToShow = item;
        menu.LoadStatus();
    }

    public void LoadItems() {

        for (int i = 0; i < GameManager.instance.player.Length; i++) {
            PlayerStats player = GameManager.instance.player[i];
            CharacterItemButton button = playerContainer[i].GetComponent<CharacterItemButton>();
            button.charSprite.sprite = player.charImage;
            button.charName.text = player.charName;
            button.charLvl.text = "Lv: " + player.level;

            button.hpBar.SetMaxValue(player.maxHp);
            button.hpBar.SetValue(player.hp);

            button.manaBar.SetMaxValue(player.maxMana);
            button.manaBar.SetValue(player.mana);

            button.expBar.SetMaxValue(player.CalcMaxExp());
            button.expBar.SetValue(player.exp);

            playerContainer[i].GetComponent<Button>().onClick.AddListener(() => SetItem(player));
        }

        GameManager.instance.terminarManager.AddMethod("CharacterListMenu.LoadItems()");

    }
}
