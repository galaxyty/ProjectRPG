using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 플레이어 애니메이터.
    [SerializeField]
    private Animator _animator;

    // 리지드바디.
    [SerializeField]
    private Rigidbody _rigidbody;

    // 현재 이동 벡터.
    private Vector3 _currentMoveDirection = Vector3.zero;    

    // 현재 회전 쿼터니언.
    private Quaternion _currentRotate = Quaternion.identity;

    // 이동속도.
    private float _moveSpeed = 10.0f;

    // 회전속도.
    private float _rotateSpeed = 700.0f;

    private void FixedUpdate()
    {
        Move();
        Rotate();
        HillRoad();
    }

    // 속도.
    private void Move()
    {
        // 현재 속도 가져옴.
        Vector3 velocity = _rigidbody.linearVelocity;

        // 중력 영향 받지 않게 y는 기존 값으로 남기고.
        velocity.x = _currentMoveDirection.x * _moveSpeed;
        velocity.z = _currentMoveDirection.z * _moveSpeed;

        // 속도 적용.
        _rigidbody.linearVelocity = velocity;
    }

    // 회전.
    private void Rotate()
    {
        // 회전 보간.
        Quaternion lerp = Quaternion.RotateTowards(_rigidbody.rotation, _currentRotate, _rotateSpeed * Time.fixedDeltaTime);

        // 회전 적용.
        _rigidbody.MoveRotation(lerp);
    }

    // 언덕 부드럽게 이동.
    private void HillRoad()
    {
        RaycastHit groundHit;

        // 땅을 향해 레이캐스트를 쏘고 그 땅에 법선 벡터를 가져옴.
        bool isGrounded = Physics.Raycast(
            transform.position + Vector3.up * 0.1f,
            Vector3.down,
            out groundHit,
            0.5f
        );

        // 땅에 닿았는지 체크.
        if (isGrounded == false)
        {
            return;
        }
        
        // 캐릭터 벡터와 땅에 법선 벡터를 투영하여 경사 벡터를 구함.
        _currentMoveDirection = Vector3.ProjectOnPlane(_currentMoveDirection, groundHit.normal);

        Vector3 velocity = _rigidbody.linearVelocity;

        // y축을 경사 y 벡터로 사용.
        velocity.y = _currentMoveDirection.y * _moveSpeed;

        _rigidbody.linearVelocity = velocity;
    }

    // 키 입력.
    private void OnMove(InputValue value)
    {
        // 이동 좌표.
        Vector2 input = value.Get<Vector2>();

        // 이동 벡터 적용.
        _currentMoveDirection = new Vector3(input.x, 0, input.y);

        // 키 입력이 있을 경우에만 회전 적용.
        if (_currentMoveDirection.sqrMagnitude >= 0.001f)
        {                        
            // 입력 방향으로 Y축 회전 각도 계산
            float angle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;

            // 회전 쿼터니언 적용.
            _currentRotate = Quaternion.Euler(0, angle, 0);

            // 애니메이션 적용.
            _animator.SetInteger("Move", 1);            

            return;
        }

        _animator.SetInteger("Move", 0);
    }

    // 디버그용 기즈모.
    private void OnDrawGizmos()
    {
        // Z축.
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 2.0f);

        // X축.
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * 2.0f);

        // Y축.
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.up * 2.0f);

        // 지형 체크.
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * 0.5f);
    }
}
