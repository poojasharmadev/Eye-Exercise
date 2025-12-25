using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameObject donePanel;
    public RectTransform playerRect;

    void Update()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(
                GetComponent<RectTransform>(), 
                playerRect.position))
        {
            donePanel.SetActive(true);
        }
    }
}
