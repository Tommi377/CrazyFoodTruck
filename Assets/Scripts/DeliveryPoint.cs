using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour {
    private Delivery currentDelivery;

    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Sprite deselectSprite;
    [SerializeField] private Sprite selectSprite;

    private void Start() {
        DeliveryManager.Instance.RegisterDeliveryPoint(this);
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        Deselect();
    }

    public bool CanDeliver(ItemContainer item) => item.GetIngredientSO() == currentDelivery?.RequestedFood;

    public void SetDelivery(Delivery delivery) {
        currentDelivery = delivery;
        gameObject.SetActive(true);
    }

    public Delivery GetDelivery() => currentDelivery;

    public void CompleteDelivery() {
        GameController.Instance.Car.RemoveCurrentDeliveryPoint();
        currentDelivery.Complete();
        currentDelivery = null;
        gameObject.SetActive(false);
    }

    public void Select() {
        sr.sprite = selectSprite;
    }

    public void Deselect() {
        sr.sprite = deselectSprite;
    }
}
