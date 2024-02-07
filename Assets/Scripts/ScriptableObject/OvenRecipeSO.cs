using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OvenRecipe", menuName = "Recipe/OvenRecipe")]
public class OvenRecipeSO : ScriptableObject {
    public IngredientSO Input;
    public IngredientSO Output;
    public int CookTime;
    public int BurnTime;
}
