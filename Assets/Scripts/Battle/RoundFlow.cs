using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundFlow : MonoBehaviour
{
    [Header("Battle Screen")]
    [Header("Left Most Character")]
    [SerializeField] GameObject LeftCharacter;
    [Header("Center Character")]
    [SerializeField] GameObject CenterCharacter;

    [Header("Right Most Character")]
    [SerializeField] GameObject RightCharacter;
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

    public void InitiateRound(List<BattleCharacter> roundBank, int roundIndex, GameObject left, GameObject center, GameObject right)
    {
        Debug.Log(roundBank.Count);
        GameObject temp = Instantiate(roundBank[0].cardPrefab, left.transform.position , left.transform.rotation);
        temp.transform.parent = left.transform;
        temp.transform.localScale = new Vector3(1, 1,1);
        temp = Instantiate(roundBank[1].cardPrefab, center.transform.position , center.transform.rotation);
        temp.transform.parent = center.transform;
        temp.transform.localScale = new Vector3(1, 1,1);
        temp = Instantiate(roundBank[2].cardPrefab, right.transform.position , right.transform.rotation);
        temp.transform.parent = right.transform;
        temp.transform.localScale = new Vector3(1, 1,1);
      

    }
}
