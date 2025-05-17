using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField, Label("フェード速度")] private float fadeSpeed=1;
    private GameObject _fadeCanvas;
    private FadeAndLoad _load;

    System.Action BeforAction=null;
    System.Action AfterAction=null;
    System.Action FinishAction = null;

    private void Start()
    {
        _fadeCanvas = Fade_Singleton.Canvas;

        _load = new FadeAndLoad();
        _load.Image=Fade_Singleton.FadeImage;
        _load.Speed=fadeSpeed;

        if (Fade_Singleton.IsFirst)
        {
            _ = FirstFade();
            Fade_Singleton.IsFirst = false;
        }
    }

    /// <summary>
    /// 最初のフェード
    /// </summary>
    private async UniTask FirstFade()
    {
        //AfterAction.Invoke();
        _load.SetColor(Color.black);
        await _load.FadeOut();
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
    }



    /// <summary>
    /// FillAmountフェードを呼び出す関数
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <param name="startOrigin">FillOriginEnum.csのEnum</param>
    /// <param name="endOrigin">FillOriginEnum.csのEnum</param>
    /// <param name="color">[省略可]フェードの色　省略すると黒</param>
    public async UniTask Fade<TOriginEnum>(string sceneName,TOriginEnum startOrigin,TOriginEnum endOrigin,Color color=default)where TOriginEnum : Enum
    {
        _fadeCanvas = Fade_Singleton.Canvas;
        _fadeCanvas.SetActive(true);

        if(color==default)_load.SetColor(Color.black); 
            else _load.SetColor(color);

        await _load.FadeIn(startOrigin);
        //BeforAction.Invoke();

        await SceneManager.LoadSceneAsync(sceneName);
        //AfterAction.Invoke();

        await _load.FadeOut(endOrigin);
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
    }
    /// <summary>
    /// 透明度フェードを呼び出す関数
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <param name="color">どんな色でフェードするか</param>
    public async UniTask Fade(string sceneName,Color color)
    {
        _fadeCanvas = Fade_Singleton.Canvas;
        _fadeCanvas.SetActive(true);

        if (color == default) _load.SetColor(Color.black);
        else _load.SetColor(color);

        await _load.FadeIn();
        //BeforAction.Invoke();

        await SceneManager.LoadSceneAsync(sceneName);
        //AfterAction.Invoke();

        await _load.FadeOut();
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
    }
}
