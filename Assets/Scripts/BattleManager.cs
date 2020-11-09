using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

    public List<BattleStats> charactersUi = new List<BattleStats>();
    public List<PlayerStats> characterStatus = new List<PlayerStats>();

    public bool battling;
    public bool inspected;

    public BattleMenu[] battleMenus;

    public List<string> enemyName = new List<string>();
    public List<int> enemyLevel = new List<int>();
    public List<int> enemyHp = new List<int>();
    public List<int> enemyMaxHp = new List<int>();
    public List<int> enemyMana = new List<int>();
    public List<int> enemyMaxMana = new List<int>();
    public List<int> enemyAttack = new List<int>();
    public List<int> enemyDefense = new List<int>();

    public List<AttackStats> enemyAttackList = new List<AttackStats>();
    public List<AttackStats> enemyAttackList1 = new List<AttackStats>();

    public int gold;
    public int expToGive;

    public UseItem mayDrop;
    public int chanceToDrop;

    public List<Button> buttonInspect = new List<Button>();

    public Transform[] enemyPosition;

    public GameObject endPanel;
    public GameObject winPanel;
    public GameObject losePanel;

    public Text goldText;
    public Text expText;
    public Text[] playerNameText;
    public Text[] playerLvlText;
    public Bar[] playerBar;

    public Button continueButton;

    void Start() {

        GameManager.instance.terminarManager.AddObj(gameObject.name, "BattleManager");

    }

    void Update() {

        if (battling) {

            if (battleMenus[0].action && !battleMenus[1].action) {

                ProcessBattle();
                if (characterStatus[1].hp > 0) {
                    battleMenus[1].action = false;
                    battleMenus[1].gameObject.SetActive(true);
                }

            } else if (!battleMenus[0].action && battleMenus[1].action) {

                ProcessBattle();

            } else if (battleMenus[0].action && battleMenus[1].action) {
                EnemyTurns();
                ProcessBattle();
                if (characterStatus[0].hp > 0) {
                    battleMenus[0].action = false;
                    battleMenus[0].gameObject.SetActive(true);
                }
                if (characterStatus[1].hp > 0) {
                    battleMenus[1].action = false;
                    if (characterStatus[0].hp <= 0) {
                        battleMenus[1].gameObject.SetActive(true);
                    }
                }

            }

        }

    }

    private void EnemyTurns() {
        for (int index = 0; index <= enemyPosition.Length - 1; index++) {

            if (enemyPosition[index].gameObject.activeInHierarchy) {
                int playerTarget = Random.Range(0, 100);
                if (playerTarget % 2 == 0) {
                    if (characterStatus[0].hp > 0)
                        CalcDamage(0, index);
                    else
                        CalcDamage(1, index);
                } else {
                    if (characterStatus[1].hp > 0)
                        CalcDamage(1, index);
                    else
                        CalcDamage(0, index);
                }

            }

        }

        GameManager.instance.terminarManager.AddMethod("BattleManager.EnemyTurns()");

    }

    private void CleckAlive() {
        for (int index = 0; index <= characterStatus.Count - 1; index++) {
            if (characterStatus[index].hp > 0) {
                battleMenus[index].action = false;
                battleMenus[index].gameObject.SetActive(true);
            }
        }

        GameManager.instance.terminarManager.AddMethod("BattleManager.CheckAlive()");

    }

    private void ProcessBattle() {
        if (enemyHp[0] <= 0 && enemyHp[1] <= 0 && battling) {
            BattleEnd(true, false);
        }

        if (characterStatus[0].hp <= 0 && characterStatus[1].hp <= 0 && battling) {
            BattleEnd(false, true);
        }

        GameManager.instance.terminarManager.AddMethod("BattleManager.ProcessBattle()");
    }

    private void CalcDamage(int target, int enemy) {
        int damage = enemyAttack[enemy] * enemyAttackList1[Random.Range(0, enemyAttackList1.Count)].power / (characterStatus[target].defense + characterStatus[target].defItem.defense + characterStatus[target].attItem.defense);
        if (damage <= 0) {
            damage = 1;
        }
        characterStatus[target].hp -= damage;
        if (characterStatus[target].hp <= 0) {
            characterStatus[target].hp = 0;
        }
        charactersUi[target].hpText.text = characterStatus[target].hp.ToString() + "/" + characterStatus[target].maxHp;
        charactersUi[target].hpBar.SetValue(characterStatus[target].hp);

        GameManager.instance.terminarManager.AddMethod("BattleManager.CalcDamage()");
    }

    private void SetStatsUi(BattleStats painel, CharacterStats charStats, int target) {
        painel.charName.text = charStats.charName;
        painel.lv.text = "Lv " + charStats.level;
        painel.hpText.text = charStats.hp.ToString() + "/" + charStats.maxHp;
        painel.hpBar.SetMaxValue(charStats.maxHp);
        painel.hpBar.SetValue(charStats.hp);
        painel.mnText.text = charStats.mana.ToString() + "/" + charStats.maxMana;
        painel.mnBar.SetMaxValue(charStats.maxMana);
        painel.mnBar.SetValue(charStats.mana);
        if (charStats.hp <= 0) {
            battleMenus[target].action = true;
            battleMenus[target].gameObject.SetActive(false);
        }
    }

    public void Inspect(int playerId) {
        for (int index = 0; index <= 1; index++) {
            charactersUi[index + 2].charName.text = enemyName[index];
            charactersUi[index + 2].lv.text = "Lv " + enemyLevel[index];
            charactersUi[index + 2].hpText.text = enemyHp[index].ToString() + "/" + enemyMaxHp[index].ToString();
            charactersUi[index + 2].hpBar.SetMaxValue(enemyMaxHp[index]);
            charactersUi[index + 2].hpBar.SetValue(enemyHp[index]);
            charactersUi[index + 2].mnText.text = enemyMana[index].ToString() + "/" + enemyMaxMana[index];
            charactersUi[index + 2].mnBar.SetMaxValue(enemyMaxMana[index]);
            charactersUi[index + 2].mnBar.SetValue(enemyMana[index]);
        }

        battleMenus[playerId].action = true;
        battleMenus[playerId].gameObject.SetActive(false);

        inspected = true;

        for (int index = 0; index < buttonInspect.Count; index++) {
            buttonInspect[index].interactable = false;
        }

        GameManager.instance.terminarManager.AddMethod("BattleManager.Inspect()");

    }

    public void BattleEnd(bool win, bool lose) {

        if (win) {
            endPanel.SetActive(true);
            losePanel.SetActive(false);
            winPanel.SetActive(true);

            expText.text = "Exp: " + expToGive;
            goldText.text = "Ouro: " + GameManager.instance.gold + " + " + gold;
            GameManager.instance.gold += gold;

            for (int index = 0; index <= characterStatus.Count - 1; index++) {
                characterStatus[index].exp += expToGive;
                int levelUp = 0;
                bool stop = true;
                int expToLevelUp = 0;
                do {
                    expToLevelUp = characterStatus[index].CalcMaxExp();
                    if (characterStatus[index].exp >= expToLevelUp) {
                        characterStatus[index].exp -= expToLevelUp;
                        characterStatus[index].level += 1;
                        levelUp++;

                        //HP
                        characterStatus[index].maxHp += 5;
                        characterStatus[index].hp = characterStatus[index].maxHp;

                        //MANA
                        characterStatus[index].maxMana += 2;
                        characterStatus[index].mana = characterStatus[index].maxMana;

                        //ATTACK
                        characterStatus[index].attack += 4;

                        //DEFENSE
                        characterStatus[index].defense += 5;

                    } else {
                        stop = false;
                    }
                } while (stop);

                playerNameText[index].text = characterStatus[index].charName;
                if (levelUp > 0)
                    playerLvlText[index].text = "Lvl: " + characterStatus[index].level + " + " + levelUp;
                else
                    playerLvlText[index].text = "Lvl: " + characterStatus[index].level;
                playerBar[index].SetMaxValue(expToLevelUp);
                playerBar[index].SetValue(characterStatus[index].exp);
            }

        }

        if (lose) {
            if (GameManager.instance.haveSave()) {
                continueButton.gameObject.SetActive(false);
            }
            endPanel.SetActive(true);
            losePanel.SetActive(true);
            winPanel.SetActive(false);
        }
        battling = false;

        GameManager.instance.terminarManager.AddMethod("BattleManager.BattleEnd()");

    }

    public void ExitBattleField() {

        battling = false;
        gameObject.SetActive(false);
        GameManager.instance.canMove = true;
        GameManager.instance.enemyMove = true;
        GameManager.instance.ActivateTopButtons();

        GameManager.instance.terminarManager.AddMethod("BattleManager.ExitBattleField()");

    }

    public void EscapeBattle(int playerId) {

        int number1 = Random.Range(0, 100);
        int number2 = Random.Range(0, 100);
        if (number1 % 2 == 0 || number2 % 2 == 0) {
            ExitBattleField();
        }
        battleMenus[playerId].gameObject.SetActive(false);
        battleMenus[playerId].action = true;

        GameManager.instance.terminarManager.AddMethod("BattleManager.EscapeBattle()");

    }

    public void LoadGame() {
        ExitBattleField();
        GameManager.instance.player[0].GetComponent<CharacterMovement>().areaTransitionName = "";
        GameManager.instance.levelLoader.LoadFromMainMenu();
    }

    public void GoToMainMenu() {
        GameManager.instance.levelLoader.LoadToMainMenu();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void BattleStart(EnemyStats[] enemies) {

        endPanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        expToGive = 0;
        gold = 0;
        inspected = false;

        for (int index = 0; index < buttonInspect.Count; index++) {
            buttonInspect[index].interactable = true;
        }

        GameManager.instance.canMove = false;
        GameManager.instance.enemyMove = false;
        GameManager.instance.DeactivateTopButtons();

        for (int index = 0; index < battleMenus.Length; index++) {
            battleMenus[index].GetComponent<TabGroup>().mainMenu.SetActive(true);
            battleMenus[index].gameObject.SetActive(true);
        }

        charactersUi.Clear();

        charactersUi.Add(GameObject.Find("Stats 0").GetComponent<BattleStats>());
        charactersUi.Add(GameObject.Find("Stats 1").GetComponent<BattleStats>());
        charactersUi.Add(GameObject.Find("Stats 2").GetComponent<BattleStats>());
        charactersUi.Add(GameObject.Find("Stats 3").GetComponent<BattleStats>());

        characterStatus.Clear();

        characterStatus.Add(GameManager.instance.player[0]);
        characterStatus.Add(GameManager.instance.player[1]);

        enemyName.Clear();
        enemyLevel.Clear();
        enemyHp.Clear();
        enemyMaxHp.Clear();
        enemyMana.Clear();
        enemyMaxMana.Clear();
        enemyAttack.Clear();
        enemyDefense.Clear();

        foreach (EnemyStats enemy in enemies) {
            enemyName.Add(enemy.charName);
            enemyLevel.Add(enemy.level);
            enemyHp.Add(enemy.hp);
            enemyMaxHp.Add(enemy.maxHp);
            enemyMana.Add(enemy.mana);
            enemyMaxMana.Add(enemy.maxMana);
            enemyAttack.Add(enemy.attack);
            enemyDefense.Add(enemy.defense);
            expToGive += enemy.expToGive;
            gold += enemy.gold;
        }

        enemyAttackList = enemies[0].attackList;
        enemyAttackList1 = enemies[1].attackList;

        SetStatsUi(charactersUi[0], characterStatus[0], 0);
        SetStatsUi(charactersUi[1], characterStatus[1], 1);

        for (int index = 0; index <= 1; index++) {
            charactersUi[index + 2].charName.text = enemyName[index];
            charactersUi[index + 2].lv.text = "Lv???";
            charactersUi[index + 2].hpText.text = "???/???";
            charactersUi[index + 2].hpBar.SetMaxValue(999);
            charactersUi[index + 2].hpBar.SetValue(999);
            charactersUi[index + 2].mnText.text = "???/???";
            charactersUi[index + 2].mnBar.SetMaxValue(999);
            charactersUi[index + 2].mnBar.SetValue(999);
            enemyPosition[index].gameObject.SetActive(true);
        }

        battling = true;
        inspected = false;

        CleckAlive();
        battleMenus[1].gameObject.SetActive(false);

        GameManager.instance.terminarManager.AddMethod("BattleManager.BattleStart()");

    }

}
