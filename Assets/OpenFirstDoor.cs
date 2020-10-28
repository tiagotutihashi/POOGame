using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFirstDoor : MonoBehaviour {

    public List<string> dialogLines = new List<string>();
    public List<int> dialogSpearkers = new List<int>();

    public bool once = false;

    public AreaExit door;

    private void Start() {
        if(GameManager.instance.eventsDone[0] == 1) {
            door.setOpen(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!once && !door.open && GameManager.instance.eventsDone[0] == 0) {
            GameManager.instance.dialogManager.StartDialog(dialogLines, dialogSpearkers);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        once = true;

    }

}
