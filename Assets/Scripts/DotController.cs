using UnityEngine;
using UnityEngine.UI;

public class DotController : MonoBehaviour
{
    public float moveSpeed = 150f;
    public Image dotImage;
    public RectTransform moveArea;

    private RectTransform rect;
    private Vector2 targetPos;

    public bool isRed = false;

    private float minTravelTime = 1f;   // <--- must travel at least 1 second
    private float travelTimer = 0f;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        ChooseNewTarget();
        InvokeRepeating(nameof(ChangeColor), 1f, 1.2f);
    }

    void Update()
    {
        travelTimer += Time.deltaTime;

        rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);

        // only choose new target AFTER it has moved for 1 sec
        if (travelTimer >= minTravelTime &&
            Vector2.Distance(rect.anchoredPosition, targetPos) < 10f)
        {
            ChooseNewTarget();
            travelTimer = 0f;
        }
    }

    void ChooseNewTarget()
    {
        Vector2 areaSize = moveArea.rect.size;

        float limitX = (areaSize.x / 2) - 40;
        float limitY = (areaSize.y / 2) - 40;

        targetPos = new Vector2(
            Random.Range(-limitX, limitX),
            Random.Range(-limitY, limitY)
        );
    }

    void ChangeColor()
    {
        int random = Random.Range(0, 4);

        if (random == 0)
        {
            dotImage.color = Color.red;
            isRed = true;
        }
        else
        {
            dotImage.color = Color.white;
            isRed = false;
        }
    }
}
