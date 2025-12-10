using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MemoryBlinkDynamic : MonoBehaviour
{
    [Header("Shape Settings")]
    public ShapeCollection shapeCollection;
    private List<Sprite> allShapeSprites;

    [Header("Memory Phase")]
    public Transform shapeParent;
    public GameObject shapePrefab;
    public float showTime = 1f;

    [Header("Selection Phase")]
    public Transform selectionParent;
    public GameObject selectionButtonPrefab;

    [Header("Result Prefabs")]
    public GameObject correctPrefab; 
    public GameObject wrongPrefab;   

    [Header("Win/Lose Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    private List<Sprite> memoryShapes = new List<Sprite>(); 
    private int guessCount = 0;
    private int correctCount = 0;

    void Start()
    {
        allShapeSprites = shapeCollection.shapeSprites;

        if (allShapeSprites == null || allShapeSprites.Count < 3)
        {
            Debug.LogError("❌ ShapeCollection must have at least 3 sprites!");
            return;
        }

        StartCoroutine(StartMemoryPhase());
    }

    // -------------------------------
    // MEMORY PHASE
    // -------------------------------
    IEnumerator StartMemoryPhase()
    {
        memoryShapes.Clear();

        // Clear old shapes
        foreach (Transform child in shapeParent)
            Destroy(child.gameObject);

        // Select 3 unique shapes
        List<Sprite> tempShapes = new List<Sprite>(allShapeSprites);
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, tempShapes.Count);
            memoryShapes.Add(tempShapes[index]);
            tempShapes.RemoveAt(index);
        }

        // Duplicate shapes to make 9 shapes in memory panel
        List<Sprite> displayShapes = new List<Sprite>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                displayShapes.Add(memoryShapes[i]);
            }
        }

        // Shuffle the 9 shapes
        displayShapes = displayShapes.OrderBy(a => Random.value).ToList();

        // Instantiate in memory panel
        foreach (Sprite s in displayShapes)
        {
            GameObject obj = Instantiate(shapePrefab, shapeParent);
            obj.GetComponent<Image>().sprite = s;
        }

        yield return new WaitForSeconds(showTime);

        // Hide memory shapes after showing
        foreach (Transform child in shapeParent)
            child.gameObject.SetActive(false);

        // Setup selection panel
        SetupSelectionPanel();
    }

    // -------------------------------
    // SELECTION PANEL
    // -------------------------------
    void SetupSelectionPanel()
    {
        foreach (Transform child in selectionParent)
            Destroy(child.gameObject);

        List<Sprite> buttonSprites = new List<Sprite>();

        // Add the 3 correct shapes
        buttonSprites.AddRange(memoryShapes);

        // Add 3 incorrect shapes
        List<Sprite> tempIncorrect = allShapeSprites.Except(memoryShapes).ToList();
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, tempIncorrect.Count);
            buttonSprites.Add(tempIncorrect[index]);
            tempIncorrect.RemoveAt(index);
        }

        // Shuffle buttons
        buttonSprites = buttonSprites.OrderBy(a => Random.value).ToList();

        // Create buttons
        foreach (Sprite sprite in buttonSprites)
        {
            GameObject btnObj = Instantiate(selectionButtonPrefab, selectionParent);
            btnObj.GetComponent<Image>().sprite = sprite;

            Button btn = btnObj.GetComponent<Button>();
            if (btn == null) continue;

            Sprite capturedSprite = sprite;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnSelectShape(capturedSprite, btnObj));
        }
    }

    // -------------------------------
    // BUTTON CLICK LOGIC
    // -------------------------------
    void OnSelectShape(Sprite clickedSprite, GameObject clickedButton)
{
    // Hide memory shapes immediately
    foreach (Transform child in shapeParent)
        child.gameObject.SetActive(false);

    bool isCorrect = memoryShapes.Any(s => s.name == clickedSprite.name);

    // Dim clicked button
    Image btnImage = clickedButton.GetComponent<Image>();
    if (btnImage != null)
        btnImage.color = new Color(1f, 1f, 1f, 0.5f);

    // Instantiate correct/incorrect prefab as child of clicked button
    GameObject panelPrefab = isCorrect ? correctPrefab : wrongPrefab;
    GameObject panel = Instantiate(panelPrefab, clickedButton.transform); // Set parent to clicked button
    panel.transform.localPosition = new Vector3(0, 50, 0); // Position relative to button
    panel.SetActive(true);

    StartCoroutine(FadePanel(panel));

    // Update guesses
    guessCount++;
    if (isCorrect) correctCount++;

    // Show win/lose panel after 3 guesses
    if (guessCount >= 3)
    {
        if (correctCount >= 2)
            winPanel.SetActive(true);
        else
            losePanel.SetActive(true);

        // Dim all remaining buttons
        foreach (Transform btn in selectionParent)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
}



    // -------------------------------
    // FADE PANEL
    // -------------------------------
    IEnumerator FadePanel(GameObject panel)
    {
        CanvasGroup cg = panel.GetComponent<CanvasGroup>();
        if (cg == null) cg = panel.AddComponent<CanvasGroup>();

        float duration = 0.3f;
        float t = 0f;

        // Fade in
        cg.alpha = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0f, 1f, t / duration);
            yield return null;
        }
        cg.alpha = 1f;

        // Wait
        yield return new WaitForSeconds(1f);

        // Fade out
        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(1f, 0f, t / duration);
            yield return null;
        }
        Destroy(panel);
    }
}
