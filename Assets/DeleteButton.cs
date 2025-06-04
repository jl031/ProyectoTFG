using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] private SaveFileClicked data;
    private Image image;

    public void Clicked()
    {
        data.DeleteSaveFile();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

}
