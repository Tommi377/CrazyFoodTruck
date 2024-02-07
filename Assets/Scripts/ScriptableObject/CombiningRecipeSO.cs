using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombiningRecipe", menuName = "Recipe/CombiningRecipe")]
public class CombiningRecipeSO : ScriptableObject {
    public List<IngredientSO> Inputs;
    public IngredientSO Output;
}
