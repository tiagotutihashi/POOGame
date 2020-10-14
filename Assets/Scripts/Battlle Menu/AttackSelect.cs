using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSelect : MonoBehaviour {

    public AttackStats selected;

    public EnemyTargetMenu enemyTarget;

    public Text attackName;
    public Text attackPower;
    public Text attackCost;

    public void SetAttack() {

        GetComponentInParent<Transform>().GetComponentInParent<AttackMenu>().selectedAttack = selected;
        enemyTarget = GetComponentInParent<Transform>().GetComponentInParent<AttackMenu>().targetMenu;

        enemyTarget.gameObject.SetActive(true);
        enemyTarget.selected = selected;
        GetComponentInParent<Transform>().GetComponentInParent<AttackMenu>().attackMenu.SetActive(false);

        GameManager.instance.terminarManager.AddMethod("AttackSelect.SetAttack()");

    }

}
