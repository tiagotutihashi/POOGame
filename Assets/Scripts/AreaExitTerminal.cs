using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaExitTerminal : MonoBehaviour {

    public AreaExit areaExit;

    void Start() {
        areaExit = GetComponent<AreaExit>();
    }

    public void ExecuteMethod(string method) {

        switch (method) {
            case "setOpen(true)":
                if (GameManager.instance.verifyEvent(3)) {
                    areaExit.setOpen(true);
                    GameManager.instance.increaseConcepLearned(2);
                }
                break;
            case "setOpen(false)":
                areaExit.setOpen(false);
                break;
            default:
                Debug.Log("Não existe");
                break;
        }

    }

}
