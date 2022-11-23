using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoBackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject ChooselevelMenu;
    public Image image;
    public AudioSource song;
    private Text text;
    private Color inColor;
    private Color outColor;

    public void OnPointerClick(PointerEventData eventData)
    {
        image.gameObject.SetActive(false);
        if (text != null)
        {
            text.color = outColor;
        }
        if (OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
            ChooselevelMenu.SetActive(false);
        }

        if (text != null)
        {
            text.color = outColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.gameObject.SetActive(true);
        if (text != null)
        {
            text.color = inColor;
        }
        if (song != null)
            song.Play();
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
    }

}
