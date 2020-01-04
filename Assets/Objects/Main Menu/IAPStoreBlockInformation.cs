using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New IAPStoreInformation", menuName ="CrowdRunner/IAPStoreInformation")]
public class IAPStoreBlockInformation : ScriptableObject
{
    public string price;
    public int amount;
    public eItemToBuy itemToBuy;
    public Sprite itemSprite;
}
