using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    [SerializeField, Min(0f)] private float _lerpRatio;

    [SerializeField, Min(0f)] private float _decelerationRatio;
    [SerializeField] private float _decelerationDistance;

    private Transform _target;

    [Inject]
    private void Construct(Player player)
    {
        _target = player.transform;
    }

    private void LateUpdate()
    {
        if (_target == null) return;
        var newXposition = Mathf.Lerp(transform.position.x, _target.position.x, LerpRatio * Time.deltaTime);
        if ((BorderReached(Direction.left) && transform.position.x > _target.position.x) ||
            (BorderReached(Direction.right) && transform.position.x < _target.position.x)) return;
        transform.position = new Vector3(newXposition, transform.position.y, transform.position.z);
    }

    private bool BorderReached(Direction direction)
    {
        float cameraBorder = direction == Direction.left ? Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f)).x :
                                                           Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f)).x;
        if ((cameraBorder < _leftBorder.position.x && direction == Direction.left) ||
            (cameraBorder > _rightBorder.position.x && direction == Direction.right)) return true;

        return false;
    }

    private float LerpRatio
    {
        get
        {
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f));

            if (leftBorder.x >= _leftBorder.position.x + _decelerationDistance &&
                rightBorder.x <= _rightBorder.position.x - _decelerationDistance) return _lerpRatio;
            return 0f;
        }        
    }
    private enum Direction
    {
        left,
        right
    }
}