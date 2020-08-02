using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int gold;
    public static GameManager instance;
    public BattleManager battleManager;

    public PlayerStats[] player;

    void Start() {

        instance = this;

        DontDestroyOnLoad(this);   
    }

    void Update() {
        
    }

    public void EnemyTrigger(EnemyStats[] toBattle) {

        battleManager.gameObject.SetActive(true);
        battleManager.BattleStart(toBattle);

    }
}
