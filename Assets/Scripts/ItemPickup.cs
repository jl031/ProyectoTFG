using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    CircleCollider2D itemCollider;
    Animator animator;
    public TextMeshProUGUI text;
    public GameObject prompt;
    public Item item;
    private Boolean open = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (!open && prompt.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            open = true;
            animator.SetTrigger("OpenChest");
            item.OnPickup();
            item.Activate();
            text.text = item.ItemName + "\n" + item.Description;
            text.gameObject.SetActive(true);
            Destroy(itemCollider);

            StartCoroutine(DisableItemText());

        }
    }

    IEnumerator DisableItemText()
    {
        yield return new WaitForSeconds(2);
        text.gameObject.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            prompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            prompt.SetActive(false);
        }        
    }
}
