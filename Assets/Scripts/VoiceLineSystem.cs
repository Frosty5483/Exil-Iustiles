using UnityEngine;

public class VoiceLineSystem : MonoBehaviour
{
    [SerializeField] private AudioClip[] voiceLines;

    public void PlayVoiceLine(int index)
    {
        gameObject.GetComponent<AudioSource>().clip = voiceLines[index];

        gameObject.GetComponent<AudioSource>().Play();
    }

}
