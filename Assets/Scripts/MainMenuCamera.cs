using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MainMenuCamera : MonoBehaviour {
    [SerializeField] private List<Transform> points;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float cameraDamp = 0.3f;
    private int currentPoint = 0;

    private Vector3 velocity = Vector3.zero;

    private void Update() {
        if (Vector2.Distance(transform.position, points[currentPoint].position) < 5f) {
            currentPoint = (currentPoint + 1) % points.Count;
        }

        //transform.position = Vector3.Lerp(transform.position, points[currentPoint].position, Time.deltaTime * speed);
        transform.position = Vector3.SmoothDamp(transform.position, points[currentPoint].position, ref velocity, cameraDamp); 
    }
}
