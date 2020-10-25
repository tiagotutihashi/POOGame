using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public List<string> dialogLines = new List<string>();
    public List<int> dialogCharacter = new List<int>();

    public List<string> dialogCharName = new List<string>();
    public List<Sprite> dialogCharSprite = new List<Sprite>();

    public int index = 0;

    public Text message;
    public Image charImage;
    public Text charName;
    public float typingSpeed = 0.02f;

    private IEnumerator currentChat;

    public bool lineEnd = true;

    IEnumerator Type() {

        charName.text = dialogCharName[dialogCharacter[index]];
        charImage.sprite = dialogCharSprite[dialogCharacter[index]];

        foreach (char letter in dialogLines[index].ToCharArray()) {
            message.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    public void NextSentence() {

        if(index < dialogLines.Count - 1) {
            index++;
            message.text = "";
            if (!lineEnd) {
                StopCoroutine(currentChat);
            }
            currentChat = Type();
            StartCoroutine(currentChat);
        } else {
            message.text = "";
            GameManager.instance.canMove = true;
            GameManager.instance.enemyMove = true;
            gameObject.SetActive(false);
        }
    }

    void Update() {

        if(message.text == dialogLines[index]) {
            lineEnd = true;
        } else {
            lineEnd = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            NextSentence();
        }

    }

    public void StartDialog(List<string> newDialog, List<int> howSpeaks) {

        gameObject.SetActive(true);

        GameManager.instance.canMove = false;
        GameManager.instance.enemyMove = false;

        dialogLines = newDialog;
        dialogCharacter = howSpeaks;

        index = 0; 
        message.text = "";
        currentChat = Type();
        StartCoroutine(currentChat);

    }

}
