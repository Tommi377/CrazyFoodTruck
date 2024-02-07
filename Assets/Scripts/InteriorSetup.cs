using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorSetup : MonoBehaviour {
    [SerializeField] private List<GameObject> goLevel1;
    [SerializeField] private List<GameObject> goLevel2;
    [SerializeField] private List<GameObject> goLevel3;
    [SerializeField] private List<GameObject> goLevel4;

    public void InitializeInterior(int level = 5) {
        goLevel1.ForEach(i => i.SetActive(level >= 1));
        goLevel2.ForEach(i => i.SetActive(level >= 2));
        goLevel3.ForEach(i => i.SetActive(level >= 3));
        goLevel4.ForEach(i => i.SetActive(level >= 4));
    }
}
