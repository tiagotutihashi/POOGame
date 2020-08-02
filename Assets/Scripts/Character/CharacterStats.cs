using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStats : MonoBehaviour {

    public string charName;
    public int level;
    public int hp;
    public int maxHp;
    public int mana;
    public int maxMana;
    public int attack;
    public int defense;

    public int TakeDamage(int damageToTake) {

        float result = (damageToTake) / defense;

        int damage = (int)result;

        hp -= damage;

        if (hp < 0) {
            hp = 0;
        }

        return damage;

    }

    public int InflictDamge(int attackPower) {

        float result = attack * attackPower;

        int damage = (int)result;

        return damage;

    }

}
