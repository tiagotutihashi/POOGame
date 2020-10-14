using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipList : MonoBehaviour {

    public Button defaultButton;

    public EquipItem itemToUse;

    public PainelSelect painelSelect;

    public void SetItem(EquipItem item) {
        itemToUse = item;
        painelSelect.LoadPlayer("equip");
    }

    public void LoadItems() {

        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        foreach (EquipItem item in GameManager.instance.playerEquipItems) {
            GameObject newB = Instantiate(defaultButton.gameObject, gameObject.transform);
            newB.GetComponent<EquipButton>().equipSprite.sprite = item.image;
            newB.GetComponent<EquipButton>().equipName.text = item.itemName;
            newB.GetComponent<EquipButton>().equipAtt.text = "Ata: " + item.attack.ToString();
            newB.GetComponent<EquipButton>().equipDef.text = "Def: " + item.defense.ToString();
            newB.GetComponent<EquipButton>().equipHp.text = "HP: " + item.hp.ToString();
            if (item.equiped != -1) {
                newB.GetComponent<EquipButton>().equipEquiped.color = new Color(newB.GetComponent<EquipButton>().equipEquiped.color.r, newB.GetComponent<EquipButton>().equipEquiped.color.g, newB.GetComponent<EquipButton>().equipEquiped.color.b, 1f);
                newB.GetComponent<EquipButton>().equipEquiped.sprite = GameManager.instance.player[item.equiped].charImage;
            }
            newB.GetComponent<Button>().onClick.AddListener(() => SetItem(item));
        }

        GameManager.instance.terminarManager.AddMethod("EquipList.LoadItems()");

    }

}
