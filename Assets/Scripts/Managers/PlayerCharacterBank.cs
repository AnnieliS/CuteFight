using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterBank : MonoBehaviour
{
      [Header("Characters For Players")]
    [SerializeField] Sprite[] playerCharactersBank;
    private static PlayerCharacterBank instance;

    private void Awake() {
         if (instance != null)
        {
            Debug.LogWarning("Found more than one Players Bank in the scene");
        }
        instance = this;
    }

    public static PlayerCharacterBank Instance(){
        return instance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
