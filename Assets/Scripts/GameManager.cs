using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int gold;
    public static GameManager instance;
    public BattleManager battleManager;

    public PlayerStats[] player;

    public List<UseItem> playerUseItems = new List<UseItem>();
    public List<int> playerUseItemsIndex;
    public List<int> playerUseItemsAmount;
    public List<UseItem> allUseItems;

    public bool canMove = true;
    public bool enemyMove = true;

    void Start() {

        instance = this;

        DontDestroyOnLoad(this);

        GetUseItems();

    }

    void Update() {
        
    }

    public void EnemyTrigger(EnemyMovement toBattle) {

        EnemyMovement clone = toBattle;
        Destroy(toBattle.gameObject);
        battleManager.gameObject.SetActive(true);
        battleManager.BattleStart(clone.toBattle);
        

    }

    public void GetUseItems() {

        playerUseItems.Clear();

        for (int i = 0; i < playerUseItemsIndex.Count; i++) {

            UseItem newItem = allUseItems[playerUseItemsIndex[i]];
            newItem.amount = playerUseItemsAmount[i];
            playerUseItems.Add(newItem);

        }

    }

    public void DecreaseUseItems(string itemName) {

        playerUseItems.Clear();

        for (int i = 0; i < playerUseItemsIndex.Count; i++) {

            UseItem newItem = allUseItems[playerUseItemsIndex[i]];
            if (newItem.itemName == itemName) {
                newItem.amount = playerUseItemsAmount[i] - 1;
                playerUseItemsAmount[i] -= 1;
            } else {
                newItem.amount = playerUseItemsAmount[i];
            }
            playerUseItems.Add(newItem);

        }

    }


}
