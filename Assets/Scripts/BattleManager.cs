using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

    public List<BattleStats> charactersUi = new List<BattleStats>();
    public List<CharacterStats> characterStatus = new List<CharacterStats>();

    public bool battling;

    void Start() {

        charactersUi.Add(GameObject.Find("Stats 0").GetComponent<BattleStats>());
        charactersUi.Add(GameObject.Find("Stats 1").GetComponent<BattleStats>());
        charactersUi.Add(GameObject.Find("Stats 2").GetComponent<BattleStats>());
        charactersUi.Add(GameObject.Find("Stats 3").GetComponent<BattleStats>());

    }

    void Update() {
        
        

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

        characterStatus.Clear();

        characterStatus.Add(GameManager.instance.player[0]);
        characterStatus.Add(GameManager.instance.player[1]);

        foreach (EnemyStats enemy in enemies)
            characterStatus.Add(enemy);

        SetStatsUi(charactersUi[0], characterStatus[0]);
        SetStatsUi(charactersUi[1], characterStatus[1]);

        int index = 2;
        foreach(CharacterStats enemy in characterStatus)
            if (enemy is EnemyStats) {
                SetStatsUi(charactersUi[index], characterStatus[index]);
                    index++;
            }

        battling = true;

    }

}
