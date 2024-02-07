using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : BaseTable, ITickTime {
    [SerializeField] private ProgressBar progressBar;

    public override TableTypes TableType => TableTypes.Oven;

    private OvenRecipeSO recipe;
    private float elapsedTime = 0;
    private bool cooked = false;

    private OvenRecipeSO GetRecipe(IngredientSO ingredient) => StaticDataManager.Instance.GetRecipeFromInput<OvenRecipeSO>(ingredient);

    protected override bool CanHoldItem(ItemContainer item) {
        OvenRecipeSO recipe = GetRecipe(item.GetIngredientSO());
        if (recipe != null) {
            SetRecipe(recipe);
        }
        return recipe != null;
    }

    protected override ItemContainer TakeHolding() {
        ResetTable();

        return base.TakeHolding();
    }

    private void SetRecipe(OvenRecipeSO recipe) {
        this.recipe = recipe;
        elapsedTime = 0;
        cooked = false;

        progressBar.SetBackgroundColor(StaticDataManager.Instance.ProgressBarBlack);
        progressBar.SetBarColor(StaticDataManager.Instance.ProgressBarGreen);
        progressBar.StartProgress(recipe.CookTime, true);
    }

    private void ResetTable() {
        recipe = null;
        elapsedTime = 0;
        cooked = false;
        progressBar.ResetProgress();
    }

    public void TickTime() {
        if (recipe != null) {
            SoundManager.Instance.PlaySound(SoundManager.Sound.Oven);

            elapsedTime += Time.deltaTime;
            progressBar.SetProgress(elapsedTime);

            if (!cooked && elapsedTime > recipe.CookTime) {
                cooked = true;
                elapsedTime = 0;
                holding.SetIngredient(recipe.Output);

                progressBar.SetBackgroundColor(StaticDataManager.Instance.ProgressBarGreen);
                progressBar.SetBarColor(StaticDataManager.Instance.ProgressBarRed);
                progressBar.StartProgress(recipe.BurnTime, true);
            }

            if (cooked && elapsedTime > recipe.BurnTime) {
                holding.DestroySelf();
                holding = null;
                ResetTable();
            }
        }
    }
}
