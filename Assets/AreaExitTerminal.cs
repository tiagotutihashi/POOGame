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
                areaExit.setOpen(true);
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
