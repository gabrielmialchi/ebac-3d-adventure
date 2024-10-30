using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioToggle : MonoBehaviour
{
    public Button muteButton;

    private bool isMuted = false;

    void Start()
    {
        muteButton.onClick.AddListener(ToggleMute);
    }

    private void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
        // Atualize o texto do bot�o, se necess�rio
        muteButton.GetComponentInChildren<TextMeshProUGUI>().text = isMuted ? "Unmute" : "Mute";
    }
}