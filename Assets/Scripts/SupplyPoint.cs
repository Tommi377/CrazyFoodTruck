using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyPoint : MonoBehaviour {
    [SerializeField] private IngredientSO supplying;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Sprite deselectSprite;
    [SerializeField] private Sprite selectSprite;

    public IngredientSO GetIngredient() => supplying;

    private void Start() {
        GetComponent<ItemContainer>().SetIngredient(supplying);
        GameController.Instance.RegisterSupplyPoint(this);
    }

    public void Select() {
        sr.sprite = selectSprite;
    }

    public void Deselect() {
        sr.sprite = deselectSprite;
    }
}
