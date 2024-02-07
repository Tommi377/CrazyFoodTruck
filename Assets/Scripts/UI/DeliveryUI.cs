using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryUI : MonoBehaviour {
    public static DeliveryUI Instance;

    [SerializeField] private Transform deliveryContainerTransform;
    [SerializeField] private GameObject deliveryItemPrefab;

    private List<DeliveryItemUI> deliveryItemUIs = new List<DeliveryItemUI>();

    private void Awake() {
        Instance = this;
    }

    public void AddDelivery(Delivery delivery) {
        DeliveryItemUI deliveryItemUI = Instantiate(deliveryItemPrefab, deliveryContainerTransform).GetComponent<DeliveryItemUI>();
        deliveryItemUI.Init(delivery);
        deliveryItemUIs.Add(deliveryItemUI);
    }

    public void RemoveDelivery(Delivery delivery) {
        DeliveryItemUI deliveryItemUI = deliveryItemUIs.Find(i => i.Delivery == delivery);
        Destroy(deliveryItemUI.gameObject);
        deliveryItemUIs.Remove(deliveryItemUI);
    }
}
