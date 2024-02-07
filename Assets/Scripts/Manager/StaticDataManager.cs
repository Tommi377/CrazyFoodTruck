using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataManager : MonoBehaviour {
    public static StaticDataManager Instance;

    [SerializeField] private List<CuttingRecipeSO> cuttingRecipes;
    [SerializeField] private List<OvenRecipeSO> ovenRecipes;
    [SerializeField] private List<CombiningRecipeSO> combiningRecipes;

    [SerializeField] private GameObject itemContainerPrefab;
    [SerializeField] private GameObject inventoryItemPrefab;

    [SerializeField] private Color progressBarRed = new Color(255, 0, 0, 255);
    [SerializeField] private Color progressBarGreen = new Color(0, 255, 0, 255);
    [SerializeField] private Color progressBarBlack = new Color(0, 0, 0, 255);

    public Color ProgressBarRed => progressBarRed;
    public Color ProgressBarGreen => progressBarGreen;
    public Color ProgressBarBlack => progressBarBlack;

    public GameObject ItemContainerPrefab => itemContainerPrefab;
    public GameObject InventoryItemPrefab => inventoryItemPrefab;

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else {
            Instance = this;
        }
    }

    public List<CombiningRecipeSO> GetCombiningRecipes() => combiningRecipes;


    public T GetRecipeFromInput<T> (IngredientSO input) {
        if (typeof(T) == typeof(CuttingRecipeSO)) {
            CuttingRecipeSO recipe = cuttingRecipes.Find(recipe => recipe.Input == input);
            return recipe ? (T)Convert.ChangeType(recipe, typeof(T)) : default;
        } else if ((typeof(T) == typeof(OvenRecipeSO))) {
            OvenRecipeSO recipe = ovenRecipes.Find(recipe => recipe.Input == input);
            return recipe ? (T)Convert.ChangeType(recipe, typeof(T)) : default;
        } else if ((typeof(T) == typeof(CombiningRecipeSO))) {
            CombiningRecipeSO recipe = combiningRecipes.Find(recipe => recipe.Inputs.Contains(input));
            return recipe ? (T)Convert.ChangeType(recipe, typeof(T)) : default;
        }
        return default;
    }
}
