using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarPlayer : MonoBehaviour {
    private DeliveryPoint currentDeliveryPoint;
    private SupplyPoint currentSupplyPoint;

    [SerializeField] private TMP_Text text;
    [SerializeField] private ItemContainer speechBubble;

    public bool OnDeliveryPoint() => currentDeliveryPoint != null;
    public bool OnSupplyPoint() => currentSupplyPoint != null;
    public DeliveryPoint GetDeliveryPoint() => currentDeliveryPoint;
    public IngredientSO GetSupplyIngredient() => currentSupplyPoint.GetIngredient();

    public void Subinteract() {
        if (currentSupplyPoint != null) {

        }
    }

    public void RemoveCurrentDeliveryPoint() {
        text.gameObject.SetActive(false);
        speechBubble.gameObject.SetActive(false);
        currentDeliveryPoint = null;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DeliveryPoint dp = collision.GetComponentInParent<DeliveryPoint>();
        if (dp != null) {
            dp.Select();
            text.gameObject.SetActive(true);
            text.SetText("Press E to move inside van");
            speechBubble.gameObject.SetActive(true);
            speechBubble.SetIngredient(dp.GetDelivery().RequestedFood);
            currentDeliveryPoint = dp;
            return;
        }

        SupplyPoint sp = collision.GetComponentInParent<SupplyPoint>();
        if (sp != null) {
            sp.Select();
            text.gameObject.SetActive(true);
            text.SetText("Press E to move inside van");
            currentSupplyPoint = sp;
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        DeliveryPoint dp = collision.GetComponentInParent<DeliveryPoint>();
        if (dp != null && dp == currentDeliveryPoint) {
            dp.Deselect();
            text.gameObject.SetActive(false);
            RemoveCurrentDeliveryPoint();
            return;
        }

        SupplyPoint sp = collision.GetComponentInParent<SupplyPoint>();
        if (sp != null && sp != null) {
            sp.Deselect();
            text.gameObject.SetActive(false);
            currentSupplyPoint = null;
            return;
        }
    }
}
