using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField] private Image image;

    private IngredientSO ingredient;
    private InventorySlotUI parentSlot;

    public IngredientSO GetIngredient() => ingredient;

    private void Awake() {
        parentSlot = transform.parent.GetComponent<InventorySlotUI>();
    }

    public void Init(IngredientSO ingredient) {
        this.ingredient = ingredient;
        image.sprite = ingredient.Sprite;
    }

    public void AssignNewParentSlot(InventorySlotUI slot) {
        parentSlot.OnItemRemoved();
        parentSlot = slot;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentSlot.transform);
        transform.localPosition = Vector3.zero;
        image.raycastTarget = true;
    }
}
