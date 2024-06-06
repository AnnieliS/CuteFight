using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundFlow : MonoBehaviour
{
    [Header("Battle Screen")]
    [Header("Left Most Character")]
    [SerializeField] Image characterSprite1;
    [SerializeField] Image nameSprite1;
    [Header("Center Character")]
    [SerializeField] Image characterSprite2;
    [SerializeField] Image nameSprite2;

    [Header("Right Most Character")]
    [SerializeField] Image characterSprite3;
    [SerializeField] Image nameSprite3;

    // Start is called before the first frame update
    void Start()
    {
        // sprites = GameObject.FindGameObjectsWithTag(spriteTag);
        // names = GameObject.FindGameObjectsWithTag(nameTag);
        // StartCoroutine(StartRound());

    }

    // IEnumerator StartRound()
    // {
    //     // yield return new WaitForSeconds(0.01f);
    //     // InitiateRound(CuteCharactersBank.Instance().GetRound(1), 1);
    // }

    public void InitiateRound(List<BattleCharacter> roundBank, int roundIndex)
    {
        Debug.Log(roundBank.Count);
        characterSprite1.sprite = roundBank[0].characterSprite;
        characterSprite2.sprite = roundBank[1].characterSprite;
        characterSprite3.sprite = roundBank[2].characterSprite;

        nameSprite1.sprite = roundBank[0].nameSprite;
        nameSprite2.sprite = roundBank[1].nameSprite;
        nameSprite3.sprite = roundBank[2].nameSprite;

    }
}
