using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{

    public AudioSource music, effects;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
            music.Stop();
        }
        effects.volume = MainManager.Instance.effectsVol;
    }
}
