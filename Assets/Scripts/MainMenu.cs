﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public LevelLoader load;
    public Button[] buttons;

    public List<string> firstDilog = new List<string>();
    public List<int> firstSpeakers = new List<int>();
    public List<Sprite> firstImages = new List<Sprite>();

    public List<string> firstLevelDilog = new List<string>();
    public List<int> firstLevelSpeakers = new List<int>();
    public List<Sprite> firstLevelImages = new List<Sprite>();

    void Start() {

        load = GameManager.instance.gameObject.GetComponentInChildren<LevelLoader>();

        if (GameManager.instance.haveSave()) {
            buttons[1].gameObject.SetActive(false);
        }

    }

    private void DisableButtons(bool interactable) {
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = interactable;
        }
    }

    public void NewGame() {
        DisableButtons(false);
        //load.LoadFirst(8, firstDilog, firstSpeakers, firstImages);
        load.LoadFirstLevelFinalEvent(9, firstLevelDilog, firstLevelSpeakers, firstLevelImages, new Vector3(-10f, 0, 0), new Vector3(1f, 0, 0));
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
