using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelSelect : MonoBehaviour {

    public GameObject parentObject;

    public PainelItem defaultItem;

    public ItemList itemList;
    public EquipList equipList;

    public void SetPlayer(PlayerStats item, string type) {

        PlayerStats player;
        PlayerStats player2;
        if(GameManager.instance.player[0] == item) {
            player = GameManager.instance.player[0];
            player2 = GameManager.instance.player[1];
        } else {
            player = GameManager.instance.player[1];
            player2 = GameManager.instance.player[0];
        }

        if (type == "item") {
            player.hp += itemList.itemToUse.hp;
            if (player.hp > player.maxHp)
                player.hp = player.maxHp;
            player.mana += itemList.itemToUse.mana;
            if (player.mana > player.maxMana)
                player.mana = player.maxMana;
            GameManager.instance.DecreaseUseItems(itemList.itemToUse.itemName);
            itemList.LoadItems();
        }

        if (type == "equip") {
            if (equipList.itemToUse.type == 0) {
                if (player2.attItem == equipList.itemToUse) {
                    player2.attItem = player.attItem;
                }
                player.attItem = equipList.itemToUse;
            } else {
                if (player2.defItem == equipList.itemToUse) {
                    player2.defItem = player.defItem;
                }
                player.defItem = equipList.itemToUse;
            }
            GameManager.instance.GetEquipItems();
            equipList.LoadItems();
        }

        parentObject.SetActive(false);

    }

    public void LoadPlayer(string type) {

        parentObject.SetActive(true);

        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        foreach (PlayerStats item in GameManager.instance.player) {
            GameObject newB = Instantiate(defaultItem.gameObject, transform);
            newB.GetComponent<PainelItem>().charName.text = item.charName;
            newB.GetComponent<PainelItem>().image.sprite = item.charImage;
            newB.GetComponent<Button>().onClick.AddListener(() => SetPlayer(item, type));

            newB.GetComponent<PainelItem>().useContainer.SetActive(false);
            newB.GetComponent<PainelItem>().equipContainer.SetActive(false);

            if (type == "item") {
                newB.GetComponent<PainelItem>().useContainer.SetActive(true);
                newB.GetComponent<PainelItem>().hpBar.SetMaxValue(item.maxHp);
                newB.GetComponent<PainelItem>().hpBar.SetValue(item.hp);
                newB.GetComponent<PainelItem>().manaBar.SetMaxValue(item.maxMana);
                newB.GetComponent<PainelItem>().manaBar.SetValue(item.mana);
                if (item.mana == item.maxMana && itemList.itemToUse.mana > 0 || item.hp == item.maxHp && itemList.itemToUse.hp > 0) {
                    newB.GetComponent<Button>().interactable = false;
                }
            }

            if (type == "equip") {
                newB.GetComponent<PainelItem>().equipContainer.SetActive(true);
                int valueChangeAttack = 0;
                int valueChangeDefense = 0;
                int valueChangeHp = 0;
                if (equipList.itemToUse.type == 0) {
                    valueChangeAttack = equipList.itemToUse.attack + item.defItem.attack;
                    valueChangeDefense = equipList.itemToUse.defense + item.defItem.defense;
                    valueChangeHp = equipList.itemToUse.hp + item.defItem.hp;
                } else {
                    valueChangeAttack = equipList.itemToUse.attack + item.attItem.attack;
                    valueChangeDefense = equipList.itemToUse.defense + item.attItem.defense;
                    valueChangeHp = equipList.itemToUse.hp + item.attItem.hp;
                }
                newB.GetComponent<PainelItem>().charAtt.text = "Ata:\n" + (item.attItem.attack + item.defItem.attack) + " > " + valueChangeAttack;
                newB.GetComponent<PainelItem>().charDef.text = "Def:\n" + (item.attItem.defense + item.defItem.defense) + " > " + valueChangeDefense;
                newB.GetComponent<PainelItem>().charHp.text = "HP:\n" + (item.attItem.hp + item.defItem.hp) + " > " + valueChangeHp; ;
                if (item.attItem == equipList.itemToUse || item.defItem == equipList.itemToUse) {
                    newB.GetComponent<Button>().interactable = false;
                }
            }
        }

        GameManager.instance.terminarManager.AddMethod("PainelSelect.LoadPlayer()");

    }

    public void CloseMenu() {
        parentObject.SetActive(false);
    }
}
