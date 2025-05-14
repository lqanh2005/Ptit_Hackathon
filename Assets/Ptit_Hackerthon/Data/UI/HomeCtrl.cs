using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeCtrl : MonoBehaviour
{
    public Button playBtn;
    public Button nextBtn;
    public Button backBtn;

    public GameObject vietnam;
    public GameObject korea;
    public void Start()
    {
        playBtn.onClick.AddListener(delegate { OnPlayBtnClick(); });
        nextBtn.onClick.AddListener(delegate { OnNextBtnClick(); });
        backBtn.onClick.AddListener(delegate { OnBackBtnClick(); });
    }

    private void OnPlayBtnClick()
    {
        Initiate.Fade("GamePlay", Color.black, 2f);
    }
    public void OnNextBtnClick()
    {
        vietnam.SetActive(false);
        korea.SetActive(true);
    }
    public void OnBackBtnClick()
    {
        korea.SetActive(false);
        vietnam.SetActive(true);
    }
}
