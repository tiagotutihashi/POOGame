using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public Animator[] transition;

    public float transitionTime = 1f;

    public int index = 0;

    public CharacterMovement player;

    public void LoadNextLevel(int nextScene) {
        StartCoroutine(LoadLevel(nextScene));
        GameManager.instance.terminarManager.AddMethod("LevelLoader.LoadNextLevel()");
    }

    public void LoadFromLoad(int nextScene) {
        StartCoroutine(LoadLevelLoad(nextScene));
        GameManager.instance.terminarManager.AddMethod("LevelLoader.LoadFromLoad()");
    }

    public void LoadFirst(int nextScene, List<string> newDialog, List<int> howSpeaks, List<Sprite> images) {
        StartCoroutine(LoadFirstLevel(nextScene, newDialog, howSpeaks, images));
        GameManager.instance.terminarManager.AddMethod("LevelLoader.LoadFirst()");
    }

    public void LoadFromMainMenu() {
        StartCoroutine(LoadLevelMainMenu());
        GameManager.instance.terminarManager.AddMethod("LevelLoader.LoadFromMainMenu()");
    }

    public void LoadToMainMenu() {
        StartCoroutine(LoadLevelToMainMenu());
        GameManager.instance.terminarManager.AddMethod("LevelLoader.LoadToMainMenu()");
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

    IEnumerator LoadLevelLoad(int levelIndex) {

        GameManager.instance.enemyMove = false;

        GameManager.instance.canMove = false;

        transition[index].gameObject.SetActive(true);

        transition[index].SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        transition[index].SetTrigger("End");

        player.gameObject.SetActive(true);

        player.gameObject.transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(transitionTime);

        transition[index].gameObject.SetActive(false);

        GameManager.instance.canMove = true;

        GameManager.instance.enemyMove = true;

    }

    IEnumerator LoadFirstLevel(int levelIndex, List<string> newDialog, List<int> howSpeaks, List<Sprite> images) {

        GameManager.instance.enemyMove = false;

        GameManager.instance.canMove = false;

        transition[index].gameObject.SetActive(true);

        transition[index].SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        transition[index].SetTrigger("End");

        player.gameObject.SetActive(true);

        player.gameObject.transform.position = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(transitionTime);

        transition[index].gameObject.SetActive(false);

        GameManager.instance.canMove = true;

        GameManager.instance.enemyMove = true;

        GameManager.instance.ActivateTopButtons();

        GameManager.instance.dialogManager.StartDialog(newDialog, howSpeaks, images);

    }

    IEnumerator LoadLevelMainMenu() {
        GameManager.instance.LoadGame();

        GameManager.instance.enemyMove = false;

        GameManager.instance.canMove = false;

        transition[index].gameObject.SetActive(true);

        transition[index].SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(PlayerPrefs.GetInt("Current_Scene"));

        transition[index].SetTrigger("End");

        player.gameObject.SetActive(true);

        player.gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        yield return new WaitForSeconds(transitionTime);

        transition[index].gameObject.SetActive(false);

        GameManager.instance.canMove = true;

        GameManager.instance.enemyMove = true;

        GameManager.instance.ActivateTopButtons();

    }

    IEnumerator LoadLevelToMainMenu() {

        GameManager.instance.enemyMove = false;

        GameManager.instance.canMove = false;

        GameManager.instance.DeactivateTopButtons();

        transition[index].gameObject.SetActive(true);

        transition[index].SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(0);

        transition[index].SetTrigger("End");

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(transitionTime);

        transition[index].gameObject.SetActive(false);

    }

}
