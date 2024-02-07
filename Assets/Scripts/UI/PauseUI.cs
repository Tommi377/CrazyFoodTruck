using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour {

    private bool paused = false;

    public void TogglePause() {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        gameObject.SetActive(paused);
    }

    public void ExitGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

public static class StaticKiller {
    public static void Kill() {
        DeliveryManager.Instance = null;
        GameController.Instance = null;
        InventoryManager.Instance = null;
        SoundManager.Instance = null;
        StaticDataManager.Instance = null;
        DeliveryUI.Instance = null;
    }
}