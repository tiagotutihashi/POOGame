using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFirstDoor : MonoBehaviour {

    public List<string> dialogLines = new List<string>();
    public List<int> dialogSpearkers = new List<int>();

    public bool once = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!once)
            GameManager.instance.dialogManager.StartDialog(dialogLines, dialogSpearkers);

    }

    private void OnTriggerExit2D(Collider2D collision) {

        once = true;

    }

}
