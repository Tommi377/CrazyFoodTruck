using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private Transform holdTransform;

    private List<BaseTable> tablesInReach = new List<BaseTable>();
    private BaseTable closestTable;
    private ItemContainer holding;

    private void Awake() {
        if (holdTransform != null ) {
            holding = holdTransform.GetComponentInChildren<ItemContainer>();
        }
    }

    private void Update() {
        if (tablesInReach.Count > 0) {
            BaseTable table = null;
            float bestDist = float.MaxValue;
            tablesInReach.ForEach(t => {
                float dist = Vector2.Distance(t.transform.position, transform.position);
                if (dist < bestDist) {
                    bestDist = dist;
                    table = t;
                }
            });

            if (table != closestTable) {
                if (closestTable != null) {
                    closestTable.Disable();
                }
                table.Enable();
                closestTable = table;
            }
        } else if (tablesInReach.Count == 0 && closestTable != null) {
            closestTable.Disable();
            closestTable = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.transform.parent == null) return;

        BaseTable table = collision.gameObject.transform.parent.gameObject.GetComponent<BaseTable>();
        if (table != null) {
            tablesInReach.Add(table);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.transform.parent == null) return;

        BaseTable table = collision.gameObject.transform.parent.gameObject.GetComponent<BaseTable>();
        if (table != null) {
            tablesInReach.Remove(table);
        }
    }

    public bool IsHoldingItem() => holding != null;

    public void Interact() {
        Debug.Log("Interacted");
        if (closestTable != null) {
            closestTable.Interact();
        }
    }

    public void Subinteract() {
        Debug.Log("Subinteracted");
        if (closestTable != null) {
            closestTable.Subinteract();
        }
    }

    public BaseTable GetClosestTable() => closestTable;

    public ItemContainer TakeHolding() {
        if (holding == null) return null;

        SoundManager.Instance.PlaySound(SoundManager.Sound.PickUp);
        ItemContainer item = holding;
        holding.transform.parent = null;
        holding = null;
        return item;
    }

    public ItemContainer GetHolding() => holding;
    public void SetHolding(ItemContainer item) {

        SoundManager.Instance.PlaySound(SoundManager.Sound.PickUp);
        holding = item;
        holding.transform.SetParent(holdTransform);
        holding.transform.localPosition = Vector3.zero;
    }
}
