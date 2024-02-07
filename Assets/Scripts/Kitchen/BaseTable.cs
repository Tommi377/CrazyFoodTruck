using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTable : MonoBehaviour {
    [SerializeField] private Transform highlightTransform;
    [SerializeField] protected Transform holdTransform;

    protected ItemContainer holding;

    protected Player Player => GameController.Instance.Player;
    public virtual TableTypes TableType => TableTypes.Table;

    public virtual bool IsHoldingItem() => holding != null;

    protected virtual bool CanHoldItem(ItemContainer item) => true;

    public virtual void Interact() {
        if (Player.IsHoldingItem() && !IsHoldingItem()) {
            if (CanHoldItem(Player.GetHolding())) {
                ItemContainer item = Player.TakeHolding();
                SetHolding(item);
            }
        } else if (!Player.IsHoldingItem() && IsHoldingItem()) {
            ItemContainer item = TakeHolding();
            Player.SetHolding(item);
        } else {
            Debug.Log("No item in hand");
        }
    }

    public virtual void Subinteract() { }

    protected virtual void SetHolding(ItemContainer item) {
        holding = item;
        holding.transform.SetParent(holdTransform);
        holding.transform.localPosition = Vector3.zero;
    }

    protected virtual ItemContainer TakeHolding() {
        ItemContainer item = holding;
        holding = null;
        return item;
    }

    public void Enable() {
        highlightTransform.gameObject.SetActive(true);
    }

    public void Disable() {
        highlightTransform.gameObject.SetActive(false);
    }
}

public enum TableTypes {
    Table, Combine, Cutting, Delivery, Seat, Oven, Trash, Fridge
}