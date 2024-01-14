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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    [SerializeField]
    private AudioClip 
        clickSound,
        placeSound,
        removeSound,
        wrongPlacementSound,
        correctSignalSound,
        victorySound;

    [SerializeField]
    private AudioSource audioSource;

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Click:
                audioSource.PlayOneShot(clickSound);
                break;
            case SoundType.Place:
                audioSource.PlayOneShot(placeSound);
                break;
            case SoundType.Remove:
                audioSource.PlayOneShot(removeSound);
                break;
            case SoundType.WrongPlacement:
                audioSource.PlayOneShot(wrongPlacementSound);
                break;
            case SoundType.CorrectSignalSound:
                audioSource.PlayOneShot(correctSignalSound);
                break;
            case SoundType.VictorySound:
                audioSource.PlayOneShot(victorySound);
                break;
            default:
                break;
        }
    }
}

public enum SoundType
{
    Click,
    Place,
    Remove,
    WrongPlacement,
    CorrectSignalSound,
    VictorySound
}
