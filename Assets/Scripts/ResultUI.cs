using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    public TMP_Text scoreText;

    private void OnEnable()
    {
        scoreText.text = "Score: " + GameManager.Score;
    }

    public void OnReplay()
    {
        PanelSwitcher.instance.ShowPanel(
            PanelSwitcher.instance.GetPanelByMode(ModeManager.SelectedMode)
        );
    }

    public void OnBack()
    {
        PanelSwitcher.instance.ShowPanel(
            PanelSwitcher.instance.modeSelection
        );
    }
}
