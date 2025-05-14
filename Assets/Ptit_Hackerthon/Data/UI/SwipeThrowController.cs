using UnityEngine;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;

public class SwipeThrowController : MonoBehaviour
{
    public static SwipeThrowController instance;

    public Transform spawnPosition;
    public float throwForceMultiplier = 0.1f;
    public float minSwipeVelocity = 0.2f;

    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;
    private float swipeTime;
    private bool isTracking = false;
    private List<Vector2> positionHistory = new List<Vector2>();
    private List<float> timeHistory = new List<float>();

    public GameObject imagePrefab;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateSwipeData(Vector2 currentPosition)
    {
        if (!isTracking)
        {
            swipeStartPosition = currentPosition;
            isTracking = true;
            swipeTime = Time.time;

            positionHistory.Clear();
            timeHistory.Clear();
        }
        positionHistory.Add(currentPosition);
        timeHistory.Add(Time.time);
        if (positionHistory.Count > 10)
        {
            positionHistory.RemoveAt(0);
            timeHistory.RemoveAt(0);
        }

        swipeEndPosition = currentPosition;
    }

    public bool TryThrowItem(GameObject uiItem)
    {
        if (!isTracking) return false;

        isTracking = false;
        Vector2 swipeVelocity = CalculateSwipeVelocity();
        Debug.LogError(" swipeVelocity " + swipeVelocity);
        float swipeMagnitude = swipeVelocity.magnitude;
        if (swipeMagnitude >= minSwipeVelocity)
        {
            
            Rigidbody2D rb = uiItem.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1f;
                Vector3 worldDir = Camera.main.ScreenToWorldPoint(new Vector3(swipeVelocity.x, swipeVelocity.y, 0))
                     - Camera.main.ScreenToWorldPoint(Vector3.zero);

                rb.velocity = Vector2.zero;
                rb.AddForce(worldDir * throwForceMultiplier, ForceMode2D.Impulse);
            }
            return true;
        }

        return false;
    }

    private Vector2 CalculateSwipeVelocity()
    {
        if (positionHistory.Count < 2)
            return Vector2.zero;
        Vector2 velocity = Vector2.zero;
        int startIdx = Mathf.Max(0, positionHistory.Count - 3);

        for (int i = startIdx; i < positionHistory.Count - 1; i++)
        {
            Vector2 displacement = positionHistory[i + 1] - positionHistory[i];
            float deltaTime = timeHistory[i + 1] - timeHistory[i];

            if (deltaTime > 0)
                velocity += displacement / deltaTime;
        }
        velocity /= (positionHistory.Count - startIdx - 1);
        return velocity;
    }
}
