using UnityEngine;
using TMPro;
using System.Collections;

public class DualFocusManager : MonoBehaviour
{
    [Header("Spawn")]
    public RectTransform spawnArea;
    public DualFocusNumber numberPrefab;
    public float spawnInterval = 1.2f;

    [Header("UI")]
    public TMP_Text scoreText;
    public GameObject donePanel;

    [Header("Game")]
    public int winScore = 10;

    private int score;
    private bool gameActive;

    public void OnEnable()
    {
        ResetGame();
        StartCoroutine(SpawnLoop());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnLoop()
    {
        gameActive = true;

        while (gameActive)
        {
            SpawnNumber();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnNumber()
    {
        DualFocusNumber num =
            Instantiate(numberPrefab, spawnArea);

        RectTransform r = num.GetComponent<RectTransform>();

        float x = Random.Range(spawnArea.rect.xMin + 60, spawnArea.rect.xMax - 60);
        float bottomY = spawnArea.rect.yMin - 80;
        float topY = spawnArea.rect.yMax + 80;

        r.anchoredPosition = new Vector2(x, bottomY);

        int value = Random.Range(0, 100);
        num.Init(value, topY, this);
    }

   public void OnNumberClicked(int value)
{
    if (value % 2 == 0)
        score++;
    else
        score--;

    UpdateScoreUI();

    if (score >= winScore)
    {
        gameActive = false;
        StopAllCoroutines();

        // Show Done Panel
        if (donePanel != null)
            donePanel.SetActive(true);
    }
}


   public void ResetGame()
{
    score = 0;
    UpdateScoreUI();

    foreach (Transform child in spawnArea)
        Destroy(child.gameObject);

    // Hide Done Panel
    if (donePanel != null)
        donePanel.SetActive(false);
}


    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
