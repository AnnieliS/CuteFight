using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum RoundPhase { INIT, QUESTION, CHOOSE, POINTS, FINAL };
    public RoundPhase currentRoundPhase = RoundPhase.INIT;
    public int currentRound = 0;
    [Header("Screens")]
    [SerializeField] GameObject splashScreen;
    [SerializeField] GameObject playerSelectScreen;
    [SerializeField] GameObject instructionsScreen;
    [SerializeField] GameObject questionScreen;
    [SerializeField] GameObject characterBattleScreen;
    [SerializeField] GameObject winningCharacterScreen;
    [SerializeField] GameObject nextQuestionScreen;

    [Header("Questions Screen")]
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI questionText;
    [Header("Battle Screen")]
    [SerializeField] TextMeshProUGUI battleQuestionText;

    [Header("Winning Character Screen")]
    [SerializeField] Image winningCharacter;
    [SerializeField] Image winningCharacterName;


    List<BattleCharacter> currentRoundCharacters = new List<BattleCharacter>();
    RoundFlow roundFlow;
    int roundWinner = -1;
    private static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Game Manager in the scene");
        }
        instance = this;
    }

    public static GameManager Instance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        roundFlow = gameObject.GetComponent<RoundFlow>();
        splashScreen.SetActive(true);
        InitiateGame();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentRoundPhase != RoundPhase.CHOOSE)
        {
            Debug.Log("clicked");
            Debug.Log("current round " + currentRound);
            Debug.Log("current phase " + currentRoundPhase);
            ScreenControl();
        }
        if (currentRoundPhase == RoundPhase.CHOOSE)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ClickCharacter(0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ClickCharacter(1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ClickCharacter(2);
            }
        }
    }

    private void InitiateGame()
    {
        currentRound = 0;
        CuteCharactersBank.Instance().InitiateCharactersPerRound();
        QuestionsBank.Instance().ChooseQuestionForRounds();
    }

    private void ScreenControl()
    {
        switch (currentRoundPhase)
        {
            case RoundPhase.INIT:
                InitiateRound();
                currentRoundPhase = RoundPhase.QUESTION;
                break;
            case RoundPhase.QUESTION:
                QuestionToCharacter();
                break;
            case RoundPhase.CHOOSE:
                //currently in other function//
                break;
            case RoundPhase.POINTS:
                ChooseRoundWinner(roundWinner, currentRound);
                break;
            case RoundPhase.FINAL:
                TempRound4();
                break;




        }
    }

    private void InitiateRound()
    {
        currentRound++;
        foreach (BattleCharacter character in CuteCharactersBank.Instance().GetRound(currentRound))
        {
            currentRoundCharacters.Add(character);
        }
        roundText.text = "ROUND " + currentRound.ToString();
        questionText.text = QuestionsBank.Instance().GetCurrentRoundQuestion(currentRound);
        battleQuestionText.text = QuestionsBank.Instance().GetCurrentRoundQuestion(currentRound);
        roundFlow.InitiateRound(currentRoundCharacters, currentRound);
        splashScreen.SetActive(false);
        nextQuestionScreen.SetActive(false);
        questionScreen.SetActive(true);
    }

    private void QuestionToCharacter()
    {
        StartCoroutine(MoveScreensQues2Choose());
        Debug.Log("question To Character screen");
    }

    IEnumerator MoveScreensQues2Choose()
    {
        yield return new WaitForSeconds(0.5f);
        currentRoundPhase = RoundPhase.CHOOSE;
        questionScreen.SetActive(false);
        characterBattleScreen.SetActive(true);

    }

    private void ClickCharacter(int characterClicked)
    {
        roundWinner = characterClicked;
        winningCharacter.sprite = currentRoundCharacters[characterClicked].characterSprite;
        winningCharacterName.sprite = currentRoundCharacters[characterClicked].nameSprite;
        //add if for all players choose or timer
        winningCharacterScreen.SetActive(true);
        currentRoundPhase = RoundPhase.POINTS;
    }

    private void ChooseRoundWinner(int winner, int currentRound)
    {
        CuteCharactersBank.Instance().AddRoundWinner(winner, currentRound);

        currentRoundCharacters.Clear();
        winningCharacterScreen.SetActive(false);
        currentRoundPhase = RoundPhase.INIT;
        nextQuestionScreen.SetActive(true);
        if (currentRound == 3)
        {
            currentRoundPhase = RoundPhase.FINAL;
        }

    }

    private void TempRound4()
    {
        battleQuestionText.text = "ROUND 4 PARTICIPANTS";
        foreach (BattleCharacter character in CuteCharactersBank.Instance().GetRound(4))
        {
            currentRoundCharacters.Add(character);
        }
        roundFlow.InitiateRound(currentRoundCharacters, 4);
        nextQuestionScreen.SetActive(false);
    }


}
