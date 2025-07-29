using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [System.Serializable]
    public class UpgradeUIItem
    {
        public UpgradeDefinition Definition;
        public Text ValueText;
        public Text LevelText;
        public Button UpgradeButton;
        public Slider Slider;
    }

    public UpgradeUIItem[] UpgradeItems;
    public Text SkillPointsText;
    public ParticleSystem Particles;
    public Button ResetButton;

    [Header("Error")]
    public GameObject ErrorPopup;
    public Text ErrorText;

    public void ShowError(string msg)
    {
        ErrorPopup.SetActive(true);
        ErrorText.text = msg;
        Invoke(nameof(HideError), 2f);
    }

    private void HideError()
    {
        ErrorPopup.SetActive(false);
    }
}
