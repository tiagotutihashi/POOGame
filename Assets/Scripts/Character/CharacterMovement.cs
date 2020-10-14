using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    private Rigidbody2D rigid;
    private Animator anim;

    public float x = 0;
    public float y = 0;
    public float speed = 5f;
    public string areaTransitionName;

    private Vector3 bottomLeftLimite;
    private Vector3 topRightLimite;

    void Start() {

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameManager.instance.terminarManager.AddObj("CharacterMovement");

    }

    void Update() {

        if (GameManager.instance.canMove) {
            Movement(false);
        } else if (GameManager.instance.battleManager.battling) {
            rigid.velocity = Vector2.zero;
        } else {
            Movement(true);
        }

    }

    private void Movement(bool isLoading) {

        if (!isLoading) {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }

        if (x != 0 || y != 0) {
            if(!GameManager.instance.terminarManager.methodsLines.LastOrDefault().Equals("CharacterMovement.Movement()"))
                GameManager.instance.terminarManager.AddMethod("CharacterMovement.Movement()");
        }

        rigid.velocity = new Vector2(x, y) * speed;

        anim.SetFloat("moveX", rigid.velocity.x);
        anim.SetFloat("moveY", rigid.velocity.y);

        if (x == 1 || x == -1 || y == 1 || y == -1) {
            anim.SetFloat("lastMoveX", x);
            anim.SetFloat("lastMoveY", y);
        }

        if (!isLoading) {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimite.x, topRightLimite.x), Mathf.Clamp(transform.position.y, bottomLeftLimite.y, topRightLimite.y), transform.position.z);
        }

    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight) {

        bottomLeftLimite = botLeft + new Vector3(.5f, .5f, 0f);
        topRightLimite = topRight + new Vector3(-.5f, -.5f, 0f);

    }

}
