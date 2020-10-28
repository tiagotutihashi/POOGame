using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerminarManager : MonoBehaviour {

    public Text methodsText;
    public Text objsText;
    public TMP_InputField inputText;

    public List<string> methodsLines;
    public List<string> objsLines;
    public List<string> objsTypes;

    public int maxLines = 10;

    public void StopWrite() {

        GameManager.instance.canMove = true;
        GameManager.instance.menuOpen = false;

    }

    public void StartWhite() {

        GameManager.instance.canMove = false;
        GameManager.instance.menuOpen = true;

    }

    public void ExecuteMethod() {
        string inputValue = inputText.text; ;
        if (inputValue.Split('.').Length == 2) {
            string obj = inputValue.Split('.')[0];
            string method = inputValue.Split('.')[1];
            GameObject gameO = GameObject.Find(obj);

            if (gameO.GetComponent<AreaExitTerminal>()) {
                gameO.GetComponent<AreaExitTerminal>().ExecuteMethod(method);
            }
        }

        inputText.text = "";
    }

    public void UpdateMethodList() {

        string lines = "";

        foreach (string line in methodsLines) {
            lines += line + "\n";
        }

        methodsText.text = lines;

    }

    public void UpdateObjList() {

        string lines = "";

        for (int i = 0; i <= objsLines.Count - 1; i++) {
            lines += objsLines[i] + "-" + objsTypes[i] + "\n";
        }

        objsText.text = lines;

    }

    public void AddMethod(string line) {
        methodsLines.Add(line);
        if (methodsLines.Count >= maxLines) {
            methodsLines.RemoveAt(0);
        }
        UpdateMethodList();
    }

    public void AddObj(string line, string type) {
        objsLines.Add(line);
        objsTypes.Add(type);
        if (objsLines.Count >= maxLines) {
            objsLines.RemoveAt(0);
            objsTypes.RemoveAt(0);
        }
        UpdateObjList();
    }

}
