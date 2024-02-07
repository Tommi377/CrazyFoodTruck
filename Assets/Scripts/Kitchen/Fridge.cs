using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : BaseTable {
    public override TableTypes TableType => TableTypes.Fridge;
    public override void Interact() {
        if (!InventoryManager.Instance.IsInventory()) {
            InventoryManager.Instance.EnableFridgeView();
        } else {
            IngredientSO ingredient = InventoryManager.Instance.GetHandIngredient();
            Player.TakeHolding()?.DestroySelf();
            if (ingredient != null) {
                ItemContainer itemContainer = Instantiate(StaticDataManager.Instance.ItemContainerPrefab).GetComponent<ItemContainer>();
                itemContainer.SetIngredient(ingredient);
                Player.SetHolding(itemContainer);
            }

            InventoryManager.Instance.DisableFridgeView();
        }
    }

    public override void Subinteract() {
        Interact();
    }
}
