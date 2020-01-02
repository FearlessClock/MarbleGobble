using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class LifeCapsuleController : MonoBehaviour
{
    [SerializeField] private Color aliveColor = Color.green;
    [SerializeField] private Color deadColor = Color.red;
    [SerializeField] private Image image = null;
    [Range(0,1f)]
    [SerializeField] private float fadeAmount = 0;
    [SerializeField] private Animator animator = null;
    private void Update()
    {
        image.color = Color.Lerp(aliveColor, deadColor, fadeAmount);
    }

    public void Kill()
    {
        animator.SetTrigger("Die");
    }
}
