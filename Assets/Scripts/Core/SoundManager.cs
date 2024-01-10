using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    public static AudioManager instance { get; private set; }
    [SerializeField] private AudioClip healthSound;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip breakRockSound;
    [SerializeField] private AudioClip badSound;
    [SerializeField] private AudioClip positiveSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip gameWonSound;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private Button sfxToggleButton;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        StartCoroutine(Subscribe());
        sfxToggleButton.onClick.AddListener(HandleSfxToggleButton);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
        sfxToggleButton.onClick.RemoveListener(HandleSfxToggleButton);
    }

    private void Start()
    {

        if (PlayerPrefs.GetInt("sfx", 1) == 0)
        {
            sfxSource.mute = true;
            sfxToggleButton.image.sprite = soundOffSprite;
        }
        else
        {
            sfxSource.mute = false;
            sfxToggleButton.image.sprite = soundOnSprite;
        }
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    PlayDieSound();
                    PlayGameOverSound();
                }
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    PlayGameWonSound();
                }
            })
            .AddTo(subscriptions);
    }

    public void PlayDieSound()
    {
        sfxSource.PlayOneShot(dieSound);
    }
    public void PlayRockSound()
    {
        sfxSource.PlayOneShot(breakRockSound);
    }
    public void PlayGemSound()
    {
        sfxSource.PlayOneShot(gemSound);
    }
    public void PlayHealthSound()
    {
        sfxSource.PlayOneShot(healthSound);
    }
    public void PlayGameOverSound()
    {
        sfxSource.PlayOneShot(gameOverSound);
    }
    public void PlayBadSound()
    {
        sfxSource.PlayOneShot(badSound);
    }
    public void PlayGameWonSound()
    {
        sfxSource.PlayOneShot(gameWonSound);
    }
    public void PlayPositiveSound()
    {
        sfxSource.PlayOneShot(positiveSound);
    }
    public void HandleSfxToggleButton()
    {
        if (sfxSource.mute)
        {
            sfxSource.mute = false;
            sfxToggleButton.image.sprite = soundOnSprite;
            PlayerPrefs.SetInt("sfx", 1);
        }
        else
        {
            sfxSource.mute = true;
            sfxToggleButton.image.sprite = soundOffSprite;
            PlayerPrefs.SetInt("sfx", 0);
        }
    }
}
