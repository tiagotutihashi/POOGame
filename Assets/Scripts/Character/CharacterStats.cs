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

    public List<AttackStats> attackList = new List<AttackStats>();

    public Sprite charImage;

}
