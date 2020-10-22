using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour {
    public UseItem selected;

    public ItemTarget playerTarget;

    public Text itemName;
    public Text itemHp;
    public Text itemMn;
    public Text itemAmount;

    public void SetItem() {

        GetComponentInParent<Transform>().GetComponentInParent<ItemMenu>().selectedItem = selected;
        playerTarget = GetComponentInParent<Transform>().GetComponentInParent<ItemMenu>().targetMenu;
        playerTarget.selected = selected;
        playerTarget.gameObject.SetActive(true);
        playerTarget.LoadItems();
        GetComponentInParent<Transform>().GetComponentInParent<ItemMenu>().itemMenu.SetActive(false);
        

    }
}
