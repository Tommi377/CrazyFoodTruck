using UnityEngine;
using UnityEngine.EventSystems;

public class InfiniteSlotUI : InventorySlotUI {
    [SerializeField] private IngredientSO ingredient;

    private void Awake() {
        Init();
    }

    public void Init() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        SetItem(ingredient);
        SetItem(ingredient);
    }

    public override void OnDrop(PointerEventData eventData) { }

    public override void OnItemRemoved() {
        SetItem(ingredient);
    }
}