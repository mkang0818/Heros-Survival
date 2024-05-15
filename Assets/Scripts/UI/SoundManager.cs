using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] PlayerPainAudio;
    public AudioClip BulletDestroyAudio;
    public AudioClip Ch11SkiillSound;

    [SerializeField] public AudioSource MusicAudio;

    [SerializeField] public float MusicVolume = 50;
    [SerializeField] public float EffectVolume = 50;


    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicSound", -1);
        EffectVolume = PlayerPrefs.GetFloat("EffectSound", -1);
    }
    public void SoundPlay(string SoundName, AudioClip clip)
    {
        GameObject SoundObj = new GameObject(SoundName+"Sound");
        AudioSource audiosource = SoundObj.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.volume = EffectVolume;
        audiosource.Play();

        Destroy(SoundObj, clip.length);
    }
}
