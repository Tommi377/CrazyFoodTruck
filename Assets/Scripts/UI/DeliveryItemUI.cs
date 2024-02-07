using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryItemUI : MonoBehaviour {
    [SerializeField] private Image timerImage;
    [SerializeField] private Button recipeButton;
    [SerializeField] private Button gpsButton;
    [SerializeField] private TMP_Text distanceText;

    [SerializeField] private Transform outputContainerTransform;
    [SerializeField] private Transform inputContainerTransform;

    [SerializeField] private GameObject foodBigPrefab;
    [SerializeField] private GameObject foodSmallPrefab;

    public Delivery Delivery { get; private set; }

    private void Awake() {
        recipeButton.onClick.AddListener(() => {
            DeliveryManager.Instance.ShowRecipe(Delivery);
        });
        gpsButton.onClick.AddListener(() => {
            GameController.Instance.SetGPS(Delivery.DeliveryPoint.transform.position, Color.blue);
        });
    }

    private void Update() {
        timerImage.fillAmount = (Delivery.TimeLimit - Delivery.TimeElapsed) / Delivery.TimeLimit;
        int distance = (int)Vector2.Distance(GameController.Instance.Car.transform.position, Delivery.DeliveryPoint.transform.position);
        distanceText.SetText($"{distance}m");
    }

    public void Init(Delivery delivery) {
        Delivery = delivery;
        GameObject go = Instantiate(foodBigPrefab, outputContainerTransform);
        go.transform.GetChild(0).GetComponent<Image>().sprite = delivery.RequestedFood.Sprite;
    }
}
