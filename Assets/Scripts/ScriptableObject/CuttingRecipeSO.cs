using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipe", menuName = "Recipe/CuttingRecipe")]
public class CuttingRecipeSO : ScriptableObject {
    public IngredientSO Input;
    public IngredientSO Output;
    public int Toughness;
}
