using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    [SerializeField] private PauseUI pauseUI;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseUI.TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            GameController.Instance.NextLevel();
        }
    }
}
