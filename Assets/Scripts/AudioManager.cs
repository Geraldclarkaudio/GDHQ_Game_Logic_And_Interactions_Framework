using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Audio Manager is null");
            }
            return _instance;
        }
    }
    [SerializeField]
    private AudioSource _sfxAudioSource;
    [SerializeField]
    private AudioSource _sfxAudioSource2;
    [SerializeField]
    private AudioSource _mxAudioSource;
    [SerializeField]
    private AudioClip _music;
    [SerializeField]
    private AudioClip[] _FireWeaponClip;
    [SerializeField]
    private AudioClip[] _aiDeathClip;
    [SerializeField]
    private AudioClip[] _barrierHitClip;
    [SerializeField]
    private AudioClip _aiCompleteClip;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        PlayMusic();
    }
    public void PlayMusic()
    {
        _mxAudioSource.clip = _music;
        _mxAudioSource.Play();
    }

    public void FireWeapon()
    {
        _sfxAudioSource.pitch = 1;
        _sfxAudioSource.clip = _FireWeaponClip[Random.Range(0, _FireWeaponClip.Length)];
        _sfxAudioSource.pitch = Random.Range(0.85f, 1f);
        _sfxAudioSource.Play();
    }

    public void AIDeath()
    {
        _sfxAudioSource2.pitch = 1;
        _sfxAudioSource2.clip = _aiDeathClip[Random.Range(0, _aiDeathClip.Length)];
        _sfxAudioSource2.pitch = Random.Range(0.85f, 1f);
        _sfxAudioSource2.Play();
    }
    public void HitBarrier()
    {
        _sfxAudioSource2.pitch = 1;
        _sfxAudioSource2.clip = _barrierHitClip[Random.Range(0, _barrierHitClip.Length)];
        _sfxAudioSource2.pitch = Random.Range(0.85f, 1f);
        _sfxAudioSource2.Play();
    }
    public void AIComplete()
    {
        _sfxAudioSource.clip = _aiCompleteClip;
        _sfxAudioSource.Play();
    }
}
