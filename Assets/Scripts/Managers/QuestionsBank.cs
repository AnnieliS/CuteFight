using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsBank : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] List<string> questionsBank = new List<string>();
    [SerializeField] int NumOfRounds = 3;
    public List<string> questionInBattle = new List<string>();
    public List<string> tempQuestionBank = new List<string>();
    private static QuestionsBank instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Questions Manager in the scene");
        }
        instance = this;
    }

    public static QuestionsBank Instance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        ChooseQuestionForRounds();
    }

    private void ChooseQuestionForRounds()
    {
        foreach (string s in questionsBank)
        {

            tempQuestionBank.Add(s);
        }
        int questionsDealt = 0;
        while (questionsDealt < NumOfRounds)
        {
            int totalQuetions = tempQuestionBank.Count;
            int i = Random.Range(0, totalQuetions);
            string tempQuestion = tempQuestionBank[i];
            if (questionInBattle.Contains(tempQuestion) == false)
            {
                questionInBattle.Add(tempQuestion);
                tempQuestionBank.Remove(tempQuestion);
                questionsDealt++;
            }
        }
    }
}


//  charactersDeployed = 0;
//         while (charactersDeployed < charactersPerRound)
//         {
//             int totalCharacters = tempBank.Count;
//             int i = Random.Range(0, totalCharacters);
//             // Debug.Log("i: " + i);
//             BattleCharacter tempChara = battleCharactersBank[i];
//             // Debug.Log("temp chara: " + tempChara);
//             if (round.Contains(tempChara) == false)
//             {
//                 round.Add(tempChara);
//                 tempBank.Remove(tempChara);
//                 charactersDeployed++;
//             }
//             // Debug.Log("Dealt: " + round.Count + "Remain: " + tempBank.Count);
//         }