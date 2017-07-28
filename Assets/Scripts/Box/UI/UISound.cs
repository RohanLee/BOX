using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂载在按钮上的播放声音的组件
/// </summary>
[AddComponentMenu("BOX/UI/UI Sound")]
public class UISound : MonoBehaviour
{
    AudioSource audioSource;

    GameObject uiRoot;
    public AudioClip audioClip;
    public float volume = 1f, pitch = 1f;

    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    public void Start()
    {
        uiRoot = GameObject.Find("UIRoot");
    }

    void PlaySound()
    {
        audioSource = uiRoot.GetComponent<AudioSource>() ? uiRoot.GetComponent<AudioSource>() : uiRoot.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
