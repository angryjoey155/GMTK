using UnityEngine;

public class Resourses : MonoBehaviour
{
    [SerializeField] private UnityEngine.Animator _animator;
    public void ChangeAnim(string animName)
    {
        _animator.Play(animName);
    }
}
