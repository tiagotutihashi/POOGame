using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

    public int exp;
    public int maxExp;
    public int expBase;

    public EquipItem attItem;
    public EquipItem defItem;

    public int TakeDamage(int damageToTake) {

        float result = (damageToTake) / (defense + defItem.defense + attItem.defense);

        int damage = (int)result;

        hp -= damage;

        if(hp < 0) {
            hp = 0;
        }

        return damage;

    }

    public int InflictDamge(int attackPower) {

        float result = (attack + attItem.attack + defItem.attack) * attackPower;

        int damage = (int)result;

        return damage;

    }

    public int CalcMaxExp() {

        maxExp = (expBase * level) / 7;
        return maxExp;

    }

}
