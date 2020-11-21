using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour {

    public Button defaultButton;

    public UseItem itemToUse;

    public PainelSelect painelSelect;

    public void SetItem(UseItem item) {

        itemToUse = item;
        painelSelect.LoadPlayer("item");

    }

    public void LoadItems() {

        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        foreach (UseItem item in GameManager.instance.playerUseItems) {
            GameObject newB = Instantiate(defaultButton.gameObject, gameObject.transform);
            newB.GetComponent<ItemListItem>().image.sprite = item.image;
            newB.GetComponent<ItemListItem>().itemName.text = item.itemName;
            newB.GetComponent<ItemListItem>().itemHp.text = "HP: " + item.hp.ToString();
            newB.GetComponent<ItemListItem>().itemMana.text = "MN: " + item.mana.ToString();
            newB.GetComponent<ItemListItem>().itemAmount.text = "Q: " + item.amount.ToString();
            newB.GetComponent<Button>().onClick.AddListener(() => SetItem(item));
        }

        GameManager.instance.terminarManager.AddMethod("ItemList.LoadItems()");

    }

}
