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
    private bool open = false;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer itemSpriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        itemCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemSpriteRenderer = item.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!open && prompt.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            open = true;
            animator.SetTrigger("OpenChest");
            item.OnPickup();
            item.Activate();
            item.AddToList();
            itemSpriteRenderer.sprite = Sprite.Create(item.sprite, new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));
            itemSpriteRenderer.enabled = true;
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
        spriteRenderer.enabled = false;
        itemSpriteRenderer.enabled = false;
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
