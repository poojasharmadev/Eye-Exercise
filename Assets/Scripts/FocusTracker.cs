using UnityEngine;
using TMPro;

public class SmoothFocusTrackerTMP : MonoBehaviour
{
    public RectTransform targetArea;
    public RectTransform targetDot;
    public TMP_Text timerText;

    public float duration = 10f;       // total timer duration
    public float stayTime = 1.5f;      // time before choosing new target
    public float moveSpeed = 3f;       // smooth movement speed

    private float timeLeft;
    private float moveWait;
    private bool running = false;

    private Vector2 currentTarget;

    void OnEnable()
    {
        StartGame();
    }

    public void StartGame()
    {
        timeLeft = duration;
        moveWait = 0;
        running = true;

        // Choose first target position
        ChooseNewTarget();
    }

    void Update()
    {
        if (!running) return;

        // Update timer
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            running = false;
            timerText.text = "Done!";
            return;
        }

        // Smooth movement
        targetDot.anchoredPosition = Vector2.Lerp(
            targetDot.anchoredPosition,
            currentTarget,
            Time.deltaTime * moveSpeed
        );

        // Wait before choosing next point
        moveWait -= Time.deltaTime;
        if (moveWait <= 0)
        {
            ChooseNewTarget();
            moveWait = stayTime;
        }
    }

    void ChooseNewTarget()
    {
        Vector2 areaSize = targetArea.rect.size;
        Vector2 dotSize = targetDot.rect.size;

        float x = Random.Range(-(areaSize.x - dotSize.x) / 2f, (areaSize.x - dotSize.x) / 2f);
        float y = Random.Range(-(areaSize.y - dotSize.y) / 2f, (areaSize.y - dotSize.y) / 2f);

        currentTarget = new Vector2(x, y);
    }
}
