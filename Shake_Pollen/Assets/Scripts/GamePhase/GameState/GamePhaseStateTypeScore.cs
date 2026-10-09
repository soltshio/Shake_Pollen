using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//スコア表示

public class GamePhaseStateTypeScore : GamePhaseStateTypeBase
{
    [SerializeField]
    float _waitDurationControllable = 0.7f;

    [SerializeField]
    Canvas _scoreCanvas;

    [SerializeField]
    TextMeshProUGUI _scoreText;

    [SerializeField]
    AudioSource _seAudioSource;

    [SerializeField]
    AudioClip _announcementSE;

    [SerializeField]
    AudioSource _demoBGMAudioSource;

    [SerializeField]
    ShakePtManager _shakePt;

    [SerializeField]
    TextMeshProUGUI _titleInstructText;

    [SerializeField]
    PlayerInput _playerInput;

    [SerializeField]
    HighResultManager _highResultManager;

    [Header("skybox関係")]

    [SerializeField]
    Material _skyMat;

    [SerializeField]
    Color _pollenSkyColor;

    private static readonly int SkyTintID = Shader.PropertyToID("_Tint");

    [Header("花粉のパーティクル")]

    [SerializeField]
    ParticleSystem _resultPellenEffect;

    void Start()
    {
        _titleInstructText.enabled = false;
    }

    public override void OnEnter(GamePhaseStateMachine stateMachine)
    {
        //結果発表SEを鳴らす
        _seAudioSource.PlayOneShot(_announcementSE);

        //スコア表示
        _scoreCanvas.enabled = true;
        _scoreText.text = _shakePt.Point.ToString("0") + "kg";

        //空を花粉色に変える
        _skyMat.SetColor(SkyTintID, _pollenSkyColor);

        //花粉のパーティクルを出す
        _resultPellenEffect.Play();

        //高いスコアかどうかをチェック(高いスコアであれば追加の演出)
        _highResultManager.CheckHighScore(_shakePt.Point);

        FinishAnnounceScoreAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    public override void OnUpdate(GamePhaseStateMachine stateMachine)
    {

    }

    public override void OnExit(GamePhaseStateMachine stateMachine)
    {

    }

    async UniTask FinishAnnounceScoreAsync(CancellationToken ct)
    {
        //少し遅らせる
        await UniTask.Delay(System.TimeSpan.FromSeconds(_waitDurationControllable), cancellationToken: ct);

        //デモBGMを流し始める
        _demoBGMAudioSource.Play();

        //タイトルシーンに戻す操作を可能にする
        _titleInstructText.enabled = true;
        _playerInput.SwitchCurrentActionMap(ActionMapNameList.finish);
    }
}
