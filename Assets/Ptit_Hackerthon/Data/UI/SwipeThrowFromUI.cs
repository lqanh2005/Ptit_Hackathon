using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeThrowFromUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject ballPrefab;               // Prefab ném trong world
    public Canvas canvas;
    public Camera uiCamera;
    public float throwForce = 0.01f;

    private Vector2 startPos;
    private float startTime;

    private RectTransform draggingImage;
    private bool isDragging = false;

    private void Start()
    {
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
        if (uiCamera == null)
        {
            uiCamera = Camera.main;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = eventData.position;
        startTime = Time.time;

        // Clone image từ slot
        draggingImage = Instantiate(transform.GetChild(0), canvas.transform) as RectTransform;
        draggingImage.position = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        draggingImage.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 endPos = eventData.position;
        float endTime = Time.time;
        float duration = endTime - startTime;
        if (duration <= 0.01f) duration = 0.01f;

        Vector2 velocity = (endPos - startPos) / duration;

        // Spawn bóng world ở vị trí chuột
        Vector3 spawnPos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, eventData.position, uiCamera, out spawnPos);
        GameObject ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

        // Tính lực ném
        Vector3 worldVelocity = uiCamera.ScreenToWorldPoint(new Vector3(velocity.x, velocity.y, 0)) -
                                uiCamera.ScreenToWorldPoint(Vector3.zero);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.AddForce(worldVelocity * throwForce, ForceMode2D.Impulse);

        // Dọn image UI
        Destroy(draggingImage.gameObject);
        isDragging = false;
    }
}
