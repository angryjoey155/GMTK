using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resourses : MonoBehaviour
{
    [SerializeField] private UnityEngine.Animator _animator;
    public void ChangeAnim(string animName)
    {
        _animator.Play(animName);
    }
}
