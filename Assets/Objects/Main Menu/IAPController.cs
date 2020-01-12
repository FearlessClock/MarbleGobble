using EasyMobile;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

// Add items that can be bought here
public enum eItemToBuy { Coin50, Coin150, Coin300, RemoveAds};

public class IAPController : MonoBehaviour
{

    
    // Add names here for the products that can be bought. Same as on the platforms
    private const string kProductIDRemoveAds = EasyMobile.EM_IAPConstants.Product_noads;

    // Add Events when Items have been bought
    public IntEvent OnCoinsBought;
    [SerializeField] private PlayerPrefIntVariable hasRemovedAds = null;

    void OnEnable()
    {
        InAppPurchasing.PurchaseCompleted += PurchaseCompletedHandler;
        InAppPurchasing.PurchaseFailed += PurchaseFailedHandler;
    }

    // Unsubscribe when the game object is disabled
    void OnDisable()
    {
        InAppPurchasing.PurchaseCompleted -= PurchaseCompletedHandler;
        InAppPurchasing.PurchaseFailed -= PurchaseFailedHandler;
    }

    // Successful purchase handler
    void PurchaseCompletedHandler(IAPProduct product)
    {
        // Compare product name to the generated name constants to determine which product was bought
        switch (product.Name)
        {
            case kProductIDRemoveAds:
                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", product.Name));
                hasRemovedAds.SetValue(1);
                break;
            default:
                break;
                // More products here...
        }
    }

    // Failed purchase handler
    void PurchaseFailedHandler(IAPProduct product)
    {
        Debug.Log("The purchase of product " + product.Name + " has failed.");
    }
}

