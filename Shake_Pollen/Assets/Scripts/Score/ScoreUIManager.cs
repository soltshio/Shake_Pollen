using TMPro;
using UnityEngine;

//スコアのUI

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _pointText;

    [SerializeField]
    ShakePtManager _shakePtManager;

    void OnEnable()
    {
        _shakePtManager.OnPointChanged += UpdatePointText;
    }

    private void OnDisable()
    {
        _shakePtManager.OnPointChanged -= UpdatePointText;
    }

    void UpdatePointText(float point)
    {
        _pointText.text = point.ToString("0")+"kg";
    }
}
