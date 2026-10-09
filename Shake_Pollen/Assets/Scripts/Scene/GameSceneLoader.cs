using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameSceneLoader : MonoBehaviour
{
    [Tooltip("フェードイン・アウトをするパネル")]
    [SerializeField]
    FadeInOutPanel _fadeInOutPanel;

    bool _isLoading = false;

    public void StartLoad(InputAction.CallbackContext context)
    {
        if(!context.performed) return;

        //既にロードが始まってたら弾く
        if (_isLoading) return;

        LoadSceneAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    async UniTask LoadSceneAsync(CancellationToken ct)
    {
        _isLoading = true;

        //フェードアウトをしきってから、シーンのロードを始める
        _fadeInOutPanel.FadeTrigger(FadeInOutPanel.FadeEType.FadeOut);

        await UniTask.WaitUntil(() => (_fadeInOutPanel.FadeState == FadeInOutEState.CompleteFadeOut), cancellationToken: ct);

        await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name).ToUniTask(cancellationToken: ct);

        _isLoading = false;
    }
}
