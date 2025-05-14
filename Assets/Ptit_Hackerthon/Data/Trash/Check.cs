using UnityEngine;
using System.Collections;
using DG;
using DG.Tweening;

public class Check : MonoBehaviour
{  
    [SerializeField] private Transform inventoryUITransform;
    [SerializeField] private Vector3 offsetToScreen = Vector3.zero;
    [SerializeField] private float flyDuration = 2f;
    private TrashBase trashBase;

    private bool collected = false;
    private float flyTimer = 0f;
    private Vector3 startWorldPos;
    private Vector3 targetWorldPos;

    private void Start()
    {
        trashBase = GetComponentInParent<TrashBase>();
        inventoryUITransform = GameplayController.instance.uICtrl.inventoryBtn.transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (collected || !other.CompareTag("Player")) return;

        collected = true;
        flyTimer = 0f;
        startWorldPos = transform.position;
        startWorldPos.y = 0.5f;
        
    }

    private void Update()
    {
        if (!collected) return;
        Vector3 screenPos = inventoryUITransform.position + offsetToScreen;
        float objectDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
        targetWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, objectDistance));
        targetWorldPos.y = 0.5f;
        flyTimer += Time.deltaTime;
        float t = flyTimer / flyDuration;
        t = Mathf.Clamp01(t);
        t = t * t * (3f - 2f * t);
        
        transform.position = Vector3.Lerp(startWorldPos, targetWorldPos, t);
        this.transform.DOScale(Vector3.zero, 5f).SetEase(Ease.OutBack);
        float distance = Vector3.Distance(transform.position, targetWorldPos);
        if (distance < 0.1f)
        {
            Destroy(gameObject);
            GameplayController.instance.uICtrl.conditionBox.CollectItem(trashBase.trashData);
        }
    }
}