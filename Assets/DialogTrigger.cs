using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public List<string> dialogLines = new List<string>();
    public List<int> dialogSpeakers = new List<int>();
    public List<Sprite> dialogImages = new List<Sprite>();
    public int eventNumber;

    public EnemyMovement enemies;

    void Update() {

        if (enemies != null && GameManager.instance.verifyEvent(eventNumber)) {
            GameManager.instance.EnemyTrigger(enemies);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" && !GameManager.instance.verifyEvent(eventNumber)) {
            GameManager.instance.dialogManager.StartDialog(dialogLines, dialogSpeakers, dialogImages);
            GameManager.instance.increaseEventCount(eventNumber);
        }
    }

}
