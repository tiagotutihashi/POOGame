using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour {

    public Button backButton;

    public GameObject defaultButton;

    public GameObject itemMenu;

    public ItemTarget targetMenu;

    public TabGroup buttonGroup;

    public UseItem selectedItem;

    public GameObject initialMenu;

    public GameObject battleMenu;

    void SetItem(UseItem selected) {

        selectedItem = selected;
        GameManager.instance.terminarManager.AddMethod("ItemMenu.SetItem()");

    }

    public void LoadItems() {

        gameObject.SetActive(true);

        initialMenu.SetActive(false);

        foreach (Transform child in itemMenu.transform) {
            Destroy(child.gameObject);
        }

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

        Button newBackButton = Instantiate(backButton, itemMenu.transform);
        newBackButton.onClick.AddListener(() => gameObject.SetActive(false));
        newBackButton.onClick.AddListener(() => battleMenu.GetComponent<TabGroup>().BackTab());

        GameManager.instance.terminarManager.AddMethod("ItemMenu.LoadItems()");
    }

}
