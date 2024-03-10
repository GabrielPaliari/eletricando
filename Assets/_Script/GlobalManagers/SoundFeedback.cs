using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFeedback : MonoBehaviour
{
    private static SoundFeedback instance;
    public static SoundFeedback Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;         
        }
    }

    [SerializeField]
    private AudioClip 
        switchOn,
        switchOff,
        winStar,
        ledChargeUp, 
        ledChargeDown,
        completeLevel;

    [SerializeField]
    private AudioSource audioSource;

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.SwitchOn:
                audioSource.PlayOneShot(switchOn);
                break;
            case SoundType.SwitchOff:
                audioSource.PlayOneShot(switchOff);
                break;
            case SoundType.WinStarSound:
                audioSource.PlayOneShot(winStar);
                break;
            case SoundType.CompleteLevelSound:
                audioSource.PlayOneShot(completeLevel);
                break;
            case SoundType.LedChargeUp:
                audioSource.Stop();
                audioSource.PlayOneShot(ledChargeUp);
                break;
            case SoundType.LedChargeDown:
                audioSource.Stop();
                audioSource.PlayOneShot(ledChargeDown);
                break;
            default:
                break;
        }
    }
}

public enum SoundType
{
    SwitchOn,
    SwitchOff,
    WinStarSound,
    LedChargeUp,
    LedChargeDown,
    CompleteLevelSound,
}
