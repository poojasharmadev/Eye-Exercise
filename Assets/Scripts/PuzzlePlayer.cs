using UnityEngine;

public class PuzzlePlayer : MonoBehaviour
{
    public float moveSpeed = 200f;

    private Vector2 moveDir = Vector2.zero;
    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (moveDir != Vector2.zero)
        {
            rect.anchoredPosition += moveDir * moveSpeed * Time.deltaTime;
        }
    }

    // Called from buttons
    public void MoveUp()    { moveDir = Vector2.up; }
    public void MoveDown()  { moveDir = Vector2.down; }
    public void MoveLeft()  { moveDir = Vector2.left; }
    public void MoveRight() { moveDir = Vector2.right; }

    // Called on Pointer Up
    public void StopMove()
    {
        moveDir = Vector2.zero;
    }
}
