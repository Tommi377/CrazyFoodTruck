using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarVisual : MonoBehaviour {
    [SerializeField] private Transform canonicalTransform;
    [SerializeField] private List<Transform> carLayers;

    [SerializeField] private float stackOffset = 0.03125f;

    private void Update() {
        for (int i = 0; i < carLayers.Count; i++) {
            Transform t = carLayers[i];
            t.SetPositionAndRotation(canonicalTransform.position + new Vector3(0, i * stackOffset, 0), canonicalTransform.rotation);
        }
    }
}
