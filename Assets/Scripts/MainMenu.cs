using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartNormalMode() {
        StaticData.SetPreGameData(1, 1.75f, false);
        SceneManager.LoadScene("CityScene");
    }

    public void StartInfiniteMode() {
        StaticData.SetPreGameData(5, 1.25f, true);
        SceneManager.LoadScene("CityScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
