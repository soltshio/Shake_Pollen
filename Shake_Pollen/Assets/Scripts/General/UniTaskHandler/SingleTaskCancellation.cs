using System.Threading;

//作成者:杉山
//よく使う、タスクを必ず一つだけ動かすようにする処理のクラス

public class SingleTaskCancellation
{
    CancellationTokenSource _linkedCts;

    public CancellationToken CancelAndReCreateToken(CancellationToken parent)
    {
        _linkedCts?.Cancel();
        _linkedCts?.Dispose();

        _linkedCts = CancellationTokenSource.CreateLinkedTokenSource(parent);

        return _linkedCts.Token;
    }

    public void Cancel()
    {
        _linkedCts?.Cancel();
        _linkedCts?.Dispose();
        _linkedCts = null;
    }
}
