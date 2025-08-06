using UnityEngine;
using UnityEngine.UI;

public class SoundToggleUI : MonoBehaviour
{
    public Button toggleButton;
    public Image iconImage;

    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    private bool isMuted = false;

    private void Start()
    {
        toggleButton.onClick.AddListener(ToggleSound);
        UpdateIcon();
    }

    private void ToggleSound()
    {
        isMuted = !isMuted;

        SoundManager.Instance.musicSource.mute = isMuted;

        foreach (var sfx in FindObjectsOfType<AudioSource>())
        {
            if (sfx.gameObject.name.Contains("SFX_Pool"))
            {
                sfx.mute = isMuted;
            }
        }

        UpdateIcon();
    }

    private void UpdateIcon()
    {
        iconImage.sprite = isMuted ? soundOffIcon : soundOnIcon;
    }
}
