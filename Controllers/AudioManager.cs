using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//using UnityEngine.UIElements;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    [SerializeField]
    Slider sliderBGM;
    AudioSource bgmPlayer;

    [Header("#UI")]
    public AudioClip[] UIClips;
    public float UIVolume;
    [SerializeField]
    Slider sliderUI;
    public int UIChannels;
    AudioSource[] UIPlayers;
    int UIchannelIndex;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    [SerializeField]
    Slider sliderSFX;
    public int sfxChannels;
    AudioSource[] sfxPlayers;
    int sfxchannelIndex;

    public enum UI { Click, Hover}
    public enum Sfx { Loading, Noise, Scan, tvOn, glitch, gunshot}
    //public enum Hit { Type_Speed = 0, Type_Balance = 1, Type_Hard = 2 }
    private void Awake()
    {
        instance = this;
        Init();
    }
    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;

        //Debug.Log(PlayerPrefs.HasKey("SliderBGM"));
        //Debug.Log(PlayerPrefs.GetFloat("SliderBGM"));
        if (!PlayerPrefs.HasKey("SliderBGM"))
        {
            bgmPlayer.volume = 1f;
            PlayerPrefs.SetFloat("SliderBGM", sliderBGM.value);
        }
        else
        {
            bgmPlayer.volume = PlayerPrefs.GetFloat("SliderBGM");
            sliderBGM.value = PlayerPrefs.GetFloat("SliderBGM");
        }
        //bgmPlayer.volume = bgmVolume;
        //Debug.Log(bgmPlayer.volume);
        //bgmPlayer.volume = sliderBGM.value;
        bgmPlayer.clip = bgmClip;

        GameObject UIObject = new GameObject("UIPlayer");
        UIObject.transform.parent = transform;
        UIPlayers = new AudioSource[UIChannels];
        if (!PlayerPrefs.HasKey("SliderUI"))
        {
            UIVolume = 1f;
            PlayerPrefs.SetFloat("SliderUI", sliderUI.value);
        }
        else
        {
            UIVolume = PlayerPrefs.GetFloat("SliderUI");
            sliderUI.value = PlayerPrefs.GetFloat("SliderUI");
        }
        for (int index = 0; index < UIPlayers.Length; index++)
        {
            UIPlayers[index] = UIObject.AddComponent<AudioSource>();
            UIPlayers[index].playOnAwake = false;
            UIPlayers[index].volume = UIVolume;
            //UIPlayers[index].volume = sliderUI.value;
        }

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[sfxChannels];
        if (!PlayerPrefs.HasKey("SliderSFX"))
        {
            sfxVolume = 1f;
            PlayerPrefs.SetFloat("SliderSFX", sliderSFX.value);
        }
        else
        {
            sfxVolume = PlayerPrefs.GetFloat("SliderSFX");
            sliderSFX.value= PlayerPrefs.GetFloat("SliderSFX");
        }
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
            //sfxPlayers[index].volume = sliderSFX.value;
        }
    }
    private void Start()
    {
        AudioManager.instance.PlayBgm(true);
    }
    void Update()
    {
        //slider1.value = PlayerPrefs.GetFloat("SliderA");
        //slider2.value = PlayerPrefs.GetFloat("SliderB");
        //PlayerPrefs.SetFloat("SliderA", slider1.value);
        //PlayerPrefs.SetFloat("SliderB", slider2.value);
        if (!PlayerPrefs.HasKey("SliderBGM"))
        {
            sliderBGM.value = 1f;
            //AudioManager.instance.SetBGMVolume(1);
            PlayerPrefs.SetFloat("SliderBGM", sliderBGM.value);
        }
        else
        {
            //PlayerPrefs.SetFloat("SliderBGM", sliderBGM.value);
            //AudioManager.instance.SetBGMVolume(sliderBGM.value);
            PlayerPrefs.SetFloat("SliderBGM", sliderBGM.value);
            bgmPlayer.volume = PlayerPrefs.GetFloat("SliderBGM");
        }
        if (!PlayerPrefs.HasKey("SliderUI"))
        {
            sliderUI.value = 1f;
            //AudioManager.instance.SetUIVolume(1);
            PlayerPrefs.SetFloat("SliderUI", sliderUI.value);
        }
        else
        {
            //PlayerPrefs.SetFloat("SliderUI", sliderUI.value);
            //AudioManager.instance.SetUIVolume(sliderUI.value);
            for (int index = 0; index < UIPlayers.Length; index++)
            {
                UIPlayers[index].volume = PlayerPrefs.GetFloat("SliderUI");
            }
            PlayerPrefs.SetFloat("SliderUI", sliderUI.value);
            
        }
        if (!PlayerPrefs.HasKey("SliderSFX"))
        {
            sliderSFX.value = 1f;
            //AudioManager.instance.SetSFXVolume(1);
            PlayerPrefs.SetFloat("SliderSFX", sliderSFX.value);
        }
        else
        {
            //PlayerPrefs.SetFloat("SliderSFX", sliderSFX.value);
            //AudioManager.instance.SetSFXVolume(sliderSFX.value);
            for (int index = 0; index < sfxPlayers.Length; index++)
            {
                sfxPlayers[index].volume = PlayerPrefs.GetFloat("SliderSFX");
            }
            PlayerPrefs.SetFloat("SliderSFX", sliderSFX.value);
            
        }
    }
    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + sfxchannelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;

            //// 2 <= effects , use switch or if
            //if (sfx == Sfx.Hit || sfx == Sfx.Melee)
            //{
            //    ranIndex = Random.Range(0, 2);
            //}

            // pitch control

            /*
            float randomPitch = Random.Range(minPitch, maxPitch);
            sfxPlayers[loopIndex].pitch = randomPitch;
            */

            sfxchannelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }

    }
    public void PlayUI(UI ui)
    {
        for (int index = 0; index < UIPlayers.Length; index++)
        {
            int loopIndex = (index + UIchannelIndex) % UIPlayers.Length;

            //if (UIPlayers[loopIndex].isPlaying)
            //    continue;

            int ranIndex = 0;

            //// 2 <= effects , use switch or if
            //if (sfx == Sfx.Hit || sfx == Sfx.Melee)
            //{
            //    ranIndex = Random.Range(0, 2);
            //}

            UIchannelIndex = loopIndex;
            UIPlayers[loopIndex].clip = UIClips[(int)ui + ranIndex];
            UIPlayers[loopIndex].Play();
            //break;
        }

    }
}