using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private LevelLoader loader;
    private CharacterMovement player;

    public float speed = 5f;
    public bool follow = false;

    public EnemyStats[] toBattle;

    void Start()  {

        loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        player = GameObject.Find("Player").GetComponent<CharacterMovement>();

    }

    
    void LateUpdate() {

        if (follow) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 1) {
                GameManager.instance.EnemyTrigger(toBattle);
                Destroy(this);
            }
        }
 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {
            if (Vector2.Distance(transform.position, player.transform.position) > 3)
                follow = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other) {

        if (other.tag == "Player") {
            follow = false;
        }

    }
}
