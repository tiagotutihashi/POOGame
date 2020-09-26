using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour {

    public Text savedGame;

    public void SaveGame() {

        GameManager.instance.SaveGame();
        savedGame.text = "Jogo Salvo";

    }

    public void ExitGame() {

        Application.Quit();

    }

}
