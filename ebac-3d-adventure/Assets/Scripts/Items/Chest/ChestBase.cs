using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class ChestBase : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Controls")]
    public KeyCode openChestKey;

    [Header("Notification")]
    public GameObject notification;
    public float animationDuration;
    public Ease animationEase;
    private float startScale;

    [Space]
    public ChestItemBase chestItem;

    private bool _isChestOpened = false;
    private string triggerOpen = "Open";

    [Space]
    [Header("Sound")]
    public AudioSource chestAS;

    private void Start()
    {
        startScale = notification.transform.localScale.x;
    }

    private void Update()
    {
        if (Input.GetKeyDown(openChestKey) && notification.activeSelf)
        {
            OpenChest();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerBase player = other.transform.GetComponent<PlayerBase>();
        if (player != null)
        {
            ShowNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerBase player = other.transform.GetComponent<PlayerBase>();
        if (player != null)
        {
            HideNotification();
        }
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_isChestOpened) return;

        chestAS.Play();
        animator.SetTrigger(triggerOpen);
        _isChestOpened = true;
        HideNotification();
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }


    [NaughtyAttributes.Button]
    public void ShowNotification()
    {
        if (_isChestOpened) return;

        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, animationDuration);
    }

    [NaughtyAttributes.Button]
    public void HideNotification()
    {
        notification.transform.DOScale(0, animationDuration);
        notification.SetActive(false);
    }
}
