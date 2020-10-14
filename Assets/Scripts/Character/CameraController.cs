using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour {

    public CharacterMovement player;

    public Tilemap map;
    private Vector3 bottomLeftLimite;
    private Vector3 topRightLimite;

    private float halfHeight;
    private float halfWidth;

    void Start() {

        player = GameObject.Find("Player").GetComponent<CharacterMovement>();

        //Pegando metado do tamanho da câmera
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        //Pegas os limite do tilemap da cena limitando com o tamanho da câmera
        bottomLeftLimite = map.localBounds.min + new Vector3(halfWidth, halfHeight-1.8f,0f);
        topRightLimite = map.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        player.SetBounds(map.localBounds.min, map.localBounds.max);

        GameManager.instance.terminarManager.AddObj("CameraController");

    }

    void LateUpdate() {

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimite.x, topRightLimite.x), Mathf.Clamp(transform.position.y, bottomLeftLimite.y, topRightLimite.y), transform.position.z);

    }
}
