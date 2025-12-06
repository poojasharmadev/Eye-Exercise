using UnityEngine;
using TMPro;

public class FocusTracker : MonoBehaviour
{
    public DotController dot;
    public TextMeshProUGUI scoreText;

    private int score = 0;

    // Button calls this
    public void CheckTap()
    {
        if (dot.isRed)
        {
            score++;
        }
        else
        {
            score--;
        }

        scoreText.text = "Score: " + score;
    }
}
