using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(0.5f, 0.5f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }
}
