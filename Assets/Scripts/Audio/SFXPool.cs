using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SFXPool : Singleton<SFXPool>
{

    private List<AudioSource> _audioSourceList;

    public int poolSize = 10;

    private int _index = 0;

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for(int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        AudioSource audioSource = go.AddComponent<AudioSource>();
        _audioSourceList.Add(audioSource);
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.NOME) return;

        var sfx = SoundManager.Instance.GetSFXByType(sfxType);
        if (sfx == null || sfx.audioClip == null) return;

        var currentAudioSource = _audioSourceList[_index];
        currentAudioSource.clip = sfx.audioClip;
        currentAudioSource.volume = SoundManager.Instance.sfxVolume; // Aplica volume
        currentAudioSource.Play();

        _index++;
        if (_index >= _audioSourceList.Count) _index = 0;
    }
}
