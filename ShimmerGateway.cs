using UnityEngine;
using UnityEngine.SceneManagement;

public class ShimmerGateway : MonoBehaviour
{
    public string shimmerSceneName = "Scene_ShimmerCore";
    public float hoverTimeRequired = 3f;
    public GameObject visualEffect;
    public GameObject messagePanel;
    public TMPro.TextMeshProUGUI messageText;
    public AudioClip activationSound;

    private float hoverTimer = 0f;
    private bool isHovering = false;
    private bool isActivated = false;
    private AudioSource audioSource;

    void Start()
    {
        if (visualEffect != null)
            visualEffect.SetActive(false);

        if (messagePanel != null)
            messagePanel.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (isHovering && !isActivated)
        {
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= hoverTimeRequired)
            {
                ActivatePortal();
            }
        }
    }

    private void ActivatePortal()
    {
        isActivated = true;
        if (visualEffect != null)
            visualEffect.SetActive(true);

        if (messagePanel != null)
        {
            messageText.text = "You are about to step through.\n\nSaying YES means you are ready to be seen, gently.\nYou can leave at any time by waiting at the Dock for the next Departure.\n\n(You may encounter 42 meanings before truth becomes visible.)";
            messagePanel.SetActive(true);
        }

        if (activationSound != null)
        {
            audioSource.PlayOneShot(activationSound);
        }

        // Automatically continue after a short delay
        Invoke("LoadShimmerScene", 6f);
    }

    private void LogEntry()
    {
        string log = $"Shimmer entered at: {System.DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        Debug.Log(log);

        PlayerPrefs.SetString("LastShimmerEntry", log);  // Stores for local session data

        // Optional file logging (if needed in the future)
        // System.IO.File.AppendAllText(Application.persistentDataPath + "/shimmerlog.txt", log + "\n");
    }

    private void LoadShimmerScene()
    {
        LogEntry();
        SceneManager.LoadScene(shimmerSceneName);
    }

    private void OnMouseEnter()
    {
        isHovering = true;
    }

    private void OnMouseExit()
    {
        isHovering = false;
        hoverTimer = 0f;

        if (!isActivated && visualEffect != null)
            visualEffect.SetActive(false);
    }
}


