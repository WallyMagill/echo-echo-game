using System;
using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public GameObject portalBase;
    public GameObject portalDoor;
    public GameObject portalText;
    public Animator portalAnimator;

    public string nextSceneName;

    private bool isPlayerInBase = false;
    private bool isPortalActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (portalText != null) {portalText.SetActive(false);}
        if (portalDoor != null) {portalDoor.SetActive(false);}
    }

    // Update is called once per frame
    void Update()
    {
        // To end the level - player goes inactive, portal closes, scene changes
        if (isPlayerInBase && isPortalActive && Input.GetKeyDown(KeyCode.E))
        {
            if (nextSceneName != null)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    // Make text prompt appear on enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isPortalActive)
            {
                OpenPortal();
            }
            isPlayerInBase = true;
            ShowText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideText();
        }
    }

    // Open the portal and make text appear
    private void OpenPortal()
    {
        isPortalActive = true;
        portalDoor.SetActive(true);
    }

    private void ClosePortal()
    {
        portalAnimator.SetTrigger("Close");
    }

    private void ShowText()
    {
        portalText.SetActive(true);
    }

    private void HideText()
    {
        portalText.SetActive(false);
    }
}
