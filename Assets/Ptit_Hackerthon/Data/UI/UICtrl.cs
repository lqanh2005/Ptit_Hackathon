using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    public Button inventoryBtn;
    public Button pauseBtn;
    public GameObject winpop;

    public InventoryCtrl scrollView;
    [HideInInspector] public RectTransform scroll;
    public SwipeThrowController thrower;
    public GameObject gameCore;
    public GameObject minigame;
    public Transform trashCanTransform;

    public ConditionBox conditionBox;

    private bool isOpenBag = false;

    public void Init()
    {
        scroll = scrollView.GetComponent<RectTransform>();
        conditionBox.Init();
        inventoryBtn.onClick.AddListener(delegate { OpenInventory(); });
    }
    public void OpenInventory()
    {
        isOpenBag = !isOpenBag;
        if (isOpenBag)
        {
            scrollView.Show();
        }
        else
        {
            scrollView.Close();
        }
    }
}
