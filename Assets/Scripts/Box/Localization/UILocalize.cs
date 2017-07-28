using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂载在Text上的本地化组件
/// </summary>
[ExecuteInEditMode]
[AddComponentMenu("BOX/UI/UI Localize")]
public class UILocalize : MonoBehaviour
{
    public string key;

	void Start ()
	{
		OnLocalize();
	}

	/// <summary>
    /// Text显示本地化文字的方法
    /// </summary>
	void OnLocalize ()
	{
#if UNITY_EDITOR //这里是为了防止在非运行状态下 预制体拖入场景时报错
        if (!Application.isPlaying) return;
#endif
        GetComponent<Text>().text = string.IsNullOrEmpty(key) ? GetComponent<Text>().text : Localization.Get(key);
    }
}
