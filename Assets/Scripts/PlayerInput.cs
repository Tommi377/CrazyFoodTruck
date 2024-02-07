using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 5f;

    private Player player;
    private Rigidbody2D rb;

    private Vector2 movement;

    private bool CanMove() => !InventoryManager.Instance.IsInventory();

    private void Awake() {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (GameController.Instance.KitchenMode) {
            if (CanMove()) {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            } else {
                movement = Vector2.zero;
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                player.Interact();
            }

            if (Input.GetKeyDown(KeyCode.F)) {
                player.Subinteract();
            }

        }

        if (movement.x < 0) animator.transform.localScale = new Vector3(-1, 1, 1);
        if (movement.x > 0) animator.transform.localScale = new Vector3(1, 1, 1);

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.magnitude > 0) {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Walk);
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
