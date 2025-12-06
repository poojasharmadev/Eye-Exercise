using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnStart()
    {
        PanelSwitcher.instance.ShowPanel(
            PanelSwitcher.instance.modeSelection
        );
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
