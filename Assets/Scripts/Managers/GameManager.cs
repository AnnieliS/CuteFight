using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum RoundPhase { SPLASH, INIT, STORY, PLAYERSELECT, INSTRUCTIONS, QUESTION, CHOOSE, POINTS, NEXTQUESTION, MINIGAME, FINAL };
    public RoundPhase currentRoundPhase = RoundPhase.INIT;
    private ObjectsHolder items;
    public int currentRound = 0;



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
        items = this.gameObject.GetComponent<ObjectsHolder>();
        roundFlow = gameObject.GetComponent<RoundFlow>();
        items.splashScreen.SetActive(true);
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

    #region Screen control

    private void ScreenControl()
    {
        switch (currentRoundPhase)
        {
            case RoundPhase.SPLASH:
                InitiateRound();
                currentRoundPhase = RoundPhase.INIT;
                break;
            case RoundPhase.INIT:
                StartGame();
                break;
            case RoundPhase.STORY:
                EndStory();
                break;
            case RoundPhase.PLAYERSELECT:
                PlayerSelected();
                break;
            case RoundPhase.INSTRUCTIONS:
                HideInstructions();
                break;
            case RoundPhase.QUESTION:
                QuestionToCharacter();
                break;
            case RoundPhase.CHOOSE:
                //currently in other function//
                break;
            case RoundPhase.POINTS:
                // 
                break;
            case RoundPhase.NEXTQUESTION:
                MoveToNextQuestion();
                break;
            case RoundPhase.MINIGAME:
                break;
            case RoundPhase.FINAL:
                TempRound4();
                break;




        }
    }

    #endregion

    #region Move Between Phases

    private void InitiateRound()
    {
        currentRound++;
        foreach (BattleCharacter character in CuteCharactersBank.Instance().GetRound(currentRound))
        {
            currentRoundCharacters.Add(character);
        }
        items.questionRoundText.text = "ROUND " + currentRound.ToString();
        items.questionText.text = QuestionsBank.Instance().GetCurrentRoundQuestion(currentRound);
        items.battleQuestionText.text = QuestionsBank.Instance().GetCurrentRoundQuestion(currentRound);
        roundFlow.InitiateRound(currentRoundCharacters, currentRound, items.leftCharacterPlace, items.centerCharacterPlace, items.rightCharacterPlace);
        // items.splashScreen.SetActive();
        // nextQuestionScreen.SetActive(false);
        items.battleScreen.SetActive(false);
        items.battleUI.SetActive(false);
        items.questionUI.SetActive(false);
        items.storyScreen.SetActive(false);
        items.playerSelectionScreen.SetActive(false);
        items.instructionsScreen.SetActive(false);
        items.roundWinnerScreen.SetActive(false);
        items.miniGameScreen.SetActive(false);
        items.winningScreen.SetActive(false);
    }

    private void NextRound()
    {
        currentRound++;
        foreach (BattleCharacter character in CuteCharactersBank.Instance().GetRound(currentRound))
        {
            currentRoundCharacters.Add(character);
        }
        items.questionRoundText.text = "ROUND " + currentRound.ToString();
        items.questionText.text = QuestionsBank.Instance().GetCurrentRoundQuestion(currentRound);
        items.battleQuestionText.text = QuestionsBank.Instance().GetCurrentRoundQuestion(currentRound);
        roundFlow.InitiateRound(currentRoundCharacters, currentRound, items.leftCharacterPlace, items.centerCharacterPlace, items.rightCharacterPlace);
    }

    private void StartGame()
    {
        items.splashScreen.SetActive(false);
        items.storyScreen.SetActive(true);
        currentRoundPhase = RoundPhase.STORY;

    }

    private void EndStory()
    {
        items.storyScreen.SetActive(false);
        items.playerSelectionScreen.SetActive(true);
        currentRoundPhase = RoundPhase.PLAYERSELECT;
    }

    private void PlayerSelected()
    {
        items.playerSelectionScreen.SetActive(false);
        items.instructionsScreen.SetActive(true);
        currentRoundPhase = RoundPhase.INSTRUCTIONS;
    }

    private void HideInstructions()
    {
        currentRoundPhase = RoundPhase.QUESTION;
        items.instructionsScreen.SetActive(false);
        items.battleScreen.SetActive(true);
        items.battleUI.SetActive(false);
        items.questionUI.SetActive(true);
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
        items.questionUI.SetActive(false);
        items.battleUI.SetActive(true);
        items.timer.StartTimer();

    }

    private void MoveToNextQuestion()
    {
        NextRound();
        items.roundWinnerScreen.SetActive(false);
        items.battleScreen.SetActive(true);
        items.battleUI.SetActive(false);
        items.questionUI.SetActive(true);
        currentRoundPhase = RoundPhase.QUESTION;
    }

    #endregion

    private void ClickCharacter(int characterClicked)
    {
        roundWinner = characterClicked;
        items.firstPlace.sprite = currentRoundCharacters[characterClicked].characterSprite;
        #region FIX
        //add if for all players choose or timer
        ////////////////////////// FOR NOW ONLY TIMERRRRR//////////////////////
        // items.roundWinnerScreen.SetActive(true);
        #endregion
        currentRoundPhase = RoundPhase.POINTS;
    }

    public void ChooseRoundWinner()
    {
        items.battleScreen.SetActive(false);
        items.roundWinnerScreen.SetActive(true);
        CuteCharactersBank.Instance().AddRoundWinner(roundWinner, currentRound);

        currentRoundCharacters.Clear();
        // items.roundWinnerScreen.SetActive(false);
        currentRoundPhase = RoundPhase.NEXTQUESTION;
        // nextQuestionScreen.SetActive(true);
        if (currentRound == 3)
        {
            currentRoundPhase = RoundPhase.FINAL;
        }

    }

    private void TempRound4()
    {
        items.battleQuestionText.text = "ROUND 4 PARTICIPANTS";
        foreach (BattleCharacter character in CuteCharactersBank.Instance().GetRound(4))
        {
            currentRoundCharacters.Add(character);
        }
        roundFlow.InitiateRound(currentRoundCharacters, 4, items.leftCharacterPlace, items.centerCharacterPlace, items.rightCharacterPlace);
        // nextQuestionScreen.SetActive(false);
    }


}
