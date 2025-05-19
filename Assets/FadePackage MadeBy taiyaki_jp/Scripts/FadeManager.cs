using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField, Label("フェード速度")] private float _fadeSpeed=1;
    private GameObject _fadeCanvas;
    private FadeAndLoad _load;

    System.Action BeforeAction=null;
    System.Action AfterAction=null;
    System.Action FinishAction = null;

    private void Start()
    {
        _fadeCanvas = Fade_Singleton.Canvas;

        _load = new FadeAndLoad
        {
            Image = Fade_Singleton.FadeImage,
            Speed = _fadeSpeed
        };

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
        await _load.FadeSystem<Enum>(-1,Color.black, Color.clear);
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
    }


    /// <summary>
    /// FillAmountフェードを呼び出す関数
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <param name="startOrigin">FillOriginEnum.csのEnum</param>
    /// <param name="endOrigin">FillOriginEnum.csのEnum</param>
    /// <param name="startColor">[省略可]フェード開始時の色　省略すると黒</param>
    /// <param name="midColor">[省略可]画面が見えなくなった時の色　省略すると黒</param>
    /// <param name="midColor2">[省略可]画面が見えなくなったあと色をさらに変えたいときに使う</param>
    /// <param name="endColor">[省略可]フェード終了時の色　省略すると黒</param>
    public async UniTask Fade<TOriginEnum>(string sceneName,TOriginEnum startOrigin,TOriginEnum endOrigin,Color startColor=default,Color midColor=default,Color midColor2=default,Color endColor=default)where TOriginEnum : Enum
    {
        //defaultを黒に
        if(startColor==default)startColor=Color.black;
        if(midColor==default)midColor=Color.black;
        if(endColor==default)endColor=Color.black;

        _fadeCanvas = Fade_Singleton.Canvas;
        _fadeCanvas.SetActive(true);
        Color finalMid = midColor;

        await _load.FadeSystem(+1,startColor,midColor,startOrigin);
        //BeforeAction.Invoke();
        if (midColor2 != default)
        {
            await _load.FadeSystem<Enum>(-1, midColor, midColor2);
            finalMid = midColor2;
        }

        await SceneManager.LoadSceneAsync(sceneName);
        //AfterAction.Invoke();

        await _load.FadeSystem(-1,finalMid,endColor,endOrigin);
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
    }

    /// <summary>
    /// 透明度フェードを呼び出す関数
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <param name="startColor">[省略可]フェードの色　省略すると透明</param>
    /// <param name="midColor">[省略可]フェードの色　省略すると黒</param>
    /// <param name="midColor2">[省略可]画面が見えなくなったあと色をさらに変えたいときに使う</param>
    /// <param name="endColor">[省略可]フェードの色　省略すると透明</param>
    public async UniTask Fade(string sceneName,Color startColor=default,Color midColor=default,Color midColor2=default,Color endColor=default)
    {
        //defaultを変換
        if(startColor==default)startColor=Color.clear;
        if(midColor==default)midColor=Color.black;
        if(endColor==default)endColor=Color.clear;

        _fadeCanvas = Fade_Singleton.Canvas;
        _fadeCanvas.SetActive(true);
        Color finalMid = midColor;

        await _load.FadeSystem<Enum>(+1,startColor,midColor);
        //BeforeAction.Invoke();
        if (midColor2 != default)
        {
            await _load.FadeSystem<Enum>(-1, midColor, midColor2);
            finalMid = midColor2;
        }

        await SceneManager.LoadSceneAsync(sceneName);
        //AfterAction.Invoke();

        await _load.FadeSystem<Enum>(-1, finalMid,endColor);
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
    }
}
