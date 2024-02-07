using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VoidSlotUI : InventorySlotUI {
    public override void OnDrop(PointerEventData eventData) {
        GameObject dropped = eventData.pointerDrag;
        if (dropped.GetComponent<InventoryItem>())
            Destroy(dropped);
    }
}
