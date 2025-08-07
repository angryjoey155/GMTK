using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animator : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Image _image;
    [SerializeField] public List<Sprite> frames = new List<Sprite>(); 
    [SerializeField] float _SecondsBetweenFrames;
    [SerializeField] bool _isSprite = true;
    int _currentFrame = 0;
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = _SecondsBetweenFrames;
    }

    private void Update()
    {
        if (_isSprite)
            ChangeSprite();
        else
            ChangeUI();
    }

    private void ChangeUI()
    {
        if (_timer >= 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            if (_currentFrame < frames.Count - 1)
                _currentFrame++;
            else
                _currentFrame = 0;
            _timer = _SecondsBetweenFrames;
            _image.sprite = frames[_currentFrame];
        }
    }

    private void ChangeSprite()
    {
        if (frames == null)
        {
            _spriteRenderer.sprite = null;
            return;
        }
        if (_timer >= 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            if (_currentFrame < frames.Count - 1)
                _currentFrame++;
            else
                _currentFrame = 0;
            _timer = _SecondsBetweenFrames;
            _spriteRenderer.sprite = frames[_currentFrame];
        }
    }
    public void SetNewFrames(List<Sprite> newFrames)
    {
        frames = newFrames;
    }
}
