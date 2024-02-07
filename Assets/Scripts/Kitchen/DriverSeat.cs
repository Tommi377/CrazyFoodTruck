using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverSeat : BaseTable {
    public override TableTypes TableType => TableTypes.Seat;
    public override void Interact() {
        GameController.Instance.ToggleMode();
    }
}
