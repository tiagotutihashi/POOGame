using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConceptButton : MonoBehaviour {

    public HelpMenu helpMenu;

    public Sprite conceptImage;
    public string conceptText;
    public string conceptTitle;

    public void SetConcept() {

        helpMenu.conceptTitle.text = conceptTitle;
        helpMenu.conceptText.text = conceptText;
        helpMenu.conceptImage.sprite = conceptImage;
        helpMenu.conceptPanel.SetActive(true);

    }

    void Start() {

        Button btn = GetComponent<Button>();
        btn.GetComponent<Button>().onClick.AddListener(() => SetConcept());

    }

}
