using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public List<GameObject> objectsToSwap;

    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    public TabButton selectedButton;

    public void AddToTabList(TabButton button) {
        if(tabButtons == null) {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button) {

        ResetTabs();
        if (selectedButton == null || button != selectedButton) {
            button.background.sprite = tabHover;
        }

    }

    public void OnTabExit(TabButton button) {

        ResetTabs();

    }

    public void OnTabSelect(TabButton button) {

        selectedButton = button;
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();

        for(int i = 0; i < objectsToSwap.Count; i++) {
            if(i == index) {
                objectsToSwap[i].SetActive(true);
            } else {
                objectsToSwap[i].SetActive(false);
            }
        }

    }

    public void ResetTabs() {

        foreach(TabButton button in tabButtons) {

            if(selectedButton != null && button == selectedButton) { continue; }
            button.background.sprite = tabIdle;

        }

    }

}
