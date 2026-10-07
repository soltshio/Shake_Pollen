using UnityEngine;

public class AnimTest : MonoBehaviour
{
    [SerializeField]
    Animator _animator;

    // Update is called once per frame
    void Update()
    {
        float blend = Mathf.Sin(Time.time*5);

        _animator.SetFloat("Blend", blend);
    }
}
