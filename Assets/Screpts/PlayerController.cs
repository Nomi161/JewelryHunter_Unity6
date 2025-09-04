using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�̔\�͒l")]
    public float speed = 3.0f;      // �v���C���[�̃X�s�[�h�𒲐�
    public float jumpPower = 9.0f;  // �W�����v��

    [Header("�n�ʔ���̑Ώۃ��C���[")]
    public LayerMask groundLayer;   // �n�ʃ��C���[���w�����邽�߂̕ϐ�

    Rigidbody2D rbody;              // Player�ɂ��Ă���Rigidbody2D���������߂̕ϐ�
    Animator animator;              // Animetor�R���|�[�l���g���������߂̕ϐ�

    float axisH;                    // ���͂̕������L�����邽�߂̕ϐ�
    bool goJump = false;            // �W�����v�t���O(true:�^on/false:�Uoff)
    bool onGround = false;          // �n�ʂɂ��邩�ǂ����̔���(�n�ʂɂ���Ftrue/�n�ʂɂ��Ȃ��Ffalse)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();    // Player�ɂ��Ă���R���|�[�l���g�����擾
        animator = GetComponent<Animator>();    // Animator�R���|�[�l���g�̏�����
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity�̌��ƂȂ�l�̎擾(�E�Ȃ�1.0f�A���Ȃ�-1.0f�A�Ȃɂ��Ȃ����0)
        axisH = Input.GetAxisRaw("Horizontal");

        // ���͂���Ă��鍶�E�L�[�̏�Ԃɏ]���v���C���[�̌�����ς���
        if (axisH > 0)       // �E��������
        {
            // �E������
            transform.localScale = new Vector3(1, 1, 1);
            
        }
        else if (axisH < 0)  // ����������
        {
            // ��������
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // GetButtonDown���\�b�h�������Ɏw�肵���{�^���������ꂽ��true��Ԃ�/������Ă��Ȃ����false��Ԃ�
        if (Input.GetButtonDown("Jump"))
        {
            Jump();   // Jump���\�b�h�̔���
        }

    }

    // 1�b�Ԃ�50��(50fps)�J��Ԃ��悤�ɐ��䂵�Ȃ���s���J��Ԃ����\�b�h
    private void FixedUpdate()
    {
        // �n�ʔ�����T�[�N���L���X�g�ł����Ȃ��āA���̌��ʂ�onGround�ɑ��
        onGround = Physics2D.CircleCast(
            transform.position,     // ���ˈʒu=�v���C���[�̈ʒu(��_)
            0.2f,                   // ��������~�̔��a
            new Vector2(0, 1.0f),   // ���˕��� ��������
            0,                      // ���ˋ���
            groundLayer             // �ΏۂƂȂ郌�C���[��� ��LayerMask
            );

        // Velocity�ɒl����
        // ��y�̒l�͏d�͂���ɓ����ĕω����Ă���̂ł��̒l�������߂�
        rbody.linearVelocity = new Vector2(axisH * speed, rbody.linearVelocity.y);

        // �W�����v�t���O����������
        if (goJump)
        {
            // �W�����v�����遨�v���C���[����ɉ����o��
            rbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            goJump = false; // �t���O��OFF�ɖ߂�
        }

        if (onGround)   // �n�ʂ̏�ɂ��鎞
        {
            if (axisH == 0) // ���E��������Ă��Ȃ�
            {
                animator.SetBool("Run", false); // Idle�A�j���ɐ؂�ւ�
            }
            else // ���E��������Ă���
            {
                animator.SetBool("Run", true);  // Run�A�j���ɐ؂�ւ�
            }
        }
    }

    // �W�����v�{�^���������ꂽ���ɌĂяo����郁�\�b�h
    void Jump()
    {
        // �v���C���[���n�ʂɂ��邩�m�F����
        if (onGround)
        {
            goJump = true;  // �W�����v�t���O��ON
            animator.SetTrigger("Jump");
        }
    }
}
