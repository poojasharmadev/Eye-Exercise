using UnityEngine;

public class MoveDot : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    void Start()
    {
        // Start moving in a random direction
        direction = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        // Move the dot
        transform.Translate(direction * speed * Time.deltaTime);

        // Get camera bounds
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 pos = transform.position;

        // Check X boundaries
        if (pos.x < min.x || pos.x > max.x)
        {
            direction.x = -direction.x; // bounce back
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        }

        // Check Y boundaries
        if (pos.y < min.y || pos.y > max.y)
        {
            direction.y = -direction.y; // bounce back
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        }

        transform.position = pos;
    }
}
