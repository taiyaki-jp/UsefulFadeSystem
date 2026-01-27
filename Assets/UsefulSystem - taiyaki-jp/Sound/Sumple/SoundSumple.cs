using UnityEngine;
using UnityEngine.UI;

public class SoundSumple : MonoBehaviour
{
    [SerializeField]private Button _bgm1PlayButton;
    [SerializeField]private Button _bgm2PlayButton;
    [SerializeField]private Button _bgmFadeChangeButton;
    [SerializeField]private Button _sePlayButton;
    [SerializeField]private Button _jinglePlayButton;
    [SerializeField]private Button _jingleWithFadeButton;

    private int _nowBGM = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        //これで初期化をする ※必須
        _ = SoundManager.Instance.Init();

        _bgm1PlayButton.onClick.AddListener(BGM1Play);
        _bgm2PlayButton.onClick.AddListener(BGM2Play);
        _bgmFadeChangeButton.onClick.AddListener(BGMFadeChange);
        _sePlayButton.onClick.AddListener(()=> SoundManager.Instance.PlaySE(SETypeEnum.Pumch));
        _jinglePlayButton.onClick.AddListener(() => _ = SoundManager.Instance.PlayJingle(JingleTypeEnum.SumpleJingle1,false));
        _jingleWithFadeButton.onClick.AddListener(() => _ = SoundManager.Instance.PlayJingle(JingleTypeEnum.SumpleJingle1,true));
    }

    private void BGM1Play()
    {
        _ = SoundManager.Instance.PlayBGM(BGMTypeEnum.幻想的で勇ましいオーケストラ);
        _nowBGM = 1;
    }

    private void BGM2Play()
    {
        _ = SoundManager.Instance.PlayBGM(BGMTypeEnum.人類未踏の秘境);
        _nowBGM = 2;
    }

    private void BGMFadeChange()
    {
        switch (_nowBGM)
        {
            case 1:
                _ = SoundManager.Instance.PlayBGM(BGMTypeEnum.人類未踏の秘境,true);
                _nowBGM = 2;
                break;
            case 2:
                _ = SoundManager.Instance.PlayBGM(BGMTypeEnum.幻想的で勇ましいオーケストラ,true);
                _nowBGM = 1;
                break;
        }
    }

    private void OnDestroy()
    {
        _bgm1PlayButton.onClick.RemoveAllListeners();
        _bgm2PlayButton.onClick.RemoveAllListeners();
        _bgmFadeChangeButton.onClick.RemoveAllListeners();
        _sePlayButton.onClick.RemoveAllListeners();
        _jinglePlayButton.onClick.RemoveAllListeners();
        _jingleWithFadeButton.onClick.RemoveAllListeners();
    }
}
