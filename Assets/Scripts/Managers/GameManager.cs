using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private void Awake() {
         if (instance != null)
        {
            Debug.LogWarning("Found more than one Game Manager in the scene");
        }
        instance = this;
    }

    public static GameManager Instance(){
        return instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
