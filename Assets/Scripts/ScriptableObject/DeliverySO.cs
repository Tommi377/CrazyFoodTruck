using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Delivery", menuName = "Food/Delivery")]
public class DeliverySO : ScriptableObject {
    public IngredientSO Product;
    public float TimeLimit;
    public int Level;
}
