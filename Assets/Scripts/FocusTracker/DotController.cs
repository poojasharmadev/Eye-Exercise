using UnityEngine;
using UnityEngine.UI;

public class DotController : MonoBehaviour
{
    public Image dotImage;
    public GameObject donePanel;
    public float moveSpeed = 200f;
    public float shrinkAmount = 40f;

    public DonePanelController donePanelController;

    private Vector2 topLeft, topRight, bottomRight, bottomLeft;
    private Vector2 currentTarget;
    private int state = 0;           // Tracks which corner to move to
    private int loopCount = 0;       // Counts completed loops
    private bool done = false;

    public bool isRed = false;

    public float maxLoops = 6.5f;         // Number of loops before donePanel shows

 void Start()
{
    // Save initial spawn position
    startPosition = transform.GetComponent<RectTransform>().anchoredPosition;

    InitCorners();
    currentTarget = topLeft;

    InvokeRepeating(nameof(ChangeColor), 1f, 1.2f);
}


    void Update()
    {
        if (done) return;

        // Move dot toward current target
        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, currentTarget, moveSpeed * Time.deltaTime);

        // Check if reached the corner
        if (Vector2.Distance(rect.anchoredPosition, currentTarget) < 1f)
        {
            NextCorner();
        }
    }

 void InitCorners()
{
    RectTransform parentRect = transform.parent.GetComponent<RectTransform>();
    Vector2 size = parentRect.rect.size;
    float halfX = size.x / 1.3f;
    float halfY = size.y / 1.3f;

    // Move more towards center
    float marginX = halfX * 0.5f; // 50% from left/right edges
    float marginY = halfY * 0.5f; // 50% from top/bottom edges

    topLeft = new Vector2(-halfX + marginX, halfY - marginY);
    topRight = new Vector2(halfX - marginX, halfY - marginY);
    bottomRight = new Vector2(halfX - marginX, -halfY + marginY);
    bottomLeft = new Vector2(-halfX + marginX, -halfY + marginY);
}



   private Vector2 startPosition;

public void ResetDot()
{
    // Reset all states
    done = false;
    loopCount = 0;
    state = 0;
    isRed = false;

    // Reset dot color
    dotImage.color = Color.white;

    // Reset movement variables
    transform.GetComponent<RectTransform>().anchoredPosition = startPosition;

    // Recalculate the square corners
    InitCorners();
    currentTarget = topLeft;

    // Restart color changing
    CancelInvoke(nameof(ChangeColor));
    InvokeRepeating(nameof(ChangeColor), 1f, 1.2f);

    // Hide done panel
    if (donePanel != null)
        donePanel.SetActive(false);
}


    void NextCorner()
    {
        state++;
        switch (state)
        {
            case 1: currentTarget = topRight; break;
            case 2: currentTarget = bottomRight; break;
            case 3: currentTarget = bottomLeft; break;
            case 4:
                currentTarget = topLeft;
                state = 0;

                loopCount++;  // Completed one loop

                // Shrink the square a little after each loop
                ShrinkSquare();

                // Check if reached max loops
                if (loopCount >= maxLoops)
                {
                    done = true;
                    ShowDonePanel();
                }
                break;
        }
    }

    void ShrinkSquare()
    {
        topLeft += new Vector2(shrinkAmount, -shrinkAmount);
        topRight += new Vector2(-shrinkAmount, -shrinkAmount);
        bottomRight += new Vector2(-shrinkAmount, shrinkAmount);
        bottomLeft += new Vector2(shrinkAmount, shrinkAmount);
    }

     void ShowDonePanel()
    {
        if (donePanel != null)
        {
            donePanel.SetActive(true);  // Activate panel
            DonePanelController controller = donePanel.GetComponent<DonePanelController>();
            if (controller != null)
                controller.ShowPanel();  // Animate scale in
        }
    }


    void ChangeColor()
    {
        if (done) return;

        int random = Random.Range(0, 3);
        if (random == 0)
        {
            dotImage.color = Color.yellow;
            isRed = true;
        }
        else
        {
            dotImage.color = Color.white;
            isRed = false;
        }
    }
}
