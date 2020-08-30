using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public Animator[] transition;

    public float transitionTime = 1f;

    public int index = 0;

    private CharacterMovement player;

    void Start() {
        player = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }

    public void LoadNextLevel(int nextScene) {
        StartCoroutine(LoadLevel(nextScene));
    }

    IEnumerator LoadLevel(int levelIndex) {

        GameManager.instance.enemyMove = false;

        GameManager.instance.canMove = false;

        transition[index].gameObject.SetActive(true);

        transition[index].SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        transition[index].SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);

        transition[index].gameObject.SetActive(false);

        GameManager.instance.canMove = true;

        GameManager.instance.enemyMove = true;

    }

}
