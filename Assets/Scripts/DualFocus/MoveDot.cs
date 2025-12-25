using UnityEngine;

public class MoveDot : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 direction;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        ResetDot();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);

        Vector2 pos = transform.position;

        if (pos.x < min.x || pos.x > max.x)
        {
            direction.x *= -1;
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        }

        if (pos.y < min.y || pos.y > max.y)
        {
            direction.y *= -1;
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        }

        transform.position = pos;
    }

    // 🔁 RESET DOT
    public void ResetDot()
    {
        transform.position = startPosition;
        direction = Random.insideUnitCircle.normalized;
    }
}
