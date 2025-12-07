using UnityEngine;

public class DonePanelController : MonoBehaviour
{
    public float animationSpeed = 5f;
    private Vector3 targetScale = Vector3.one;
    private bool show = false;

    void Start()
    {
        transform.localScale = Vector3.zero; // start hidden
    }


    void Update()
    {
        if (show)
        {
            // Smoothly scale up
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);
        }
    }

    public void ShowPanel()
    {
        show = true;
    }
}
