using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaExit : MonoBehaviour {

    public int areaToLoad;

    public string areaTransitionName;

    private LevelLoader loader;
    private CharacterMovement player;
    private AreaEntrace entrance;

    void Start() {
        entrance = GetComponentInChildren<AreaEntrace>();
        loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        player = GameObject.Find("Player").GetComponent<CharacterMovement>();

        entrance.transitionName = areaTransitionName;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            loader.LoadNextLevel(areaToLoad);

            player.areaTransitionName = areaTransitionName;
        }
    }

}
