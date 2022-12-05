using Fragsurf.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fragsurf.Movement
{
    [System.Serializable]
    public class PlayerOptions
    {
        //alldata values
        public float MusicVolume;
        public float Effectsvolume;
        public float Fov;
        public int Resolution;
        public float Sensivity;

        public PlayerOptions(ApplyButton _applyButton)
        {
            MusicVolume = _applyButton.music.value;
            Effectsvolume = _applyButton.effects.value;
            Fov = _applyButton.fov.value;
            Resolution = _applyButton.res.value;
            Sensivity = _applyButton.sens.value;
        }
    }

    [System.Serializable]
    public class PlayerScores { 

        private int numberOfScenes = SceneManager.sceneCountInBuildSettings; 
        public List<float> BestSpeed = new List<float>(4);
        public List<float> BestTime = new List<float>(4);
        
        public PlayerScores(SurfCharacter Scores)
        {
            for (int i = 0; i < numberOfScenes; i++)
            {
                BestSpeed.Insert(i, Scores.BestSpeeds[i]);
            }
            
            for (int i = 0; i < numberOfScenes; i++)
            {
                BestTime.Insert(i, Scores.BestTimes[i]);
            }

            
        }

    }



}
