using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager Instance;

    [SerializeField] private Transform fridgeTransform;
    [SerializeField] private List<InventorySlotUI> fridgeSlots;

    [SerializeField] private Transform handTransform;
    [SerializeField] private InventorySlotUI handSlot;

    [SerializeField] private Transform supplyTransform;
    [SerializeField] private InfiniteSlotUI supplySlot;

    [SerializeField] private Transform trashTransform;

    [SerializeField] private List<IngredientSO> initialFridgeInventory;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        for (int i = 0; i < initialFridgeInventory.Count; i++) {
            fridgeSlots[i].SetItem(initialFridgeInventory[i]);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            if (!handTransform.gameObject.activeSelf) {
                EnableHand();
            } else {
                DisableHand();
            }
        }
    }

    public bool IsInventory() => fridgeTransform.gameObject.activeSelf ||
        handTransform.gameObject.activeSelf ||
        supplyTransform.gameObject.activeSelf ||
        trashTransform.gameObject.activeSelf;

    public IngredientSO GetHandIngredient() => handSlot.GetItem();

    public void EnableFridgeView() {
        EnableHand();
        fridgeTransform.gameObject.SetActive(true);
        trashTransform.gameObject.SetActive(true);
    }

    public void DisableFridgeView() {
        DisableHand();
        fridgeTransform.gameObject.SetActive(false);
        trashTransform.gameObject.SetActive(false);
    }

    public void EnableHand() {
        if (GameController.Instance.Player.IsHoldingItem()) {
            handSlot.SetItem(GameController.Instance.Player.GetHolding().GetIngredientSO());
        }
        handTransform.gameObject.SetActive(true);
    }

    public void DisableHand() {
        handSlot.Clear();
        handTransform.gameObject.SetActive(false);
    }
}
