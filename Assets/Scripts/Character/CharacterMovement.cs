using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    private Rigidbody2D rigid;
    private Animator anim;

    public float x = 0;
    public float y = 0;
    public float speed = 5f;
    public string areaTransitionName;

    public bool canMove = true;
    private bool stop;

    void Start() {

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

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

        anim.SetFloat("moveX", rigid.velocity.x);
        anim.SetFloat("moveY", rigid.velocity.y);

        if (x == 1 || x == -1 || y == 1 || y == -1) {
            anim.SetFloat("lastMoveX", x);
            anim.SetFloat("lastMoveY", y);
        } 

    }
}
