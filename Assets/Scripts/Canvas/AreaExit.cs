using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaExit : MonoBehaviour {

    public int areaToLoad;

    private float directionX = 0.3f;
    private float directionY = 0.3f;

    public bool isX;
    public bool isY;
    public bool negativeX;
    public bool negativeY;

    public string areaTransitionName;

    private LevelLoader loader;
    private CharacterMovement player;
    public AreaEntrace entrance;

    public bool open = true;

    void Start() {
        entrance = GetComponentInChildren<AreaEntrace>();
        loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        player = GameObject.Find("Player").GetComponent<CharacterMovement>();

        entrance.transitionName = areaTransitionName;

        GameManager.instance.terminarManager.AddObj(gameObject.name,"AreaExit"); 

    }

    public void setOpen(bool newOpen) {

        open = newOpen;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && GameManager.instance.canMove) {
            if (isX) {
                if (negativeX) {
                    player.x = directionX * -1;
                } else {
                    player.x = directionX;
                }
                player.y = 0;
            }

            if (isY) {
                if (negativeY)
                    player.y = directionY * -1;
                else
                    player.y = directionY;
                player.x = 0;
            }

            player.areaTransitionName = areaTransitionName;

            loader.LoadNextLevel(areaToLoad);

        }
    }

}
