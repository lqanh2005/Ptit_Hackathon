using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Transform originalParent;
    private Rigidbody2D rb;
    private Image image;
    public TrashType type;

    public RectTransform bin;

    [HideInInspector]
    public Transform parentAfterDrag;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void Init()
    {
        switch (type)
        {
            case TrashType.Paper:
                bin = GameplayController.instance.uICtrl.scrollView.bins[0];
                break;
            case TrashType.Plastic:
                bin = GameplayController.instance.uICtrl.scrollView.bins[1];
                break;
            case TrashType.Can:
                bin = GameplayController.instance.uICtrl.scrollView.bins[2];
                break;
            case TrashType.Glass:
                bin = GameplayController.instance.uICtrl.scrollView.bins[3];
                break;
            default:
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.position;
        originalParent = transform.parent;
        parentAfterDrag = originalParent;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();

        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
        SwipeThrowController.instance.UpdateSwipeData(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        
        rectTransform.DOAnchorPos(bin.anchoredPosition, 0.3f)
         .SetEase(Ease.InOutQuad)
         .OnComplete(() =>
         {
             GameplayController.instance.uICtrl.scrollView.draggableItems.Remove(this);
             Destroy(image.gameObject);
             GameplayController.instance.uICtrl.scrollView.Show();
             
         });
    }
}
