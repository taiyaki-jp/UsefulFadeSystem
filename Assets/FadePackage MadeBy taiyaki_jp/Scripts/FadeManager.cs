using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    [SerializeField, Header("フェード速度")] private float _fadeSpeed=1;
    private GameObject _fadeCanvas;
    private FadeAndLoad _load;

    System.Action BeforeAction=null;
    System.Action AfterAction=null;
    System.Action FinishAction = null;

    Coroutine _fadeCoroutine;

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
            _fadeCoroutine = StartCoroutine(FirstFade());
            Fade_Singleton.IsFirst = false;
        }
    }

    /// <summary>
    /// 最初のフェード
    /// </summary>
    private IEnumerator FirstFade()
    {
        //AfterAction.Invoke();
        yield return _load.FadeSystem<Enum>(-1,Color.black, Color.clear);
        //FinishAction.Invoke();

        _fadeCanvas.SetActive(false);
        Stop();
    }
    /// <summary>
    /// フェードを呼び出す関数
    /// </summary>
    /// <param name="sceneName">遷移先のシーンの名前</param>
    /// <param name="startOrigin">[省略可]FillOriginEnum.csのEnum 省略すると透明度フェード</param>
    /// <param name="endOrigin">[省略可]FillOriginEnum.csのEnum 省略すると透明度フェード</param>
    /// <param name="startColor">[省略可]フェード開始時の色　省略すると黒　透明度フェードなら透明</param>
    /// <param name="midColor">[省略可]画面が見えなくなった時の色　省略すると黒</param>
    /// <param name="midColor2">[省略可]画面が見えなくなったあと色をさらに変えたいときに使う</param>
    /// <param name="endColor">[省略可]フェード終了時の色　省略すると黒　透明度フェードなら透明</param>
    public void Fade<TOriginEnum>(string sceneName, TOriginEnum startOrigin = default, TOriginEnum endOrigin = default, Color startColor = default, Color midColor = default, Color midColor2 = default, Color endColor = default) where TOriginEnum : Enum
    {
        //defaultを変換
        if (startOrigin == null && startColor == default) startColor = Color.clear;
        else if (startColor == default) startColor = Color.black;

        if (midColor == default) midColor = Color.black;

        if (endOrigin == null && endColor == default) endColor = Color.clear;
        else if (endColor == default) endColor = Color.black;

        StartCoroutine(FadeCor<Enum>(sceneName,startOrigin,endOrigin,startColor,midColor,midColor2,endColor));
    }

    private IEnumerator FadeCor<TOriginEnum>(string sceneName,TOriginEnum startOrigin,TOriginEnum endOrigin,Color startColor,Color midColor,Color midColor2,Color endColor)where TOriginEnum : Enum
    {

        _fadeCanvas = Fade_Singleton.Canvas;
        _fadeCanvas.SetActive(true);
        Color finalMid = midColor;

        yield return _fadeCoroutine = StartCoroutine(_load.FadeSystem(+1, startColor, midColor, startOrigin));
        //BeforeAction.Invoke();
        Stop();
        if (midColor2 != default)
        {
            yield return new WaitForSeconds(1);
            yield return _fadeCoroutine = StartCoroutine(_load.FadeSystem<Enum>(-1, midColor, midColor2));
            Stop();
            yield return new WaitForSeconds(1);
            finalMid = midColor2;
        }

        yield return SceneManager.LoadSceneAsync(sceneName);
        //AfterAction.Invoke();

        yield return _fadeCoroutine = StartCoroutine(_load.FadeSystem(-1, finalMid, endColor, endOrigin));
        //FinishAction.Invoke();
        Stop();
        Debug.Log("a");

        _fadeCanvas.SetActive(false);
    }

    private void Stop()
    {
        StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = null;
    }
}
