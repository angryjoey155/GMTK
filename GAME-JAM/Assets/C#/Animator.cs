using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _frames = new List<Sprite>();
    [SerializeField] float _SecondsBetweenFrames;
    int _currentFrame = 0;
    float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = _SecondsBetweenFrames;
    }

    private void Update()
    {
        if (_timer >= 0)
        {
            _timer -= Time.deltaTime;
        }
        else 
        {
            if (_currentFrame < _frames.Count-1)
                _currentFrame++;
            else
                _currentFrame = 0;
            _timer = _SecondsBetweenFrames;
            _spriteRenderer.sprite = _frames[_currentFrame];

        }
    }
}
