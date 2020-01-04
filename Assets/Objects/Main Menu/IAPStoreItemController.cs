using EasyMobile;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IAPStoreItemController : MonoBehaviour
{
    private IAPController iAPController = null;
    public IAPStoreBlockInformation itemInformation = null;
    [SerializeField] private eItemToBuy itemToBuy = eItemToBuy.Coin50;
    private string itemToBuyString = "";
    [Space]
    [SerializeField] private Image itemSpriteRenderer = null;
    [SerializeField] private TextMeshProUGUI realMoneyPrice = null;
    [SerializeField] private TextMeshProUGUI amountOfCoins = null;

    public UnityEvent OnPurchaseSuccesful;
    public UnityEvent OnPurchaseFailure;


    private void Awake()
    {
        if(itemSpriteRenderer != null)
            itemSpriteRenderer.sprite = itemInformation.itemSprite;
        realMoneyPrice?.SetText(itemInformation.price);
        amountOfCoins?.SetText(itemInformation.amount.ToString());
        itemToBuy = itemInformation.itemToBuy;
        switch (itemToBuy)
        {
            case eItemToBuy.RemoveAds:
                itemToBuyString = EM_IAPConstants.Product_NoAds;
                break;
            default:
                break;
        }
        iAPController = FindObjectOfType<IAPController>();
        if (!iAPController)
        {
            Debug.LogError("Can't find the IAPController");
        }
    }
    public void OnClick()
    {
        Debug.Log("Clicked");
        InAppPurchasing.Purchase(itemToBuyString);
    }


    // Successful purchase handler
    void PurchaseCompletedHandler(IAPProduct product)
    {

        Invoke("DelayDeactivation", 0.2f);
    }

    private void DelayDeactivation()
    {
        OnPurchaseSuccesful?.Invoke();
    }

    // Failed purchase handler
    void PurchaseFailedHandler(IAPProduct product)
    {
        Debug.Log("Failed");
        OnPurchaseFailure?.Invoke();
    }
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
}
