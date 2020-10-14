using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour {

    public GameObject backButton;

    public GameObject defaultButton;

    public GameObject itemMenu;

    public ItemTarget targetMenu;

    public TabGroup buttonGroup;

    public UseItem selectedItem;

    void SetItem(UseItem selected) {

        selectedItem = selected;
        GameManager.instance.terminarManager.AddMethod("ItemMenu.SetItem()");

    }

    void Start() {

        buttonGroup = GetComponent<TabGroup>();

        foreach (UseItem item in GameManager.instance.playerUseItems) {
            GameObject newB = Instantiate(defaultButton, itemMenu.transform);
            newB.GetComponent<ItemSelect>().itemName.text = item.itemName;
            newB.GetComponent<ItemSelect>().itemHp.text = "HP: " + item.hp.ToString();
            newB.GetComponent<ItemSelect>().itemMn.text = "MN: " + item.mana.ToString();
            newB.GetComponent<ItemSelect>().itemAmount.text = "Q: " + item.amount.ToString();
            newB.GetComponent<ItemSelect>().selected = item;
            newB.GetComponent<Button>().onClick.AddListener(() => SetItem(item));
        }

        backButton.transform.SetAsLastSibling();

    }

}
