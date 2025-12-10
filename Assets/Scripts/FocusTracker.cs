using UnityEngine;
using TMPro;

public class FocusTracker : MonoBehaviour
{
    public DotController dot;           // Reference to the moving dot
    public TextMeshProUGUI scoreText;   // UI text to display score

    private int score = 0;


    void Start()
    {
        UpdateScoreUI(); // Show initial score
    }

public void ResetGame()
{
    score = 0;
    UpdateScoreUI();

    dot.ResetDot();
}



    // Call this from a UI button when player taps
    public void CheckTap()
    {
        if (dot.isRed)   // Dot is yellow
        {
            score++;
        }
        else            // Dot is white
        {
            score--;
        }

        UpdateScoreUI();
    }

    // Updates the score text
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
