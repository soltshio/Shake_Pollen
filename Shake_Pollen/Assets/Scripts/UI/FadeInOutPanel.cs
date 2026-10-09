using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

//作成者:杉山
//フェードイン・アウトするパネル
//フェードイン...徐々に映像がはっきりと見えてくる手法
//フェードアウト...徐々に映像が見えなくなる手法

[RequireComponent(typeof(Image))]
public class FadeInOutPanel : MonoBehaviour
{
    public enum FadeEType
    {
        FadeIn,//フェードイン
        FadeOut//フェードアウト
    }


    [Tooltip("シーン開始時点でパネルで隠しておくか")] [SerializeField] 
    bool _isInitHide = false;

    [Tooltip("フェードイン・アウトにかける時間")] [SerializeField] 
    float _fadeInOutDuration = 1f;

    [Tooltip("隠す時に使うパネル")] [SerializeField]
    Image _myPanelImage;

    CancellationTokenSource _cts;

    FadeInOutEState _fadeState;

    public event Action <FadeInOutEState> OnChangeState;

    public FadeInOutEState FadeState
    {
        get { return _fadeState; }
        private set
        {
            if (_fadeState == value) return;

            _fadeState = value;
            OnChangeState?.Invoke(_fadeState);
        }
    }

    public float FadeInOutDuration
    {
        get { return _fadeInOutDuration; }
        set { _fadeInOutDuration = value; }
    }

    //フェードイン・アウトを開始する
    //引数isFadeInがtrueのときはフェードイン、falseのときはフェードアウト
    public void FadeTrigger(FadeEType fadeType)
    {
        //既に完了していた場合は弾く
        if(FadeState == FadeInOutEState.CompleteFadeIn && fadeType == FadeEType.FadeIn) return;//フェードインが完了しているのにフェードインをしようとしたとき

        if (FadeState == FadeInOutEState.CompleteFadeOut && fadeType == FadeEType.FadeOut) return;//フェードアウトが完了しているのにフェードアウトをしようとしたとき

        //フェードイン・アウトの最中であれば今行っているフェード処理を中断して新しくフェード処理を開始する
        if (FadeState == FadeInOutEState.FadingIn || FadeState == FadeInOutEState.FadingOut)
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        //トークンを生成
        _cts = new CancellationTokenSource();

        //フェード処理開始
        FadeAsync(fadeType, _cts.Token).Forget();
    }

    void Awake()
    {
        //イベントを発行する必要がないため、変数を直接書き換える
        _fadeState = _isInitHide ? FadeInOutEState.CompleteFadeOut : FadeInOutEState.CompleteFadeIn;

        //開始時にパネルの色の透明度をあらかじめ変えておく
        Color currentMyPanelColor = _myPanelImage.color;
        currentMyPanelColor.a = _isInitHide ? 1f : 0f;
        _myPanelImage.color = currentMyPanelColor;

        //さらにパネルのアクティブ状態も変更(パネルの当たり判定のため)
        _myPanelImage.enabled = _isInitHide;
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    //フェード処理
    async UniTask FadeAsync(FadeEType fadeType, CancellationToken ct)
    {
        //トークンの生成
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct, this.GetCancellationTokenOnDestroy());

        try
        {
            bool isFadeIn = (fadeType == FadeEType.FadeIn);

            //フェード状態の更新
            FadeState = isFadeIn ? FadeInOutEState.FadingIn : FadeInOutEState.FadingOut;

            //パネルをアクティブに
            if(!_myPanelImage.enabled) _myPanelImage.enabled = true;

            //透明度の更新に必要な変数の準備
            float targetAlpha = isFadeIn ? 0f : 1f;
            float beforeAlpha = _myPanelImage.color.a;
            float alphaDeltaFromCurrentToTarget = Mathf.Abs(targetAlpha - beforeAlpha);

            float elapsedTime = 0f;
            float fadeDuration = _fadeInOutDuration * alphaDeltaFromCurrentToTarget;//現在の透明度から目標の透明度までの差に応じてフェードにかける時間を変える

            while (elapsedTime < fadeDuration)
            {
                //透明度変更
                float newAlpha = Mathf.Lerp(beforeAlpha, targetAlpha, elapsedTime / fadeDuration);
                SetPanelAlpha(newAlpha);

                await UniTask.Yield(PlayerLoopTiming.Update, linkedCts.Token);

                elapsedTime += Time.deltaTime;
            }

            //目標の透明度にする
            SetPanelAlpha(targetAlpha);

            //フェード状態の更新
            FadeState = isFadeIn ? FadeInOutEState.CompleteFadeIn : FadeInOutEState.CompleteFadeOut;

            //フェードアウトが完了したならパネルを非アクティブにする(パネルの当たり判定を無くすため)
            if (isFadeIn) _myPanelImage.enabled = false;
        }
        catch(OperationCanceledException)
        {
            Debug.Log("フェードが中断されました");
        }
    }

    void SetPanelAlpha(float alpha)
    {
        Color currentMyPanelColor = _myPanelImage.color;
        currentMyPanelColor.a = alpha;
        _myPanelImage.color = currentMyPanelColor;
    }
}
