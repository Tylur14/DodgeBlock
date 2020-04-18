using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Made by: Tyler J. Sims
// Made on: 12/17/2019
// Made for: DodgeBlock (v3)
[RequireComponent(typeof(AudioSource))]
public class Jukebox : MonoBehaviour
{
    public enum jukeBoxState { ActivePlayer, PassivePlayer }; // Active player requires a trigger AND ignores track playlist, passive player will automatically player from the list of tracks

    [Header("Jukebox Settings")]
    public jukeBoxState playerMode = jukeBoxState.ActivePlayer;
    [Range(0.0f,1.0f)]
    public float minVolume, maxVolume; // Will add support for player settings later
    public float trackChangeSpeed = 1.0f;
    public AudioClip[] tracks;

    [Header("Jukebox Information")]
    public bool changingTracks = false;
    public AudioClip nextTrack;
    public float currentVolume;
    public float timerToNextTrack;
    public int trackIndex;

    private AudioSource aSrc;
    void Start()
    {
        aSrc = GetComponent<AudioSource>();
        currentVolume = aSrc.volume;
        nextTrack = aSrc.clip;
        timerToNextTrack = nextTrack.length;
        trackIndex = 0;
    }

    void Update()
    {
        switch (playerMode)
        {
            case (jukeBoxState.ActivePlayer):
                ActivePlay();
                break;
            case (jukeBoxState.PassivePlayer):
                PassivePlay();
                break;
        }
    }

    void ActivePlay()
    {
        TrackTick();
    }

    void PassivePlay()
    {
        if (timerToNextTrack <= 0)
        {
            NextTrackIndex();
            ChangeTrack(tracks[trackIndex], tracks[trackIndex].length);
        }
        else if (timerToNextTrack > 0)
            timerToNextTrack -= Time.deltaTime;
        TrackTick();
    }

    void TrackTick()
    {
        if (changingTracks && CheckTrack())
        {
            if (aSrc.volume < maxVolume)
            {
                aSrc.volume += Time.deltaTime * trackChangeSpeed;
                if (!aSrc.isPlaying)
                {
                    aSrc.Play();
                    print(aSrc.isPlaying);
                }


            }
            else changingTracks = false;
            currentVolume = aSrc.volume;
        }

        else if (changingTracks && aSrc.volume >= minVolume && !CheckTrack())
        {
            if (aSrc.volume <= minVolume)
            {
                aSrc.clip = nextTrack;
            }
            else
            {
                aSrc.volume -= Time.deltaTime * trackChangeSpeed;
            }
            currentVolume = aSrc.volume;
        }
    }

    public bool CheckTrack()
    {
        if (nextTrack == aSrc.clip)
            return true;
        else
            return false;
    }

    public void ChangeTrack(AudioClip track)
    {
        changingTracks = true;
        nextTrack = track;
    }
    public void ChangeTrack(AudioClip track, float time)
    {
        changingTracks = true;
        nextTrack = track;
        timerToNextTrack = time;
    }

    void NextTrackIndex()
    {
        trackIndex++;
        if (trackIndex > tracks.Length - 1)
            trackIndex = 0;
    }
}
