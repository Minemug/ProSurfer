using Fragsurf.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Options values
    public float MusicVolume;
    public float Effectsvolume;
    public float Fov;
    public int Resolution;
    public float Sensivity;   

    public PlayerData(ApplyButton Options)
    {
        MusicVolume = Options.music.value;
        Effectsvolume = Options.effects.value;
        Fov = Options.fov.value;
        Resolution = Options.res.value;
        Sensivity = Options.sens.value;
    }

}
