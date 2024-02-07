using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler {
    public virtual void OnDrop(PointerEventData eventData) {
        if (Full) return;

        GameObject dropped = eventData.pointerDrag;
        InventoryItem draggedItem = dropped.GetComponent<InventoryItem>();
        draggedItem.AssignNewParentSlot(this);
    }

    public IngredientSO GetItem() => Full ? transform.GetChild(0).GetComponent<InventoryItem>().GetIngredient() : null;

    public void Clear() {
        if (Full) Destroy(transform.GetChild(0).gameObject);
    }

    public void SetItem(IngredientSO item) {
        InventoryItem invItem = Instantiate(StaticDataManager.Instance.InventoryItemPrefab, transform).GetComponent<InventoryItem>();
        invItem.Init(item);
    }

    public bool Free => transform.childCount == 0;
    public bool Full => transform.childCount > 0;

    public virtual void OnItemRemoved() { }
}