using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : BaseTable {
    public override TableTypes TableType => TableTypes.Trash;
    protected override void SetHolding(ItemContainer item) {
        item.DestroySelf();
    }
}
