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

    public List<EquipItem> playerEquipItems = new List<EquipItem>();
    public int playerEquipItemsHave;
    public List<EquipItem> allEquipItems;

    public bool canMove = true;
    public bool enemyMove = true;
    public bool menuOpen = false;
    public bool dialogOpen = false;
    public bool helpOpen = false;

    public TabButton characterButton;
    public GameObject selectContainer;
    public GameObject charDetailsContainer;
    public CharacterListMenu characterListMenu;
    public Text savedGame;

    public TerminarManager terminarManager;

    public LevelLoader levelLoader;

    public DialogManager dialogManager;

    public List<int> eventsDone = new List<int>();

    public int conceptsLearned;
 
    public bool openMenuOnce = false;

    public GameObject topButtons;

    void Start() {

        if (GameManager.instance == null) {
            instance = this;
        } else {
            Destroy(GameManager.instance.gameObject);
            GameManager.instance = this;
        }

        DontDestroyOnLoad(this);

        GetUseItems();
        GetEquipItems();

    }

    void Update() {

        /* if (Input.GetKeyDown(KeyCode.Q) && canMove) {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.E) && canMove) {
            LoadGame();
        } */

        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (!battleManager.battling)
                if (menu.activeInHierarchy) {
                    DeactivateMenu();
                } else if (canMove) {
                    ActivateMenu();
                }
        }

    }

    public void DeactivateMenu() {
        savedGame.text = "";
        menu.SetActive(false);
        selectContainer.SetActive(false);
        charDetailsContainer.SetActive(false);

        canMove = true;
        enemyMove = true;
        menuOpen = false;

        terminarManager.AddMethod("GameManager.DeactivateMenu()");
    }

    public void ActivateMenu() {

        if (!helpOpen && !dialogOpen) {
            menu.SetActive(true);
            if (openMenuOnce)
                menu.GetComponentInChildren<TabGroup>().OnTabSelect(characterButton);

            openMenuOnce = true;

            characterListMenu.LoadItems();

            canMove = false;
            enemyMove = false;
            menuOpen = true;

            terminarManager.AddMethod("GameManager.ActivateMenu()");
        }
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

    public void GetEquipItems() {

        playerEquipItems.Clear();

        for (int i = 0; i < playerEquipItemsHave; i++) {

            EquipItem newItem = allEquipItems[i];
            newItem.equiped = -1;
            for (int f = 0; f < player.Length; f++) {
                if (newItem.itemName.Equals(player[f].attItem.itemName) || newItem.itemName.Equals(player[f].defItem.itemName)) {
                    newItem.equiped = f;
                }
            }
            playerEquipItems.Add(newItem);

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
            if (playerUseItemsAmount[i] > 0)
                playerUseItems.Add(newItem);
        }

        terminarManager.AddMethod("GameManager.DecreaseUseItems(" + itemName + ")");

    }

    public void SaveGame() {

        //Salvar o gold
        PlayerPrefs.SetInt("gold", gold);

        ///Salvar a posição personagem
        PlayerPrefs.SetInt("Current_Scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetFloat("Player_Position_x", player[0].gameObject.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", player[0].gameObject.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", player[0].gameObject.transform.position.z);

        //Salvar os status dos personagens
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

        //Salvar os itens consumíveis
        PlayerPrefs.SetInt("UseItem_Amount", playerUseItemsAmount.Count);

        for (int i = 0; i <= playerUseItemsAmount.Count - 1; i++) {

            PlayerPrefs.SetInt("UseItem_" + i, allUseItems[i].amount);

        }

        //Salvar os equips
        PlayerPrefs.SetInt("EquipItem", playerEquipItemsHave);

        //Salvar números de eventos feitos
        PlayerPrefs.SetInt("EventsDone_total", eventsDone.Count);
        for (int i = 0; i <= eventsDone.Count - 1; i++) {
            PlayerPrefs.SetInt("EventsDone_" + i, eventsDone[i]);
        }

        PlayerPrefs.SetInt("ConceptsLearned", conceptsLearned);

        terminarManager.AddMethod("GameManager.SaveGame()");

    }

    public void LoadGame() {

        // Carregar gold
        gold = PlayerPrefs.GetInt("gold");

        //Carregar itens consumíveis
        playerUseItems.Clear();

        for (int i = 0; i <= PlayerPrefs.GetInt("UseItem_Amount") - 1; i++) {
            playerUseItemsAmount[i] = PlayerPrefs.GetInt("UseItem_" + i);
        }

        //Carrregar equips
        playerEquipItemsHave = PlayerPrefs.GetInt("EquipItem");

        GetUseItems();
        GetEquipItems();

        // Carregar Status
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
            player[i].attItem = playerEquipItems.Find(item => item.itemName.Equals(PlayerPrefs.GetString("Player_" + i + "_Weapon")));
            player[i].defItem = playerEquipItems.Find(item => item.itemName.Equals(PlayerPrefs.GetString("Player_" + i + "_Armor")));
        }
 

        for (int i = 0; i <= PlayerPrefs.GetInt("EventsDone_total") - 1; i++) {

            eventsDone[i] = PlayerPrefs.GetInt("EventsDone_" + i);

        }

        conceptsLearned = PlayerPrefs.GetInt("ConceptsLearned");

        terminarManager.AddMethod("GameManager.LoadGame()");

    }

    public void GoToMainMenu() {

        GetComponentInChildren<LevelLoader>().LoadToMainMenu();
        menu.SetActive(false);

        terminarManager.AddMethod("GameManager.GoToMainMenu()");

    }

    public bool haveSave() {

        return PlayerPrefs.GetInt("Current_Scene") == 0;

    }

    public void DeactivateTopButtons() {

        topButtons.SetActive(false);

    }

    public void ActivateTopButtons() {

        topButtons.gameObject.SetActive(true);

    }

    public void increaseConcepLearned(int amount) {

        if(conceptsLearned < amount) {
            conceptsLearned = amount;
        }

    }

    public void increaseEventCount(int amount) {
        int theDiff = amount - eventsDone.Count;
        if (theDiff > 1) {
            for (int i = 0; i < theDiff; i++) {
                eventsDone.Add(1);
            }
        }
        if (eventsDone.Count < amount) {
            eventsDone.Add(1);
        } else {
            eventsDone[amount - 1] = 1;
        }

    }

    public bool verifyEvent(int number) {

        if (eventsDone.Count < number) {
            return false;
        } else {
            if(eventsDone[number - 1] == 1) {
                return true;
            }
        }

        return false;

    }

}
