using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectsHolder : MonoBehaviour
{
    [Header("Screens")]
    public GameObject splashScreen;
    public GameObject storyScreen;
    public GameObject playerSelectionScreen;
    public GameObject instructionsScreen;
    public GameObject battleScreen;
    public GameObject roundWinnerScreen;
    public GameObject miniGameScreen;
    public GameObject winningScreen;
    // public GameObject creditsScreen;

    [Header("Battle")]
    public Timer timer;
    public GameObject questionUI;
    public TextMeshProUGUI questionRoundText;
    public TextMeshProUGUI questionText;
    public GameObject battleUI;
    public TextMeshProUGUI battleQuestionText;
    public GameObject leftCharacterPlace;
    public GameObject centerCharacterPlace;
    public GameObject rightCharacterPlace;

    [Header("Round Points Screen")]
    public Image firstPlace;
    public Image secondPlace;
    public Image thirdPlace;


}
