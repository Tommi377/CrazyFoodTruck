using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContextUI : MonoBehaviour {
    [SerializeField] private TMP_Text eText;
    [SerializeField] private TMP_Text fText;

    public void Update() {
        if (GameController.Instance == null) return;

        eText.transform.parent.gameObject.SetActive(false);
        fText.transform.parent.gameObject.SetActive(false);

        bool kitchenMode = GameController.Instance.KitchenMode;
        bool playerHolding = GameController.Instance.Player.IsHoldingItem();
        BaseTable table = GameController.Instance.Player.GetClosestTable();

        if (!kitchenMode) {
            SetEText("Switch to Van View");
        } else {
            if (table != null) {
                switch (table.TableType) {
                    case TableTypes.Cutting:
                        if (playerHolding && !table.IsHoldingItem())
                            SetEText("Place Food");
                        if (!playerHolding && table.IsHoldingItem())
                            SetEText("Take Food");
                        SetFText("Cut");
                        break;
                    case TableTypes.Combine:
                        if (playerHolding && !table.IsHoldingItem())
                            SetEText("Place Food");
                        if (!playerHolding && table.IsHoldingItem())
                            SetEText("Take Food");
                        SetFText("Trash Contents");
                        break;
                    case TableTypes.Oven:
                        if (playerHolding && !table.IsHoldingItem())
                            SetEText("Cook Food");
                        if (!playerHolding && table.IsHoldingItem())
                            SetEText("Take Food");
                        break;
                    case TableTypes.Trash:
                        if (playerHolding)
                            SetEText("Trash Food");
                        break;
                    case TableTypes.Seat:
                        SetEText("Drive Van");
                        break;
                    default:
                        if (playerHolding && !table.IsHoldingItem())
                            SetEText("Place Food");
                        if (!playerHolding && table.IsHoldingItem())
                            SetEText("Take Food");
                        break;
                }
            }
        }
    }

    public void SetEText(string text) {
        eText.transform.parent.gameObject.SetActive(true);
        eText.SetText(text);
    }

    public void SetFText(string text) {
        fText.transform.parent.gameObject.SetActive(true);
        fText.SetText(text);
    }
}
