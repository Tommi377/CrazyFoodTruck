using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour {
    [SerializeField] private List<TutorialScreensSO> tutorialScreens;

    [SerializeField] private Transform menuTransform;
    [SerializeField] private Transform tutorialTransform;

    [SerializeField] private Transform prevButtonTransform;
    [SerializeField] private Transform nextButtonTransform;
    [SerializeField] private Transform closeButtonTransform;

    [SerializeField] private TMP_Text menuText;
    [SerializeField] private TMP_Text menuSubText;
    [SerializeField] private Image tutorialScreenImage;

    private int currentPage = 0;
    private TutorialScreensSO tutorialScreen;

    public void PrepareLevel(int level, bool skipTutorial = false) {
        Time.timeScale = 0f;
        currentPage = 0;
        tutorialScreen = tutorialScreens.Find(i => i.Level == level);

        if (GameController.Instance.InfiniteMode) {
            menuText.SetText("Infinite Mode");
            menuSubText.SetText("Highscore: " + PlayerPrefs.GetInt("Highscore") + " Deliveries");
        } else {
            menuText.SetText("Day " + level);
            menuSubText.SetText("Complete " + GameController.Instance.RequiredDeliveries + " Deliveries");
        }

        gameObject.SetActive(true);

        if (tutorialScreen != null && !skipTutorial) {
            menuTransform.gameObject.SetActive(false);
            tutorialTransform.gameObject.SetActive(true);

            tutorialScreenImage.sprite = tutorialScreen.Sprites[currentPage];

            if (tutorialScreen.Sprites.Count == 1) {
                prevButtonTransform.gameObject.SetActive(false);
                nextButtonTransform.gameObject.SetActive(false);
                closeButtonTransform.gameObject.SetActive(true);
            } else {
                prevButtonTransform.gameObject.SetActive(false);
                nextButtonTransform.gameObject.SetActive(true);
                closeButtonTransform.gameObject.SetActive(false);
            }

        } else {
            menuTransform.gameObject.SetActive(true);
            tutorialTransform.gameObject.SetActive(false);
        }
    }

    public void StartLevel() {
        Time.timeScale = 1f;
        GameController.Instance.StartLevel(GameController.Instance.Level);
        gameObject.SetActive(false);
    }

    public void PreviousPage() {
        currentPage = Mathf.Max(currentPage - 1, 0);
        tutorialScreenImage.sprite = tutorialScreen.Sprites[currentPage];

        if (currentPage == 0) prevButtonTransform.gameObject.SetActive(false);
        if (!nextButtonTransform.gameObject.activeSelf) nextButtonTransform.gameObject.SetActive(true);
        if (closeButtonTransform.gameObject.activeSelf) closeButtonTransform.gameObject.SetActive(false);
    }

    public void NextPage() {
        currentPage = Mathf.Min(currentPage + 1, tutorialScreen.Sprites.Count - 1);
        tutorialScreenImage.sprite = tutorialScreen.Sprites[currentPage];

        if (!prevButtonTransform.gameObject.activeSelf) prevButtonTransform.gameObject.SetActive(true);
        if (currentPage == tutorialScreen.Sprites.Count - 1) nextButtonTransform.gameObject.SetActive(false);
        if (currentPage == tutorialScreen.Sprites.Count - 1) closeButtonTransform.gameObject.SetActive(true);
    }

    public void CloseTutorial() {
        menuTransform.gameObject.SetActive(true);
        tutorialTransform.gameObject.SetActive(false);
    }
}
