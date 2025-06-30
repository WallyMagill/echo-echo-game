using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PortalPrompt : MonoBehaviour
{
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private TextMeshProUGUI portalText;

    private bool canLeave = false;

    // Start is called before the first frame update
    void Start()
    {
        if (portalText != null)
        {
            Color clearColor = portalText.color;
            clearColor.a = 0f; // Set alpha to 0
            portalText.color = clearColor;
        }

        StartCoroutine(ShowTextAfterDelay());

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canLeave)
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    // Coroutine to wait and then reveal the text
    private IEnumerator ShowTextAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);

        if (portalText != null)
        {
            // Make text fully visible
            Color visibleColor = portalText.color;
            visibleColor.a = 1f; // Set alpha to 1 (fully visible)
            portalText.color = visibleColor;
            canLeave = true;
        }
    }

}