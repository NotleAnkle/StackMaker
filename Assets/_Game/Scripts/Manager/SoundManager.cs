using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioType
{
    FX_Pickup = 0,
    FX_Win = 1,
    FX_Lose = 2,
    FX_Move = 3,
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
    }

    private List<AudioSource> audies = new List<AudioSource>();
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<AudioClip> clips;

    private void Start()
    {
        for (int i = 0; i < clips.Count; i++)
        {
            GameObject obj = new GameObject(clips[i].name);
            obj.transform.SetParent(transform);
            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = clips[i];

            audies.Add(source);
        }
    }

    public void PlayClip(AudioType type)
    {
        this.audies[(int)type].Play();

        //audioSource.PlayOneShot(clips[(int)type]);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

    }

}
