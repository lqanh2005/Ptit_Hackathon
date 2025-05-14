using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public UICtrl uICtrl;
    public CharacterBase characterBase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        uICtrl.Init();
        characterBase.Init();
    }
}

