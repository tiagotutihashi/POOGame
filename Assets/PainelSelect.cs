using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainelSelect : MonoBehaviour {

    public PainelItem defaultItem;

    public void SetPlayer(PlayerStats item) {

    }

    public void LoadPlayer() {

        foreach (PlayerStats item in GameManager.instance.player) {
            GameObject newB = Instantiate(defaultItem.gameObject, transform);
            newB.GetComponent<PainelItem>().charName.text = item.charName;
            newB.GetComponent<PainelItem>().image.sprite = item.charImage;
            newB.GetComponent<PainelItem>().hpBar.SetMaxValue(item.maxHp);
            newB.GetComponent<PainelItem>().hpBar.SetValue(item.hp);
            newB.GetComponent<PainelItem>().manaBar.SetMaxValue(item.maxMana);
            newB.GetComponent<PainelItem>().manaBar.SetValue(item.mana);
            newB.GetComponent<Button>().onClick.AddListener(() => SetPlayer(item));
        }

    }
   
}
