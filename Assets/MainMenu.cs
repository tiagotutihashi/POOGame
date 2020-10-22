using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public LevelLoader load;
    public Button[] buttons;

    void Start() {

        load = GameManager.instance.gameObject.GetComponentInChildren<LevelLoader>();

    }

    private void DisableButtons(bool interactable) {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = interactable;
        }
    }

    public void NewGame() {
        DisableButtons(false);
        load.LoadFromLoad(2);
    }

    public void Continue() {
        DisableButtons(false);
        load.LoadFromMainMenu();
    }

    public void ExitGame() {
        DisableButtons(false);
        Application.Quit();
    }
}
