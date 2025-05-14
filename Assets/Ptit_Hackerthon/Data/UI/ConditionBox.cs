using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionBox : MonoBehaviour
{
    [Header("Timer UI")]
    public Image fillBar;
    public List<Image> images;
    public float totalTime = 45f;
    private float timeRemaining;

    [Header("Progress UI")]
    public TMP_Text collected;
    public int totalItemGoal = 6;
    private int currentItemCollected = 0;

    public void Init()
    {

        timeRemaining = totalTime;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        float percent = Mathf.Clamp01(timeRemaining / totalTime);
        fillBar.fillAmount = percent;

        if (percent > 0.66f)
        {
            fillBar.color = Color.green;
            
        }
        else if (percent > 0.33f)
        {
            fillBar.color = Color.yellow;
            images[0].gameObject.SetActive(false);
        }
        else
        {
            fillBar.color = Color.red;
            images[1].gameObject.SetActive(false);
        }
    }
    public void CollectItem(TrashData item)
    {
        GameplayController.instance.uICtrl.scrollView.trashSlots.Add(item);
        currentItemCollected++;
        collected.text = $"{currentItemCollected} / {totalItemGoal}";
        if(currentItemCollected >= totalItemGoal)
        {
            GameplayController.instance.uICtrl.gameCore.SetActive(false);
            GameplayController.instance.uICtrl.minigame.SetActive(true);

            GameplayController.instance.uICtrl.scrollView.Show();
            GameplayController.instance.uICtrl.scroll.offsetMin = new Vector2(80f, -800);
            GameplayController.instance.uICtrl.scroll.offsetMax = new Vector2(-80f, -1200);
        }
    }
}
