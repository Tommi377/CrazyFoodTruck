using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour {
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform gpsArrow;

    private void Update() {
        if (GameController.Instance.GPSOn) {
            Enable();
            sr.color = GameController.Instance.GPSColor;
            Vector2 diff = GameController.Instance.TrackedPoint - transform.position;

            if (diff.magnitude < 2f) {
                GameController.Instance.RemoveGPS();
                return;
            }

            float zRot = Vector2.SignedAngle(Vector2.right, diff);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRot));
        } else {
            Disable();
        }
    }

    public void Enable() {
        gpsArrow.gameObject.SetActive(true);
    }

    public void Disable() {
        gpsArrow.gameObject.SetActive(false);
    }
}
