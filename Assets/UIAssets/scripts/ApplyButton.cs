using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Fragsurf.Movement
{
    public class ApplyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
        public Image image;
        public AudioSource effectsSource, musicSource, succesSound;
        private Text text;
        private Color inColor, outColor;
        //Switches
        public Slider music, effects, sens, fov;
        public Dropdown res, quality;
        public InputField _music, _effects, _sens, _fov;
        //gamesens

        public void OnPointerClick(PointerEventData eventData)
        {

            effectsSource.volume = effects.value;
            if (succesSound != null)
                succesSound.Play();

        }
        public void SlidersChanged()
        {
            effectsSource.volume = effects.value;
            musicSource.volume = music.value;
            // set manager variables
            MainManager.Instance.effectsVol = effects.value;
            MainManager.Instance.musicVol = music.value;
            MainManager.Instance.sensivity = sens.value;
            MainManager.Instance.fov = fov.value;

            _music.text = Math.Round(music.value*100,0).ToString();
            _effects.text = Math.Round(effects.value*100,0).ToString();
            _fov.text = Math.Round(fov.value,0).ToString();
            _sens.text = Math.Round(sens.value,2).ToString();
            //music.value = float.Parse(_music.text);
            //effects.value = float.Parse(_effects.text);
            //sens.value = float.Parse(_sens.text);
            //fov.value = float.Parse(_fov.text);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            image.gameObject.SetActive(true);
            if (text != null)
            {
                text.color = inColor;
            }
            if (effectsSource != null)
                effectsSource.Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            image.gameObject.SetActive(false);
            if (text != null)
            {
                text.color = outColor;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            text = gameObject.GetComponent<Text>();
            inColor = Color.black;
            outColor = Color.white;

            List<Slider> sliders = new List<Slider>();
            sliders.Add(music);
            sliders.Add(effects);
            sliders.Add(fov);
            sliders.Add(sens);
            foreach (var slider in sliders)
            {
                slider.onValueChanged.AddListener(delegate { SlidersChanged(); });
            }
            //DropBoxes
            res.onValueChanged.AddListener(delegate { ResolutionChanged(); });
            quality.onValueChanged.AddListener(delegate { QualityChanged(); });
            //input fields delegation
            _music.onEndEdit.AddListener(delegate { SlidersChanged(); });
            _effects.onEndEdit.AddListener(delegate { SlidersChanged(); });
            _fov.onEndEdit.AddListener(delegate { SlidersChanged(); });
            _sens.onEndEdit.AddListener(delegate { SlidersChanged(); });
        }

        private void QualityChanged()
        {
            QualitySettings.SetQualityLevel(quality.value, true);
        }

        private void ResolutionChanged()
        {
            switch (res.value)
            {
                case 0:
                    Screen.SetResolution(1920, 1080, true);
                    break;
                case 1:
                    Screen.SetResolution(1280, 720, true);
                    break;
                case 2:
                    Screen.SetResolution(720, 480, true);
                    break;
            }
        }
    }
}



