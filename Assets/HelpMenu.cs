using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour {

    public Button[] conceptsButton;

    public GameObject conceptPanel;
    public Text conceptText;
    public Image conceptImage;
    public Text conceptTitle;

    public void CloseMenu() {

        gameObject.SetActive(false);
        GameManager.instance.canMove = true;
        GameManager.instance.enemyMove = true;
        GameManager.instance.helpOpen = false;

    }

    public void OpenMenu() {

        if (!GameManager.instance.menuOpen && !GameManager.instance.dialogOpen && !GameManager.instance.battleManager.battling) {
            gameObject.SetActive(true);
            GameManager.instance.canMove = false;
            GameManager.instance.enemyMove = false;
            GameManager.instance.helpOpen = true;

            for (int i = 0; i <= conceptsButton.Length - 1; i++) {
                conceptsButton[i].gameObject.SetActive(false);
                if (i < GameManager.instance.conceptsLearned) {
                    conceptsButton[i].gameObject.SetActive(true);
                }
            }
        }

    }

    public void CloseConceptPainel() {

        conceptPanel.SetActive(false);

    }

}
