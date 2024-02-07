using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController Instance;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (SceneManager.GetActiveScene().name == "KitchenScene") {
                SceneManager.LoadScene("CityScene");
            } else {
                SceneManager.LoadScene("KitchenScene");
            }
        }
    }
}
