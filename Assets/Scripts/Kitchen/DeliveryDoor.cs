using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryDoor : BaseTable, ITickTime {
    public override TableTypes TableType => TableTypes.Delivery;

    protected override bool CanHoldItem(ItemContainer item) => GameController.Instance.Car.OnDeliveryPoint() && GameController.Instance.Car.GetDeliveryPoint().CanDeliver(item);

    protected override void SetHolding(ItemContainer item) {
        if (GameController.Instance.Car.OnDeliveryPoint()) {
            item.DestroySelf();
            GameController.Instance.Car.GetDeliveryPoint().CompleteDelivery();
        } else if (GameController.Instance.Car.OnSupplyPoint()) {
            base.SetHolding(item);
        }
    }

    public void TickTime() {
        if (GameController.Instance.Car.OnSupplyPoint() && !IsHoldingItem()) {
            ItemContainer item = Instantiate(StaticDataManager.Instance.ItemContainerPrefab, holdTransform).GetComponent<ItemContainer>();
            item.SetIngredient(GameController.Instance.Car.GetSupplyIngredient());
            SetHolding(item);
        } else if (!GameController.Instance.Car.OnSupplyPoint() && IsHoldingItem()) {
            TakeHolding().DestroySelf();
        }
    }
}
