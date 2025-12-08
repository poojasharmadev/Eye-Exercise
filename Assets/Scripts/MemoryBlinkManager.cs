using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MemoryBlinkDynamic : MonoBehaviour
{
    [Header("Shape Settings")]
    public ShapeCollection shapeCollection;   // ScriptableObject with List<Sprite>
    private List<Sprite> allShapeSprites;

    [Header("Memory Phase")]
    public Transform shapeParent;             // Grid holder
    public GameObject shapePrefab;            // Prefab with Image
    public float showTime = 1f;

    [Header("Selection Phase")]
    public Transform selectionParent;         // Grid for buttons
    public GameObject selectionButtonPrefab;  // Button with Image

    [Header("Result")]
    public GameObject correctPanel;
    public GameObject wrongPanel;

    private List<Sprite> shownSprites = new List<Sprite>(); // 9 shown shapes
    private int correctCount = 0;
    private int selectedCount = 0;

    void Start()
    {
        allShapeSprites = shapeCollection.shapeSprites;

        if (allShapeSprites == null || allShapeSprites.Count == 0)
        {
            Debug.LogError("ShapeCollection is EMPTY! Add sprites in the inspector.");
            return;
        }

        StartCoroutine(StartMemoryPhase());
    }

    IEnumerator StartMemoryPhase()
    {
        shownSprites.Clear();
        correctCount = 0;
        selectedCount = 0;

        // Clear previous shapes
        foreach (Transform child in shapeParent)
            Destroy(child.gameObject);

        int count = 9; // Always 9 shapes

        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, allShapeSprites.Count);

            Sprite selectedSprite = allShapeSprites[rand];
            shownSprites.Add(selectedSprite);

            GameObject s = Instantiate(shapePrefab, shapeParent);
            s.GetComponent<Image>().sprite = selectedSprite;

            // **Fix small size**
            RectTransform rt = s.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(180, 180);
        }

        yield return new WaitForSeconds(showTime);

        // Hide shapes
        foreach (Transform child in shapeParent)
            child.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.3f);

        SetupSelectionPanel();
    }

    void SetupSelectionPanel()
    {
        // Clear previous buttons
        foreach (Transform child in selectionParent)
            Destroy(child.gameObject);

        // Create selection buttons (use ALL sprites)
        foreach (Sprite s in allShapeSprites)
        {
            GameObject btnObj = Instantiate(selectionButtonPrefab, selectionParent);
            btnObj.transform.localScale = Vector3.one;

            Image img = btnObj.transform.GetChild(0).GetComponent<Image>();
            img.sprite = s;

            // Fix small button size
            RectTransform rt = btnObj.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(180, 180);

            Button btn = btnObj.GetComponent<Button>();
            Sprite captured = s;

            btn.onClick.AddListener(() => OnSelectShape(captured, btn));
        }
    }

    void OnSelectShape(Sprite selectedSprite, Button btn)
    {
        selectedCount++;

        // Disable the button after selection
        btn.interactable = false;

        if (shownSprites.Contains(selectedSprite))
            correctCount++;

        // If player chose 9 shapes → check result
        if (selectedCount == 9)
            ShowResult();
    }

    void ShowResult()
    {
        bool allCorrect = correctCount == 9;

        if (allCorrect)
        {
            correctPanel.SetActive(true);
            wrongPanel.SetActive(false);
        }
        else
        {
            correctPanel.SetActive(false);
            wrongPanel.SetActive(true);
        }
    }
}
