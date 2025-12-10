using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DualFocusManager : MonoBehaviour
{
    public RectTransform spawnArea;
    public TMP_Text numberText;  // assign in inspector
    public Image numberBox;      // the Image behind the number
    public TMP_Text scoreText;   // assign in inspector
    public float spawnInterval = 2f;

    private int score = 0;
    private RectTransform canvasRect;

    public GameObject completePanel;
    public float panelAnimDuration = 0.5f;
    private CanvasGroup panelCanvasGroup;

    void Start()
    {
        canvasRect = numberText.canvas.GetComponent<RectTransform>();
        UpdateScoreUI();

        // Make panel inactive initially
        completePanel.SetActive(false);
        panelCanvasGroup = completePanel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null)
        {
            panelCanvasGroup = completePanel.AddComponent<CanvasGroup>();
        }
        panelCanvasGroup.alpha = 0f;
        completePanel.transform.localScale = Vector3.zero;

        // Start spawning numbers repeatedly
        InvokeRepeating("SpawnNumber", 1f, spawnInterval);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int currentNumber = int.Parse(numberText.text);

            if (currentNumber % 2 == 0)
                score++;
            else
                score--;

            UpdateScoreUI();

            if (score >= 10) // show panel on score 10
            {
                StartCoroutine(ShowCompletePanel());
            }
        }
    }



void SpawnNumber()
{
    int randomNumber = Random.Range(0, 100);
    numberText.text = randomNumber.ToString();

    // Get half of box size
    float halfWidth = numberBox.rectTransform.rect.width / 2;
    float halfHeight = numberBox.rectTransform.rect.height / 2;

    // Random position inside spawn area
    float x = Random.Range(
        spawnArea.rect.xMin + halfWidth,
        spawnArea.rect.xMax - halfWidth
    );
    float y = Random.Range(
        spawnArea.rect.yMin + halfHeight,
        spawnArea.rect.yMax - halfHeight
    );

    numberBox.rectTransform.anchoredPosition = new Vector2(x, y);

    // Random light color using HSV
    float hue = Random.value;           
    float saturation = Random.Range(0.3f, 0.6f);
    float value = Random.Range(0.8f, 1f);       
    numberBox.color = Color.HSVToRGB(hue, saturation, value);
}


    IEnumerator ShowCompletePanel()
    {
        completePanel.SetActive(true); // Activate before animating
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (elapsed < panelAnimDuration)
        {
            float t = elapsed / panelAnimDuration;
            panelCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            completePanel.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        panelCanvasGroup.alpha = 1f;
        completePanel.transform.localScale = endScale;
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
