using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player _player;
    private Vector3 _offset;
    
    // Start is called before the first frame update
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _offset = _player.transform.position - transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        var pos = _player.transform.position - _offset;
        pos.x = Mathf.Clamp(pos.x, -6, 6);
        transform.position = pos;
    }
}
