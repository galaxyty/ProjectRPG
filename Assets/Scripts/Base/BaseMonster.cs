using Cysharp.Threading.Tasks;
using UnityEngine;

// 몬스터 기본 클래스
public abstract class BaseMonster : MonoBehaviour, IHealth
{
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    /// <summary>
    /// 체력.
    /// </summary>
    public int Hp { get; private set; }    

    /// <summary>
    /// 몬스터 초기화.
    /// </summary>
    public abstract void Initialization();

    protected void Update()
    {
        // 레이어 Order.
        _spriteRenderer.sortingOrder = -(int)(transform.position.y * 100);
    }

    /// <summary>
    /// 체력 셋팅
    /// </summary>    
    public void SetHP(int hp)
    {
        Hp = hp;
    }

    /// <summary>
    /// 몬스터 타입.
    /// </summary>
    protected Enums.MonsterType _type;

    // 프로퍼티
    public Enums.MonsterType Type
    {
        get { return _type; }
        private set { }
    }

    /// <summary>
    /// 데미지 받음 (피격).
    /// </summary>    
    public void TakeDamage(int damage)
    {
        Debug.Log($"받은 데미지 : {damage}");

        Hp -= damage;

        OnHit();

        if (Hp <= 0)
        {
            OnDie();
        }
    }

    /// <summary>
    /// 피격 받을 시 호출.
    /// </summary>
    public abstract UniTask OnHit();

    /// <summary>
    /// 사망.
    /// </summary>
    public abstract void OnDie();
}