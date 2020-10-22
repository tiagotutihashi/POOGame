using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackMenu : MonoBehaviour {

    public PlayerStats player;

    public List<AttackStats> attackList = new List<AttackStats>();

    public GameObject defaultButton;

    public TabGroup buttonGroup;

    public Button backButton;

    public EnemyTargetMenu targetMenu;

    public GameObject attackMenu;

    public AttackStats selectedAttack;

    public int playerId;

    public GameObject initialMenu;

    public GameObject battleMenu;

    void SetAttack(AttackStats selected) {

        selectedAttack = selected;

    }

    public void LoadAttacks() {

        initialMenu.SetActive(false);

        gameObject.SetActive(true);

        buttonGroup = GetComponent<TabGroup>();

        attackList = player.attackList;

        foreach (Transform child in attackMenu.transform) {
            Destroy(child.gameObject);
        } 

        foreach (AttackStats attack in attackList) {
            GameObject newB = Instantiate(defaultButton, attackMenu.transform);
            newB.GetComponent<AttackSelect>().attackName.text = attack.attackName;
            newB.GetComponent<AttackSelect>().attackPower.text = "PW: " + attack.power.ToString();
            newB.GetComponent<AttackSelect>().attackCost.text = "MN: " + attack.cost.ToString();
            newB.GetComponent<TabButton>().tabGroup = buttonGroup;
            newB.GetComponent<AttackSelect>().selected = attack;
            newB.GetComponent<Button>().onClick.AddListener(() => SetAttack(attack));
            if (GameManager.instance.player[playerId].mana < attack.cost) {
                newB.GetComponent<Button>().interactable = false;
                newB.GetComponent<AttackSelect>().attackCost.color = Color.red;
            }
        }

        Button newBackButton = Instantiate(backButton, attackMenu.transform);
        newBackButton.onClick.AddListener(() => gameObject.SetActive(false));
        newBackButton.onClick.AddListener(() => battleMenu.GetComponent<TabGroup>().BackTab());

        GameManager.instance.terminarManager.AddMethod("AttackMenu.LoadAttacks()");

    }

}
