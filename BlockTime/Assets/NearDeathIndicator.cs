using UnityEngine;
using UnityEngine.UI;

public class NearDeathIndicator : MonoBehaviour
{
    public Image damageOverlay;  // Assign in Inspector
    [SerializeField] private HurtboxComponent hurtbox;
    public float fadeSpeed = 1.5f; // Speed of fading effect

    private Color overlayColor;
    private float originalA; 
    private float maxHealth;

    void Start()
    {
        if (hurtbox == null)
        {
            Debug.LogError("HurtboxComponent is not assigned.");
        }
        maxHealth = hurtbox.Health;

        overlayColor = damageOverlay.color;
        originalA = overlayColor.a;
        overlayColor.a = 0f; // Start fully transparent
        damageOverlay.color = overlayColor;
    }

    void Update()
    {
        float healthPercentage = hurtbox.Health / maxHealth;

        if (healthPercentage < 0.2f) // Below 20%
        {
            overlayColor.a = Mathf.Lerp(overlayColor.a, originalA, Time.deltaTime * fadeSpeed); // Fade in
        }
        else
        {
            overlayColor.a = Mathf.Lerp(overlayColor.a, 0f, Time.deltaTime * fadeSpeed); // Fade out
        }

        damageOverlay.color = overlayColor;
    }
}
