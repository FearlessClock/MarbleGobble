using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Image))]
public class BoolButtonColorChanger : MonoBehaviour
{
    [SerializeField] private bool useSprites = false;
    [SerializeField] private PlayerPrefIntVariable isTrue = null;

    [SerializeField] private Color trueColor = Color.green;
    [SerializeField] private Color falseColor = Color.red;

    [SerializeField] private Sprite trueSprite = null;
    [SerializeField] private Sprite falseSprite = null;

    [SerializeField] private Image imageRenderer;

    public void OnClick()
    {
        isTrue.SetValue(isTrue.value == 0? 1:0);
        SetVisualState();
    }

    private void SetVisualState()
    {
        if (useSprites)
        {
            if (isTrue)
            {
                imageRenderer.sprite = trueSprite;
            }
            else
            {
                imageRenderer.sprite = falseSprite;
            }
        }
        else
        {
            if (isTrue.value == 1)
            {
                imageRenderer.color = trueColor;
            }
            else
            {
                imageRenderer.color = falseColor;
            }
        }
    }

    private void OnEnable()
    {
        SetVisualState();
    }
    private void OnDisable()
    {
        SetVisualState();
    }

    private void Update()
    {
        //SetVisualState();
    }
}
