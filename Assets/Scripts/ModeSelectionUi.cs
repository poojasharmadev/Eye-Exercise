using UnityEngine;

public class ModeSelectionUi : MonoBehaviour
{
    public void SelectMode(string mode)
    {
        ModeManager.SelectedMode = mode;
        PanelSwitcher.instance.ShowPanel(
            PanelSwitcher.instance.instructions
        );
    }
}
