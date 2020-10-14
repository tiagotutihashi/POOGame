using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminarManager : MonoBehaviour {

    public Text methodsText;
    public Text objsText;

    public List<string> methodsLines;
    public List<string> objsLines;

    public int maxLines = 10;

    public void UpdateMethodList() {

        string lines = "";

        foreach(string line in methodsLines) {
            lines += line + "\n";
        }

        methodsText.text = lines;

    }

    public void UpdateObjList() {

        string lines = "";

        foreach (string line in objsLines) {
            lines += line + "\n";
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

    public void AddObj(string line) {
        objsLines.Add(line);
        if (objsLines.Count >= maxLines) {
            objsLines.RemoveAt(0);
        }
        UpdateObjList();
    }

}
