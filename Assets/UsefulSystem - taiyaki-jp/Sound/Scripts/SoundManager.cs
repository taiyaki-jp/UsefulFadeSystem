using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UsefulSystem.Common;

public class SoundManager : SingletonBase<SoundManager>
{
#region Singleton
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("NewSoundManager");
                SetInstance(go.AddComponent<SoundManager>());
                DontDestroyOnLoad(go);
            }
            return GetInstance();
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
#endregion


    //SEを同時に何個鳴らすか
    private int _seMaxCount;

    /// <summary>
    /// 初期化してサウンドのデータをロードします
    /// </summary>
    /// <param name="seMaxCount">SEを同時に鳴らす最大数　初期値は15</param>
    public async UniTask Init(int seMaxCount = 15)
    {
        _seMaxCount = seMaxCount;
        await LoadBGMData();
        await LoadSEData();
    }

#region BGM

    private Dictionary<string, BGMData> _bgmDatas;
    private AudioSource _bgmAudioSource;
    private bool _noBGM = true;

    /// <summary>
    /// ScriptableからBGMデータを読み込み
    /// </summary>
    private async UniTask LoadBGMData()
    {
        _bgmDatas = new();
        var scriptableBGM = await Resources.LoadAsync("BGMData") as ScriptableBGMDatas;
        if(scriptableBGM == null)//BGMのデータがそもそもない場合
        {
            Debug.LogWarning($"BGMのデータが見つかりませんでした");
            _noBGM = true;
            return;
        }

        foreach (var bgmData in scriptableBGM.bgmDatas)//データの読み込み
        {
            if (!_bgmDatas.TryAdd(bgmData.bgmName, bgmData))//名前が重複している場合
            {
                Debug.LogWarning($"BGMの名前が重複しています:{bgmData.bgmName}");
            }
        }

        if (_bgmDatas.Count == 0)//データはあったけど空データだった場合
        {
            Debug.LogWarning($"BGMのデータが設定されていません");
            _noBGM = true;
            return;
        }
        Debug.Log($"{_bgmDatas.Count}個のBGMをロードしました");
        _noBGM = false;
    }
    /// <summary>
    /// 今再生中のBGMの名前を返す
    /// </summary>
    /// <returns>今再生中のBGMの名前</returns>
    private string GetNowBGMClipName()
    {
        if(_bgmAudioSource == null)
        {
           Debug.LogWarning("BGMが一度も再生されたことがありません！");
            return "error-NoPlay";
        }

        if (_bgmAudioSource.clip == null)
        {
            Debug.Log("再生中のBGMがありませんでした");
        }
        return _bgmAudioSource.clip.name;
    }

    /// <summary>
    /// BGMを再生します。再生中のものがあった場合、同じならそのまま、違うなら引数に基づいて止めます
    /// </summary>
    /// <param name="bgmType">画面上のtools内から生成されるBGMTypeEnum(BGMの名前になります)</param>
    /// <param name="fadeChange">[省略可]BGMの音量を徐々に小さくしながら元のBGMを消すか、default=false</param>
    /// <param name="fadeTime">[省略可]fadeChangeがtrueの場合、どれくらいの時間をかけて元のBGMを消すか。default=1.0f</param>
    public void PlayBGM(BGMTypeEnum bgmType,bool fadeChange=false,float fadeTime=1.0f)
    {
        if (_noBGM) return;
        if (_bgmAudioSource == null)
        {
            _bgmAudioSource = new GameObject("BGM").AddComponent<AudioSource>();
            _bgmAudioSource.loop = true;
            _bgmAudioSource.gameObject.transform.SetParent(this.transform);
        }

        var key = bgmType.ToString();
        // ReSharper disable once PossibleNullReferenceException
        // LoadBGMDataで_bgmDatasが埋まる、埋まらなかったら_noBGMがtureになりreturn
        var getBGM = _bgmDatas.TryGetValue(key,out BGMData bgm);
        if (getBGM)
        {
            //指定されたBGMが今流れているものと同じなら処理をスキップ
            if (bgm.audioClip.name == GetNowBGMClipName()) return;

            _ = StopBGM(fadeChange, fadeTime);
            _bgmAudioSource.clip = bgm.audioClip;
            _bgmAudioSource.volume = bgm.volume;
            _bgmAudioSource.Play();
            Debug.Log($"♪~ {bgm.bgmName}");
        }
        else
        {
            Debug.LogWarning("指定されたBGMが見つかりませんでした\n一時的に一つ前のBGMを再生し続けます");
        }
    }

    /// <summary>
    /// BGMを止めます
    /// </summary>
    /// <param name="fadeChange">[省略可]BGMの音量を徐々に小さくしながらBGMを止めるか、default=false</param>
    /// <param name="fadeTime">[省略可]fadeChangeがtrueの場合、どれくらいの時間をかけて元のBGMを消すか。default=1.0f</param>
    public async UniTaskVoid StopBGM(bool fadeChange=false,float fadeTime=1.0f)
    {
        if (fadeChange)
        {
            var t = 0.0f;
            var sourceVolume = _bgmAudioSource.volume;
            while (t < fadeTime)
            {
                _bgmAudioSource.volume = Mathf.Lerp(sourceVolume, 0, t/fadeTime);
                t += Time.deltaTime;
                await UniTask.Yield();
            }
        }
        _bgmAudioSource.Stop();
    }

#endregion

#region SE

    private Dictionary<string, SEData> _seDatas;
    private List<AudioSource> _seSourcePool;
    private bool _noSE = true;

    /// <summary>
    /// SEの読み込み
    /// </summary>
    private async UniTask LoadSEData()
    {
        _seDatas = new();
        var scriptableSE = await Resources.LoadAsync("SEData") as ScriptableSEDatas;
        if(scriptableSE == null)
        {
            Debug.LogWarning($"SEのデータが見つかりませんでした");
            _noSE = true;
            return;
        }
        foreach (var seData in scriptableSE.seDatas)
        {
            if (!_seDatas.TryAdd(seData.seName, seData))
            {
                Debug.LogWarning($"SEの名前が重複しています:{seData.seName}");
            }
        }
        if(_seDatas.Count == 0)//データはあったけど空データだった場合
        {
            Debug.LogWarning($"SEのデータが設定されていません");
            _noSE = true;
            return;
        }
        Debug.Log($"{_seDatas.Count}個のSEをロードしました");
        _noSE = false;
    }

    /// <summary>
    /// SEを再生します
    /// </summary>
    /// <param name="key">画面上のtools内から生成されるSETypeEnum(SEの名前になります)</param>
    public void PlaySE(SETypeEnum key)
    {
        if (_noSE) return;
        //上でデータ存在するか確認してから生成
        if(_seSourcePool == null)
        {
            _seSourcePool = new();
            var seGoRoot = new GameObject("SEPool");
            for (var i = 0; i < _seMaxCount; i++)
            {
                var tempGo = new GameObject($"SE No.{i+1}").AddComponent<AudioSource>();
                //tempGo.outputAudioMixerGroup = _seAudioMixer;
                tempGo.gameObject.transform.SetParent(seGoRoot.transform);
                _seSourcePool.Add(tempGo);
            }
        }
        // ReSharper disable once PossibleNullReferenceException
        // LoadSEDataで_seDatasが埋まる、埋まらなかったら_noSEがtureになりreturn
        var getSE = _seDatas.TryGetValue(key.ToString(), out SEData se);
        if (getSE)
        {
            AudioSource thisSeSource = _seSourcePool.Find(s => s.isPlaying == false);
            if (thisSeSource != null)
            {
                Debug.LogWarning("SEの同時再生数上限に到達しました");
                return;
            }
            thisSeSource.clip = se.audioClip;
            thisSeSource.Play();
            Debug.Log($"SE再生:{se.seName}");
        }
        else
        {
            Debug.LogWarning("指定された名前のSEはありませんでした");
        }
    }

#endregion

#region Jingle

private Dictionary<string, JingleData> _jingleDatas;
private AudioSource _jingleSource;
private bool _noJingle = true;

#endregion
}