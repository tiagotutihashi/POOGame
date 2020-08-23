using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

    public List<BattleStats> charactersUi = new List<BattleStats>();
    public List<CharacterStats> characterStatus = new List<CharacterStats>();

    public bool battling;

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

    void Start() {

    }

    void Update() {

        if (battling) {

            //if(battleMenus[0].action && battleMenus[1].action) {

            //}

        }

    }

    private void SetStatsUi(BattleStats painel, CharacterStats charStats) {
        painel.charName.text = charStats.charName;
        painel.lv.text = "Lv " + charStats.level;
        painel.hpText.text = charStats.hp.ToString() + "/" + charStats.maxHp;
        painel.hpBar.SetMaxValue(charStats.maxHp);
        painel.hpBar.SetValue(charStats.hp);
        painel.mnText.text = charStats.mana.ToString() + "/" + charStats.maxMana;
        painel.mnBar.SetMaxValue(charStats.maxMana);
        painel.mnBar.SetValue(charStats.mana);
    }

    public void BattleStart(EnemyStats[] enemies) {

        GameManager.instance.canMove = false;
            
        charactersUi.Clear();

        charactersUi.Add(GameObject.Find("Stats 0").GetComponent<BattleStats>());
        charactersUi.Add(GameObject.Find("Stats 1").GetComponent<BattleStats>());
        charactersUi.Add(GameObject.Find("Stats 2").GetComponent<BattleStats>());
        charactersUi.Add(GameObject.Find("Stats 3").GetComponent<BattleStats>());

        characterStatus.Clear();

        characterStatus.Add(GameManager.instance.player[0]);
        characterStatus.Add(GameManager.instance.player[1]);

        foreach (EnemyStats enemy in enemies) {
            enemyName.Add(enemy.charName);
            enemyLevel.Add(enemy.level);
            enemyHp.Add(enemy.hp);
            enemyMaxHp.Add(enemy.maxHp);
            enemyMana.Add(enemy.mana);
            enemyMaxMana.Add(enemy.maxMana);
            enemyAttack.Add(enemy.attack);
            enemyDefense.Add(enemy.defense);
        }

        enemyAttackList = enemies[0].attackList;
        enemyAttackList1 = enemies[1].attackList;

        SetStatsUi(charactersUi[0], characterStatus[0]);
        SetStatsUi(charactersUi[1], characterStatus[1]); 

   
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

        battling = true;

    }

}
