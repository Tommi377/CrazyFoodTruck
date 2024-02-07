using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SupplyItemUI : MonoBehaviour {
    [SerializeField] private Button gpsButton;
    [SerializeField] private TMP_Text distanceText;

    [SerializeField] private ItemContainer supplyItem;

    private float recalculateInverval = 1f;
    private float timeElapsed = 0f;

    private SupplyPoint closestSupplyPoint = null;

    private void Awake() {
        gpsButton.onClick.AddListener(() => {
            if (closestSupplyPoint) {
                GameController.Instance.SetGPS(closestSupplyPoint.transform.position, Color.red);
            }
        });
    }

    private void Update() {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > recalculateInverval) {
            timeElapsed = 0f;
            RecalculateClosestSupplyPoint();
        }

        if (closestSupplyPoint != null) {
            int distance = (int)Vector2.Distance(GameController.Instance.Car.transform.position, closestSupplyPoint.transform.position);
            distanceText.SetText($"{distance}m");
        }
    }


    public void Init(IngredientSO ingredient) {
        supplyItem.SetIngredient(ingredient);
        RecalculateClosestSupplyPoint();
    }

    public void RecalculateClosestSupplyPoint() {
        closestSupplyPoint = GameController.Instance.GetClosestSupplyPoint(supplyItem.GetIngredientSO());
    }
}
