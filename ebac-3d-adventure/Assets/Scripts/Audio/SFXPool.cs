using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Ebac.Core.Singleton;

public class SFXPool : Singleton<SFXPool>
{
    private List<AudioSource> _audioSourceList;
    public int poolSize = 10;
    private int _index = 0;
    public AudioMixerGroup outputGroup;

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject sfxPoolGO = new GameObject("SFX_Pool_" + _audioSourceList.Count);
        sfxPoolGO.transform.SetParent(gameObject.transform);
        AudioSource audioSource = sfxPoolGO.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = outputGroup;
        _audioSourceList.Add(audioSource);
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE) return;
        var sfx = SoundManager.Instance.GetSFXByType(sfxType);

        if (_index >= 0 && _index < _audioSourceList.Count)
        {
            _audioSourceList[_index].clip = sfx.audioClip;
            _audioSourceList[_index].Play();
            _index++;
            if (_index >= _audioSourceList.Count) _index = 0;
        }
    }
}

