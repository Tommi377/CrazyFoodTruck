using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial", menuName = "Tutorial")]
public class TutorialScreensSO : ScriptableObject {
    public List<Sprite> Sprites;
    public int Level;
}
