using System.Collections; // For IEnumerator and coroutines
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private HurtboxComponent hurtbox;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        if (hurtbox == null)
        {
            Debug.LogError("HurtboxComponent is not assigned.");
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = hurtbox.Health;
            healthSlider.value = hurtbox.Health;
        }
        else
        {
            Debug.LogError("Health Slider is not assigned.");
        }
    }

    private void Update()
    {
        if (hurtbox != null && healthSlider != null)
        {
            healthSlider.value = hurtbox.Health;
        }
    }
}
