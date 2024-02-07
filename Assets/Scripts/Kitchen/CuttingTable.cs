using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTable : BaseTable {
    [SerializeField] private ProgressBar progressBar;

    public override TableTypes TableType => TableTypes.Cutting;

    private int cutsLeft = 0;
    private CuttingRecipeSO recipe;

    private CuttingRecipeSO GetRecipe(IngredientSO ingredient) => StaticDataManager.Instance.GetRecipeFromInput<CuttingRecipeSO>(ingredient);

    public bool CanCut() => IsHoldingItem() && recipe && cutsLeft > 0;

    protected override void SetHolding(ItemContainer item) {
        base.SetHolding(item);

        SetRecipe(item.GetIngredientSO());
    }

    protected override ItemContainer TakeHolding() {
        ResetTable();

        return base.TakeHolding();
    }

    public override void Subinteract() {
        if (CanCut()) {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Chop);

            cutsLeft -= 1;
            progressBar.Step();
            if (cutsLeft <= 0) {
                holding.SetIngredient(recipe.Output);
                SetRecipe(recipe.Output);
            }
        }
    }

    private void SetRecipe(IngredientSO ingredient) {
        CuttingRecipeSO recipe = GetRecipe(ingredient);
        if (recipe) {
            this.recipe = recipe;
            cutsLeft = recipe.Toughness;
            progressBar.StartProgress(cutsLeft);
        } else {
            ResetTable();
        }
    }

    private void ResetTable() {
        recipe = null;
        cutsLeft = 0;
        progressBar.ResetProgress();
    }
}
