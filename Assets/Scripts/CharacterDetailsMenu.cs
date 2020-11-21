using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetailsMenu : MonoBehaviour {

    public CharacterListMenu list;

    public Image charImage;
    public Text charName;
    public Text charLv;

    public Text charHp;
    public Text charMana;
    public Text charExp;
    public Text charAtt;
    public Text charDef;

    public EquipCharacterMenu charAttItem;
    public EquipCharacterMenu charDefItem;

    public void LoadStatus() {

        gameObject.SetActive(true);

        PlayerStats player = list.playerToShow;

        charImage.sprite = player.charImage;
        charName.text = player.charName;
        charLv.text = "Lv:" + player.level;
        charHp.text = player.hp.ToString() + "/" + player.maxHp;
        charMana.text = player.mana.ToString() + "/" + player.maxMana;
        charExp.text = player.exp.ToString() + "/" + player.maxExp;
        charAtt.text = player.attack.ToString();
        charDef.text = player.defense.ToString();

        charAttItem.equipSprite.sprite = player.attItem.image;
        charAttItem.equipName.text = player.attItem.itemName;
        charAttItem.equipAtt.text = "Ata:\n" + player.attItem.attack;
        charAttItem.equipDef.text = "Def:\n" + player.attItem.defense;
        charAttItem.equipHp.text = "Hp:\n" + player.attItem.hp;

        charDefItem.equipSprite.sprite = player.defItem.image;
        charDefItem.equipName.text = player.defItem.itemName;
        charDefItem.equipAtt.text = "Ata:\n" + player.defItem.attack;
        charDefItem.equipDef.text = "Def:\n" + player.defItem.defense;
        charDefItem.equipHp.text = "Hp:\n" + player.defItem.hp;

        GameManager.instance.terminarManager.AddMethod("CharacterDetailsMenu.LoadStatus()");

    }

    public void CloseMenu() {
        gameObject.SetActive(false);
    }

}
