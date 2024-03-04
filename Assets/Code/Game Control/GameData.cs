using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]

public class GameData
{
    public int curentLevel;
    public int countItem;
    public float Volume_Music;
    public float Volume_SoundFX;

    public GameData(int curentLevel, int countItem,float Volume_Music, float Volume_SoundFX)
    {
        this.curentLevel = curentLevel;
        this.countItem = countItem;
        this.Volume_Music = Volume_Music;
        this.Volume_SoundFX = Volume_SoundFX;
    }
}