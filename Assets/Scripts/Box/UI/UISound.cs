using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂载在按钮上的播放声音的组件
/// </summary>
[AddComponentMenu("BOX/UI/UI Sound")]
public class UISound : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip audioClip;
    public float volume = 1f, pitch = 1f;

    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    public void Start()
    {
        
    }

    void PlaySound()
    {
        audioSource = UICore.UIRoot.GetComponent<AudioSource>() ? UICore.UIRoot.GetComponent<AudioSource>() : UICore.UIRoot.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
