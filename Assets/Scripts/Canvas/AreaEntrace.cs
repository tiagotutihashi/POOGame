using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrace : MonoBehaviour {

    public string transitionName;

    private CharacterMovement player;

    void Start() {

        player = GameObject.Find("Player").GetComponent<CharacterMovement>();

        if(transitionName == player.areaTransitionName) {
            player.transform.position = transform.position;
        }
    }


}
