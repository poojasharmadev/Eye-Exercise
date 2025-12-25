using UnityEngine;
using TMPro;
using System.Collections;

public class DualFocusNumber : MonoBehaviour
{
    public TMP_Text numberText;   // drag Text (TMP) child here
    public float moveSpeed = 300f;
    public float minScale = 0.8f; // min size for idle animation
    public float maxScale = 1.2f; // max size for idle animation
    public float scaleSpeed = 2f; // speed of idle scale animation
    public float pulseDuration = 0.2f; // duration of pulse on click
    public float pulseScale = 1.5f; // how big it pulses

    private int value;
    private float topY;
    private DualFocusManager manager;
    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        // Random initial color
        if (numberText != null)
            numberText.color = new Color(Random.value, Random.value, Random.value);

        // Random initial scale
        float scale = Random.Range(minScale, maxScale);
        rect.localScale = Vector3.one * scale;
    }

    // Called by manager after spawn
    public void Init(int number, float topLimitY, DualFocusManager mgr)
    {
        value = number;
        topY = topLimitY;
        manager = mgr;

        numberText.text = value.ToString();
    }

    void Update()
    {
        // Move upward
        rect.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;

        // Slight pulsating scale effect
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * scaleSpeed) + 1f) / 2f);
        rect.localScale = Vector3.one * scale;

        // Destroy if it reaches top without click
        if (rect.anchoredPosition.y >= topY)
        {
            Destroy(gameObject);
        }
    }

    // Called from Button OnClick
    public void OnClick()
    {
        if (manager == null) return;

        // Call manager
        manager.OnNumberClicked(value);

        // Determine color based on correctness
        Color flashColor = (value % 2 == 0) ? Color.green : Color.red;

        // Start pulse & flash animation then destroy
        StartCoroutine(PulseAndFlash(flashColor));
    }

    private IEnumerator PulseAndFlash(Color flashColor)
    {
        // Store original scale and color
        Vector3 originalScale = rect.localScale;
        Color originalColor = numberText.color;

        float elapsed = 0f;

        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / pulseDuration;

            // Scale up then down (pulse)
            float scale = Mathf.Lerp(1f, pulseScale, 1f - Mathf.Abs(0.5f - t) * 2f);
            rect.localScale = originalScale * scale;

            // Color lerp to flashColor
            numberText.color = Color.Lerp(originalColor, flashColor, t);

            yield return null;
        }

        // Ensure final state
        numberText.color = flashColor;

        // Destroy after pulse
        Destroy(gameObject);
    }
}
