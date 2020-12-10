using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class CountableItem 
{
    public Item item;
    public int number = 1;

    public CountableItem(Item item, int num)
    {
        this.item = item;
        number = num;
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//#region struct
//[System.Serializable]
//public class LevelElementPrefabWeight
//{
//    // weight needs to be passed through the entire filter/rule chain to pick a valid 
//    // levelElement therefore it makes sense (and much easier managable) to have them always bundled together
//    public LevelElement levelElement;
//    public float weight;

//    public LevelElementPrefabWeight(LevelElement levelElement, float weight = 0f)
//    {
//        this.levelElement = levelElement;
//        this.weight = weight;
//    }
//}
