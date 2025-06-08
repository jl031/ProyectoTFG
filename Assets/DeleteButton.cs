using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] private SaveFileClicked data;
    public void Clicked()
    {
        data.DeleteSaveFile();
    }

}
