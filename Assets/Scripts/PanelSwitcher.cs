using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public static PanelSwitcher instance;

    public GameObject mainMenu;
    public GameObject modeSelection;
    public GameObject instructions;

    public GameObject focusTracker;
    public GameObject memoryBlink;
    public GameObject spotChange;
    public GameObject dualFocus;
    public GameObject colorVision;
    public GameObject eyeSpeed;
    public GameObject hiddenObject;

    public GameObject resultPanel;

    void Awake()
    {
        instance = this;
    }

    public void ShowPanel(GameObject panel)
    {
        GameObject[] allPanels = {
            mainMenu, modeSelection, instructions,
            focusTracker, memoryBlink, spotChange,
            dualFocus, colorVision, eyeSpeed, hiddenObject,
            resultPanel
        };

        foreach (var p in allPanels) p.SetActive(false);

        panel.SetActive(true);
    }


    public GameObject GetPanelByMode(string mode)
    {
        switch(mode)
        {
            case "FocusTracker": return focusTracker;
            case "MemoryBlink": return memoryBlink;
            case "SpotChange": return spotChange;
            case "DualFocus": return dualFocus;
            case "ColorVision": return colorVision;
            case "EyeSpeed": return eyeSpeed;
            case "HiddenObject": return hiddenObject;
        }
        return null;
    }

}
