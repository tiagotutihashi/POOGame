using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public Rigidbody2D rigid;
    public float x = 0;
    public float y = 0;
    public float speed = 5f;
    public string areaTransitionName;
    public bool canMove = true;
    private bool stop;

    void Start() {

        rigid = GetComponent<Rigidbody2D>();

    }

    void Update() {

        if (canMove) {
            Movement();
        } else {
            rigid.velocity = Vector2.zero;
        }

    }

    private void Movement() {

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (x != 0 || y != 0) {
            stop = false;
        } else {
            stop = true;
        }

        rigid.velocity = new Vector2(x, y) * speed;

    }
}
