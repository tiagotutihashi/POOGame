using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public Animator[] transition;

    public float transitionTime = 1f;

    public int index = 0;

    /*void Update() {

        if (Input.GetMouseButtonDown(0)) {
            LoadNextLevel();
        }

    }*/

    public void LoadNextLevel() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            StartCoroutine(LoadLevel(1));
        } else {
            StartCoroutine(LoadLevel(0));
        }
    }

    IEnumerator LoadLevel(int levelIndex) {

        transition[index].gameObject.SetActive(true);

        transition[index].SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        transition[index].SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);

        transition[index].gameObject.SetActive(false);

    }

}
