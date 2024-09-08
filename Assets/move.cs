using UnityEngine;

public class AutoAnimation : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        // Memulai animasi
        animator.Play("Armatur");
    }
}
