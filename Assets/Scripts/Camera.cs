using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    [SerializeField] private float cameraDamp = 0.3f;
    [SerializeField] private Transform carTransform;
    [SerializeField] private Transform interiorTransform;

    [SerializeField] private Vector3 kitchenCameraOffset = new Vector3(2, 1, 0);

    private Vector3 velocity = Vector3.zero;

    private void Update() {
        Vector3 offset = new Vector3(0, 0, -10);
        if (GameController.Instance.KitchenMode) {
            offset += kitchenCameraOffset;
        }

        Vector3 targetPos = GameController.Instance.KitchenMode ? interiorTransform.position : carTransform.position;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos + offset, ref velocity, cameraDamp);
    }
}
