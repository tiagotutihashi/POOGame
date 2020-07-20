using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStatus : MonoBehaviour {

    public string charName;
    public int level;
    public int hp;
    public int maxHp;
    public int attack;
    public int defense;

    public EquipItem attItem;
    public EquipItem defItem;

    public int caclDamage(int attackToUse, int powerToUse) {

        float result = (attackToUse * powerToUse) / defense;

        int damage = (int) result;

        return damage; 

    }

}
