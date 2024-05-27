using UnityEngine;
using UnityEngine.UI;

public class MainTransitions : MonoBehaviour
{
    public Canvas titleCanvas;
    public Canvas settingsCanvas;
    public Slider volumeSlider;
    public Toggle notificationsToggle;
    public Dropdown levelDropdown;
    public Text notificationText;

    private void Start()
    {
        ShowTitleCanvas();

        // Load saved settings
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
        notificationsToggle.isOn = PlayerPrefs.GetInt("Notifications", 1) == 1;
        levelDropdown.value = PlayerPrefs.GetInt("Level", 0); 
        levelDropdown.onValueChanged.AddListener(delegate { OnLevelChanged(); });
    }

    public void ShowTitleCanvas()
    {
        titleCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }

    public void ShowSettingsCanvas()
    {
        titleCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetInt("Notifications", notificationsToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Level", levelDropdown.value);
        PlayerPrefs.Save();
    }

    public void GoToSettings()
    {
        ShowSettingsCanvas();
    }

    public void GoBackToTitle()
    {
        SaveSettings();
        ShowTitleCanvas();
    }

    private void OnLevelChanged()
    {
        if (notificationsToggle.isOn)
        {
            string selectedLevelName = levelDropdown.options[levelDropdown.value].text;

            ShowNotification($"You have selected {selectedLevelName} mode.");
        }
    }

    private void ShowNotification(string message)
    {
        notificationText.text = message;
        notificationText.gameObject.SetActive(true);
        Invoke("HideNotification", 3f);
    }

    private void HideNotification()
    {
        notificationText.gameObject.SetActive(false);
    }
}
