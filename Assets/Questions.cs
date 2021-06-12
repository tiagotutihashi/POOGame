using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour {

    public string[] questions;
    public string[] answersOne;
    public string[] answersTwo;
    public string[] answersThree;
    public string[] answersFour;
    public int[] correctAnswer;

    public Text question;
    public Text number;
    public Text[] buttons;
    public Text results;
    public Text total;
    public Button continueButton;
    public GameObject resultScreen;

    public List<int> selectedAnswer = new List<int>();
    public int indexQuestion = 0;
    public int points = 0;

    public void StartQuestions() {
        indexQuestion = 0;
        number.text = "1";
        question.text = questions[indexQuestion];
        buttons[0].text = answersOne[indexQuestion];
        buttons[1].text = answersTwo[indexQuestion];
        buttons[2].text = answersThree[indexQuestion];
        buttons[3].text = answersFour[indexQuestion];
    }

    private void Start() {
        StartQuestions();
    }

    public void ContiueAction() {

        resultScreen.SetActive(false);

    }

    public void setAnswer(int value) {
        selectedAnswer.Add(value);
        indexQuestion++;
        if (questions.Length - 1 >= indexQuestion) {
            number.text = (indexQuestion + 1).ToString();
            question.text = questions[indexQuestion];
            buttons[0].text = answersOne[indexQuestion];
            buttons[1].text = answersTwo[indexQuestion];
            buttons[2].text = answersThree[indexQuestion];
            buttons[3].text = answersFour[indexQuestion];
        } else {
            for (int f = 0; f < selectedAnswer.Count; f++) {
                if (correctAnswer[f] == selectedAnswer[f]) {
                    points++;
                    results.text = results.text + f.ToString() + " - Correto - + 1\n\n";
                } else {
                    results.text = results.text + f.ToString() + " - Errado - 0\n\n";
                }
            }
            total.text = points.ToString() + "pts";
            resultScreen.SetActive(true);
        }
    }
}
