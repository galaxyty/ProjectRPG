using UnityEngine;

// 몬스터 기본 클래스
public abstract class BaseMonster : MonoBehaviour, IHealth
{
    /// <summary>
    /// 체력.
    /// </summary>
    public int Hp { get; private set; }

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

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
    public virtual void TakeDamage(int damage)
    {
        Debug.Log($"받은 데미지 : {damage}");

        Hp -= damage;

        if (Hp <= 0)
        {
            OnDie();
        }
    }

    /// <summary>
    /// 사망.
    /// </summary>
    public abstract void OnDie();    
}