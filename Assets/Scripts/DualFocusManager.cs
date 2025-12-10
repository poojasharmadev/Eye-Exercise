using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DualFocusManager : MonoBehaviour
{
    public TMP_Text numberText; // assign in inspector
    public float spawnInterval = 2f;
    private int score = 0;
    private RectTransform canvasRect;

    void Start()
    {
        // Get the canvas RectTransform
        canvasRect = numberText.canvas.GetComponent<RectTransform>();
        InvokeRepeating("SpawnNumber", 1f, spawnInterval);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // or touch input
        {
            int currentNumber = int.Parse(numberText.text);
            if (currentNumber % 2 == 0)
            {
                score++;
                Debug.Log("Correct! Score: " + score);
            }
            else
            {
                Debug.Log("Wrong!");
            }
        }
    }

    void SpawnNumber()
    {
        int randomNumber = Random.Range(0, 100); // 0 to 99
        numberText.text = randomNumber.ToString();

        // Random position inside canvas
        float x = Random.Range(0, canvasRect.rect.width) - canvasRect.rect.width / 2;
        float y = Random.Range(0, canvasRect.rect.height) - canvasRect.rect.height / 2;
        numberText.rectTransform.anchoredPosition = new Vector2(x, y);
    }
}
