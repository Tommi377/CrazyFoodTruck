using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour {
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 50f;

    private float move;
    private float rotation;

    private void Update() {
        if (GameController.Instance.CityMode) {
            move = Input.GetAxisRaw("Vertical");
            rotation = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.E)) {
                GameController.Instance.ToggleMode();
            } else if (Input.GetKeyDown(KeyCode.F)) {
                GameController.Instance.Car.Subinteract();
            }
        } else {
            move = 0;
            rotation = 0;
        }

        if (move != 0) {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Car);
        }
    }

    private void FixedUpdate() {
        //rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime * (Vector2)rb.transform.right);
        rb.MoveRotation(rb.rotation - rotation * rotationSpeed * Time.fixedDeltaTime);
        rb.AddForce(move * moveSpeed * Time.fixedDeltaTime * (Vector2)rb.transform.right);
    }
}
