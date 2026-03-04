using UnityEngine;
using UnityEngine.UI;

public class ResetButtonAnimation : MonoBehaviour
{
    private Animator animator;
    private Button button;

    void Awake()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        // Animatorを一瞬止めて再有効化
        animator.enabled = false;
        animator.enabled = true;

        // 状態を強制的にNormalに戻す
        button.OnPointerExit(null); 
    }
}
