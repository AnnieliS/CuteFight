using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BattleCharacter", menuName = "BattleCharacter", order = 0)]
public class BattleCharacter : ScriptableObject {
  public GameObject cardPrefab;
  public string characterName;  
  public Sprite characterSprite;
  public Sprite nameSprite;
    
}
