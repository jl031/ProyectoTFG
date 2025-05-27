using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    CircleCollider2D itemCollider;
    Animator animator;
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
        if (!open && prompt.activeSelf == true && Input.GetKeyDown(KeyCode.E) ) {
            open = true;
            animator.SetTrigger("OpenChest");
            item.OnPickup();
            item.Activate();
        }
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
