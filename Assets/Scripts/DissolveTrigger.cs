using UnityEngine;
using System.Collections;

public class DissolveTrigger : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private string dissolveTriggerName = "Dissolve";

    [Header("Timing")]
    [SerializeField] private float animationLength = 1.2f;

    [Header("Disable After")]
    [SerializeField] private GameObject objectToDisable;

    private bool hasStarted = false;


    private void OnTriggerEnter(Collider other)
    {
        if (hasStarted)
            return;

        if (other.CompareTag("Player") && !hasStarted)
        {
            hasStarted = true;
            animator.SetTrigger(dissolveTriggerName);
            StartCoroutine(DisableAfterAnimation());
        }
    }

    private IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(animationLength);
        objectToDisable.SetActive(false);
    }
}
