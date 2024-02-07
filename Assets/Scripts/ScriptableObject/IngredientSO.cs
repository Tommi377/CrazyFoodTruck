using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Ingredient", menuName="Food/Ingredient")]
public class IngredientSO : ScriptableObject {
    public Sprite Sprite;
    public int Level;
}
