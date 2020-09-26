using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int gold;
    public static GameManager instance;
    public BattleManager battleManager;
    public GameObject menu;

    public PlayerStats[] player;

    public List<UseItem> playerUseItems = new List<UseItem>();
    public List<int> playerUseItemsAmount;
    public List<UseItem> allUseItems;

    public bool canMove = true;
    public bool enemyMove = true;

    public TabButton characterButton;
    public Text savedGame;

    void Start() {

        if(GameManager.instance == null) {
            instance = this;

            DontDestroyOnLoad(this);

            GetUseItems();
        } else {

            Destroy(gameObject);

        }

    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Q)) {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (menu.activeInHierarchy) {
                DeactivateMenu();
            } else {
                ActivateMenu();
            }
        }

    }

    public void DeactivateMenu() {
        savedGame.text = "";
        menu.SetActive(false);

    }

    public void ActivateMenu() {
        menu.SetActive(true);
        menu.GetComponentInChildren<TabGroup>().OnTabSelect(characterButton);
    }

    public void EnemyTrigger(EnemyMovement toBattle) {

        EnemyMovement clone = toBattle;
        Destroy(toBattle.gameObject);
        battleManager.gameObject.SetActive(true);
        battleManager.BattleStart(clone.toBattle);

    }

    public void GetUseItems() {

        playerUseItems.Clear();

        for (int i = 0; i < playerUseItemsAmount.Count; i++) {

            UseItem newItem = allUseItems[i];
            newItem.amount = playerUseItemsAmount[i];
            playerUseItems.Add(newItem);

        }

    }

    public void DecreaseUseItems(string itemName) {

        playerUseItems.Clear();

        for (int i = 0; i < playerUseItemsAmount.Count; i++) {

            UseItem newItem = allUseItems[i];
            if (newItem.itemName == itemName) {
                newItem.amount = playerUseItemsAmount[i] - 1;
                playerUseItemsAmount[i] -= 1;
            } else {
                newItem.amount = playerUseItemsAmount[i];
            }
            playerUseItems.Add(newItem);

        }

    }

    public void SaveGame() {

        PlayerPrefs.SetInt("gold", gold);

        PlayerPrefs.SetInt("Current_Scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("Player_Position_x", player[0].gameObject.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", player[0].gameObject.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", player[0].gameObject.transform.position.z);

        for (int i = 0; i <= player.Length - 1; i++) {

            PlayerPrefs.SetString("Player_" + i, player[i].charName);
            PlayerPrefs.SetInt("Player_" + i + "_Level", player[i].level);
            PlayerPrefs.SetInt("Player_" + i + "_Exp", player[i].exp);
            PlayerPrefs.SetInt("Player_" + i + "_Max_Hp", player[i].maxHp);
            PlayerPrefs.SetInt("Player_" + i + "_Current_Hp", player[i].hp);
            PlayerPrefs.SetInt("Player_" + i + "_Max_Mana", player[i].maxMana);
            PlayerPrefs.SetInt("Player_" + i + "_Current_Mana", player[i].mana);
            PlayerPrefs.SetInt("Player_" + i + "_Attack", player[i].attack);
            PlayerPrefs.SetInt("Player_" + i + "_Defense", player[i].defense);
            PlayerPrefs.SetString("Player_" + i + "_Weapon", player[i].attItem.itemName);
            PlayerPrefs.SetString("Player_" + i + "_Armor", player[i].defItem.itemName);

        }

        for (int i = 0; i <= allUseItems.Count - 1; i++) {

            PlayerPrefs.SetInt("UseItem_" + i, allUseItems[i].amount);

        }

    }

    public void LoadGame() {

        gold = PlayerPrefs.GetInt("gold");

        for (int i = 0; i <= player.Length - 1; i++) {

            player[i].charName = PlayerPrefs.GetString("Player_" + i);
            player[i].level = PlayerPrefs.GetInt("Player_" + i + "_Level");
            player[i].exp = PlayerPrefs.GetInt("Player_" + i + "_Exp");
            player[i].maxHp = PlayerPrefs.GetInt("Player_" + i + "_Max_Hp");
            player[i].hp = PlayerPrefs.GetInt("Player_" + i + "_Current_Hp");
            player[i].maxMana = PlayerPrefs.GetInt("Player_" + i + "_Max_Mana");
            player[i].mana = PlayerPrefs.GetInt("Player_" + i + "_Current_Mana");
            player[i].attack = PlayerPrefs.GetInt("Player_" + i + "_Attack");
            player[i].defense = PlayerPrefs.GetInt("Player_" + i + "_Defense");
            player[i].attItem.itemName = PlayerPrefs.GetString("Player_" + i + "_Weapon");
            player[i].defItem.itemName = PlayerPrefs.GetString("Player_" + i + "_Armor");

        }

        /*for (int i = 0; i <= allUseItems.Count - 1; i++) {

            PlayerPrefs.SetInt("UseItem_" + i, allUseItems[i].amount);

        } */

    }

    public void GoToMainMenu() {

        GetComponentInChildren<LevelLoader>().LoadToMainMenu();
        menu.SetActive(false);

    }

}
