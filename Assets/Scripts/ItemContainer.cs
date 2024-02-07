using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour {
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Image image;
    [SerializeField] private IngredientSO ingredientSO;

    private void Awake() {
        if (ingredientSO != null) {
            SetSprite();
        }
    }

    public void SetIngredient(IngredientSO ingredientSO) {
        this.ingredientSO = ingredientSO;
        SetSprite();
    }

    public IngredientSO GetIngredientSO() => ingredientSO;

    public void DestroySelf() => Destroy(gameObject);

    private void SetSprite() {
        if (sr != null) sr.sprite = ingredientSO.Sprite;
        if (image != null) image.sprite = ingredientSO.Sprite;
    }
}
