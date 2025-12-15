using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DualFocusManager : MonoBehaviour
{
    [Header("UI")]
    public RectTransform spawnArea;
    public TMP_Text numberText;
    public Image numberBox;
    public TMP_Text scoreText;

    [Header("Settings")]
    public float spawnInterval = 2f;
    public int winScore = 10;

    [Header("Complete Panel")]
    public GameObject completePanel;
    public float panelAnimDuration = 0.5f;

    private int score;
    private CanvasGroup panelCanvasGroup;
    private bool gameActive = false;

    // ------------------------
    // UNITY LIFECYCLE
    // ------------------------
    void Awake()
    {
        panelCanvasGroup = completePanel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
            panelCanvasGroup = completePanel.AddComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        ResetGame();
        StartGame();
    }

    void OnDisable()
    {
        StopGame();
    }

    void Update()
    {
        if (!gameActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            int num = int.Parse(numberText.text);

            score += (num % 2 == 0) ? 1 : -1;
            UpdateScoreUI();

            if (score >= winScore)
            {
                gameActive = false;
                StopAllCoroutines();
                CancelInvoke(nameof(SpawnNumber));
                StartCoroutine(ShowCompletePanel());
            }
        }
    }

    // ------------------------
    // GAME FLOW
    // ------------------------
    void StartGame()
    {
        gameActive = true;
        InvokeRepeating(nameof(SpawnNumber), 1f, spawnInterval);
    }

    void StopGame()
    {
        gameActive = false;
        CancelInvoke(nameof(SpawnNumber));
        StopAllCoroutines();
    }

    // ------------------------
    // RESET
    // ------------------------
    public void ResetGame()
    {
        score = 0;
        UpdateScoreUI();

        numberText.text = "0";
        numberBox.color = Color.white;

        completePanel.SetActive(false);
        panelCanvasGroup.alpha = 0f;
        completePanel.transform.localScale = Vector3.zero;
    }

    // ------------------------
    // GAME LOGIC
    // ------------------------
    void SpawnNumber()
    {
        int randomNumber = Random.Range(0, 100);
        numberText.text = randomNumber.ToString();

        float halfW = numberBox.rectTransform.rect.width / 2;
        float halfH = numberBox.rectTransform.rect.height / 2;

        float x = Random.Range(spawnArea.rect.xMin + halfW, spawnArea.rect.xMax - halfW);
        float y = Random.Range(spawnArea.rect.yMin + halfH, spawnArea.rect.yMax - halfH);

        numberBox.rectTransform.anchoredPosition = new Vector2(x, y);

        numberBox.color = Color.HSVToRGB(
            Random.value,
            Random.Range(0.3f, 0.6f),
            Random.Range(0.8f, 1f)
        );
    }

    IEnumerator ShowCompletePanel()
    {
        completePanel.SetActive(true);

        float t = 0f;
        while (t < panelAnimDuration)
        {
            t += Time.deltaTime;
            float p = t / panelAnimDuration;

            panelCanvasGroup.alpha = Mathf.Lerp(0, 1, p);
            completePanel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, p);
            yield return null;
        }

        panelCanvasGroup.alpha = 1;
        completePanel.transform.localScale = Vector3.one;
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}   