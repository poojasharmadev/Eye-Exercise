using UnityEngine;
using TMPro;

public class InstructionUI : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;

    private void OnEnable()
    {
        title.text = ModeManager.SelectedMode;

        switch(ModeManager.SelectedMode)
        {
            case "FocusTracker":
                description.text = "Follow the moving object with your eyes.";
                break;

            case "MemoryBlink":
                description.text = "Memorize the shapes before they disappear.";
                break;

            case "SpotChange":
                description.text = "Find the element that changes between two images.";
                break;

            case "DualFocus":
                description.text = "Follow the moving object and tap even numbers.";
                break;

            case "ColorVision":
                description.text = "Select the correct COLOR, not the text.";
                break;

            case "EyeSpeed":
                description.text = "Tap targets as fast as possible.";
                break;

            case "HiddenObject":
                description.text = "Find all hidden objects in the image.";
                break;
        }
    }

    public void StartMode()
    {
        PanelSwitcher.instance.ShowPanel(
            PanelSwitcher.instance.GetPanelByMode(ModeManager.SelectedMode)
        );
    }
}
