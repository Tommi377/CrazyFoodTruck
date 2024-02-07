using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombiningTable : BaseTable {
    [SerializeField] private GameObject foodIconPrefab;
    [SerializeField] private Transform gridContainer;

    public override TableTypes TableType => TableTypes.Combine;

    private List<CombiningRecipeSO> possibleRecipes = new List<CombiningRecipeSO>();
    private List<IngredientSO> containing = new List<IngredientSO>();

    private List<CombiningRecipeSO> GetPossibleRecipes(IngredientSO ingredient, bool fromStart = false) {
        List<CombiningRecipeSO> recipes = fromStart ? StaticDataManager.Instance.GetCombiningRecipes() : possibleRecipes;

        List<IngredientSO> inputs = containing.Concat(new List<IngredientSO>() {ingredient}).ToList();


        foreach (IngredientSO input in inputs) {
            recipes = recipes.Where(recipe => recipe.Inputs.Contains(input)).ToList();
        }

        return recipes;
    }

    protected void Start() {
        ResetTable();
    }

    protected override bool CanHoldItem(ItemContainer item) => !containing.Contains(item.GetIngredientSO()) && GetPossibleRecipes(item.GetIngredientSO()).Any();

    protected override void SetHolding(ItemContainer item) {
        containing.Add(item.GetIngredientSO());
        possibleRecipes = GetPossibleRecipes(item.GetIngredientSO());
        Instantiate(foodIconPrefab, gridContainer).GetComponentInChildren<ItemContainer>().SetIngredient(item.GetIngredientSO());
        item.DestroySelf();

        if (possibleRecipes.Count == 1 && possibleRecipes[0].Inputs.All(input => containing.Contains(input))) {
            holding = Instantiate(StaticDataManager.Instance.ItemContainerPrefab, holdTransform).GetComponent<ItemContainer>();
            holding.SetIngredient(possibleRecipes[0].Output);
            holding.transform.localPosition = Vector3.zero;

            ResetTable();
        }
    }

    public override void Subinteract() {
        ResetTable();
    }

    private void ResetTable() {
        foreach (Transform child in gridContainer) {
            Destroy(child.gameObject);
        }

        possibleRecipes = StaticDataManager.Instance.GetCombiningRecipes();
        containing.Clear();
    }
}
