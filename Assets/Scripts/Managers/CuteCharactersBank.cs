using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuteCharactersBank : MonoBehaviour
{
    [Header("Characters That Fight")]
    [SerializeField] List<BattleCharacter> battleCharactersBank = new List<BattleCharacter>();
    [SerializeField] int charactersPerRound = 3;


    #region private variables

    public List<BattleCharacter> tempBank = new List<BattleCharacter>();
    int charactersDeployed = 0;

    #region round arrays

    public List<BattleCharacter> round1;
    public List<BattleCharacter> round2;
    public List<BattleCharacter> round3;
    public List<BattleCharacter> round4;

    #endregion

    #endregion
    private static CuteCharactersBank instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Cute Bank in the scene");
        }
        instance = this;
    }

    public static CuteCharactersBank Instance()
    {
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        // InitiateCharactersPerRound();

    }

    public List<BattleCharacter> GetRound(int roundIndex)
    {
        switch (roundIndex)
        {
            case 1:
                return round1;
                break;
            case 2:
                return round2;
                break;
            case 3:
                return round3;
                break;
            case 4:
                return round4;
                break;
            default:
                return round1;
                break;

        }
    }

    public void InitiateCharactersPerRound()
    {
        // Debug.Log("InitiateCharactersPerRound");

        foreach (BattleCharacter character in battleCharactersBank)
        {
            tempBank.Add(character);
        }
        //Round 1 characters
        round1 = new List<BattleCharacter>();
        DealCharacters(round1);
        //Round 2 Characters
        round2 = new List<BattleCharacter>();
        DealCharacters(round2);

        //Round 3 Characters
        round3 = new List<BattleCharacter>();
        DealCharacters(round3);
    }


    private void DealCharacters(List<BattleCharacter> round)
    {
        charactersDeployed = 0;
        while (charactersDeployed < charactersPerRound)
        {
            int totalCharacters = tempBank.Count;
            int i = Random.Range(0, totalCharacters);
            // Debug.Log("i: " + i);
            BattleCharacter tempChara = tempBank[i];
            // Debug.Log("temp chara: " + tempChara);
            if (round.Contains(tempChara) == false)
            {
                round.Add(tempChara);
                tempBank.Remove(tempChara);
                charactersDeployed++;
            }
            // Debug.Log("Dealt: " + round.Count + "Remain: " + tempBank.Count);
        }

    }

    public void AddRoundWinner(int winner, int round)
    {

        BattleCharacter winningCharacter = GetRound(round)[winner];
        round4.Add(winningCharacter);
    }


}
