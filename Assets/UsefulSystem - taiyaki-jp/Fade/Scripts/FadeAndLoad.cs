using Cysharp.Threading.Tasks;
using System;
using FadeOptions;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class FadeAndLoad
{
    private float _fadeSpeed=1;
    public float Speed
    {
        set => _fadeSpeed = value;
    }
    private Image _fadeImage;

    public Image Image
    {
        set => _fadeImage = value;
    }

    /// <summary>
    /// 各フェードを呼び出すUniTask
    /// </summary>
    /// <param name="token"></param>
    /// <param name="mode">FadeMode</param>
    /// <param name="startColor">開始時の色</param>
    /// <param name="endColor">終了時の色</param>
    /// <param name="origin">[省略可]FillOriginEnumのどれか　省略すると透明度フェード</param>
    /// <typeparam name="TOriginEnum"></typeparam>
    public async UniTask FadeSystem<TOriginEnum>(CancellationToken token,FadeMode mode,Color startColor,Color endColor,TOriginEnum origin = default) where TOriginEnum : Enum
    {

        var useColor = (startColor != endColor);//フェード中色を変えるか

        if (useColor==false)_fadeImage.color=startColor;
        var useOrigin = origin!=null;//キャンバスを動かしてフェードするか

        if (useOrigin)//ジェネリック変数をdefault(null)と比較
        {
            _fadeImage.fillMethod = AutoMethodSet(origin);
            _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
            if (mode == FadeMode.FadeIn)
                _fadeImage.fillAmount = 1;
            else
                _fadeImage.fillAmount = 0;
        }
        else
        {
            useOrigin = false;
            _fadeImage.fillAmount = 1;
        }

        var t = 0f;
        while (t<1)
        {
            //キャンセルが飛んでいればこのフェードを即時完了
            if (token.IsCancellationRequested)
            {
                Debug.Log("Cancellation requested");
                TaskCancel(mode,startColor,endColor);
                return;
            }
            t += _fadeSpeed * Time.deltaTime;
            if (useOrigin) Fade(mode, t);
            if (useColor)  Fade(t,startColor,endColor);
            await UniTask.Yield();
        }
    }

    private void TaskCancel(FadeMode mode, Color startColor, Color endColor)
    {
        Fade(mode, 1);
        Fade(1,startColor,endColor);
    }

    /// <summary>
    /// FillAmount式フェード
    /// </summary>
    /// <param name="mode">インかアウトか</param>
    /// <param name="t">経過時間</param>
    private void Fade(FadeMode mode,float t)
    {
        var fillAmount = 0f;
        if (mode == FadeMode.FadeIn)
        {
            fillAmount = 1 - t;
        }
        else if (mode == FadeMode.FadeOut)
        {
            fillAmount = t;
        }
        _fadeImage.fillAmount = fillAmount;
    }

    /// <summary>
    /// RGBAをいじる
    /// </summary>
    /// <param name="t">経過時間</param>
    /// <param name="startColor">開始時の色</param>
    /// <param name="endColor">終了時の色</param>
    private void Fade( float t, Color startColor, Color endColor)
    {
        _fadeImage.color = Color.Lerp(startColor, endColor, t);
    }


    /// <summary>
    /// 渡されたoriginに基づいてmethodを変える関数
    /// </summary>
    /// <typeparam name="TOriginEnum">FillOriginEnum.csの中のEnum</typeparam>
    /// <param name="origin">渡されたOriginEnum</param>
    private Image.FillMethod AutoMethodSet<TOriginEnum>(TOriginEnum origin)where TOriginEnum : Enum
    {
        switch (origin)
        {
            case Horizontal:
                return Image.FillMethod.Horizontal;
            case Vertical:
                return Image.FillMethod.Vertical;
            case Radial_90:
                return Image.FillMethod.Radial90;
            case Radial_180:
                return Image.FillMethod.Radial180;
            case Radial_360:
                return Image.FillMethod.Radial360;
        }
        return Image.FillMethod.Horizontal;
    }
}
