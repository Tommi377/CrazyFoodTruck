using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUI : MonoBehaviour {
    [SerializeField] private Image image;

    public void Select() {
        image.color = new Color(0.5f, 1f, 0.5f);
    }

    public void Deselect() {
        image.color = Color.white;
    }
}
