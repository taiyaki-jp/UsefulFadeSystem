using UnityEngine;
using UnityEngine.UI;

public class Fade_Singleton : MonoBehaviour
{
    private static Fade_Singleton _instanceClosed;

    private static Image _image;
    private static GameObject _canvas;

    private static bool _isFirst;

    private void Awake()
    {
        //もしすでに生成されていれば
        if (_instanceClosed != null && _instanceClosed != this)
        {
            Destroy(this.gameObject);//自身を削除

        }
        else//これがないとDestroyしたあと初期化され直す
        {

            //staticに自身を入れる
            _instanceClosed = this;
            DontDestroyOnLoad(this.gameObject);//それをシーンを跨ぐ様にする

            //↓初期化処理
            _image = GetComponentInChildren<Image>();
            _canvas = this.gameObject;
            _isFirst = true;
        }
    }
    private Fade_Singleton() { }//外部からの生成をブロック

    public static Image FadeImage
    {
        get { return _image; }
    }

    public static GameObject Canvas
    {
        get { return _canvas; }
    }

    public static bool IsFirst
    {
        get { return _isFirst; }
        set { _isFirst = value; }
    }
}
