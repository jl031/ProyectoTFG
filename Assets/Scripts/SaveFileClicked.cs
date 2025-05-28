using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveFileClicked : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int saveFileId;
    public SaveFileData data;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (data == null)
        {

        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(0.5f, 0.5f, 0.5f); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = new Color(1, 1, 1); 
    }
}
