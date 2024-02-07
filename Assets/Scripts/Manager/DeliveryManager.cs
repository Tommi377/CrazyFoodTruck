using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager Instance;

    [SerializeField] private List<DeliverySO> deliverySOs;
    [SerializeField] private DeliveryUI deliveryUI;

    [SerializeField] private List<IngredientToRecipeSprite> recipeBookSprite;
    [SerializeField] private Transform recipeBookTransform;
    [SerializeField] private Image recipeBookImage;

    private List<DeliveryPoint> deliveryPoints = new List<DeliveryPoint>();
    public List<Delivery> Deliveries { get; private set; } = new List<Delivery>();

    public Stack<DeliverySO> DeliveryStack { get; private set; }

    [SerializeField] private float deliveryInterval = 60f;
    [SerializeField] private int maxDeliveries = 4;

    private float intensity = 1f;

    public int DeliveriesDelivered { get; private set; } = 0;
    public int DeliveriesFailed { get; private set; } = 0;

    private float timeElapsed = 0f;

    private void Awake() {
        Instance = this;
    }

    public void RegisterDeliveryPoint(DeliveryPoint deliveryPoint) {
        deliveryPoints.Add(deliveryPoint);
    }

    private void Update() {
        if (!GameController.Instance.InGame) return;

        if (maxDeliveries <= Deliveries.Count) {
            timeElapsed = 0;
        } else if (Deliveries.Count == 0 && deliveryInterval - timeElapsed > 1f) {
            timeElapsed = deliveryInterval - 1f;
        } else {
            timeElapsed += Time.deltaTime;
        }

        Deliveries.Where(d => d.Failed).ToList().ForEach(d => FailDelivery(d));
        foreach (Delivery delivery in Deliveries) {
            delivery.Update();
        }

        if (timeElapsed > deliveryInterval) {
            timeElapsed = 0f;
            AssignDelivery();
        }
    }

    public void LevelStart(int level) {
        DeliveriesDelivered = 0;
        DeliveriesFailed = 0;
        intensity = 1f;
        timeElapsed = deliveryInterval - 1f;
        GenerateDeliveryStack();
    }

    public void LevelEnd() {
        Deliveries.ForEach(delivery => {
            delivery.DeliveryPoint.gameObject.SetActive(false);
            DeliveryUI.Instance.RemoveDelivery(delivery);
        });
        Deliveries.Clear();
    }

    private void GenerateDeliveryStack() {
        int level = GameController.Instance.Level;
        DeliveryStack = new Stack<DeliverySO>(deliverySOs.Where(d => d.Level <= level).OrderBy(d => UnityEngine.Random.Range(0, 100)));
    }

    public void ShowRecipe(Delivery delivery) {
        Sprite sprite = recipeBookSprite.Find(item => item.Ingredient == delivery.RequestedFood)?.Sprite;
        recipeBookImage.sprite = sprite ?? delivery.RequestedFood.Sprite;
        recipeBookTransform.gameObject.SetActive(true);

    }

    public void CompleteDelivery(Delivery delivery) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.DeliverComplete);
        Deliveries.Remove(delivery);
        DeliveryUI.Instance.RemoveDelivery(delivery);

        DeliveriesDelivered += 1;
    }

    public void FailDelivery(Delivery delivery) {
        SoundManager.Instance.PlaySound(SoundManager.Sound.DeliverFail);
        Deliveries.Remove(delivery);
        DeliveryUI.Instance.RemoveDelivery(delivery);

        DeliveriesFailed += 1;
    }

    private void AssignDelivery() {

        if (DeliveryStack.Count == 0) {
            GenerateDeliveryStack();
        }

        DeliveryPoint deliveryPoint = deliveryPoints[UnityEngine.Random.Range(0, deliveryPoints.Count)];
        if (Deliveries.Any(delivery => delivery.DeliveryPoint == deliveryPoint)) {
            AssignDelivery();
            return;
        }

        DeliverySO deliverySO = DeliveryStack.Pop();

        deliveryInterval = intensity * deliverySO.TimeLimit * GameController.Instance.DifficultyModifier;
        intensity *= .9f;
        Deliveries.Add(new Delivery(deliveryPoint, deliverySO));
    }
}

public class Delivery {
    public DeliveryPoint DeliveryPoint;
    public IngredientSO RequestedFood;
    public float TimeLimit;
    public float TimeElapsed = 0f;

    public bool Failed { get; private set; } = false;

    public Delivery(DeliveryPoint deliveryPoint, DeliverySO deliverySO) { 
        DeliveryPoint = deliveryPoint;
        RequestedFood = deliverySO.Product;
        TimeLimit = deliverySO.TimeLimit * GameController.Instance.DifficultyModifier;

        DeliveryPoint.SetDelivery(this);
        DeliveryUI.Instance.AddDelivery(this);
    }


    public void Complete() {
        DeliveryManager.Instance.CompleteDelivery(this);
    }

    public void Update() {
        TimeElapsed += Time.deltaTime;
        if (TimeElapsed > TimeLimit) {
            Failed = true;
        }
    }
}

[Serializable]
public class IngredientToRecipeSprite {
    public IngredientSO Ingredient;
    public Sprite Sprite;
}