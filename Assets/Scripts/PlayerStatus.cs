using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isAttack;
    public bool isRestoringHealth;
    private float Max_Hp;
    public float hp;
    public float timer;
    private float resetTime;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        Max_Hp = 100f;
        hp = Max_Hp;
        isAttack = false;
        resetTime = 5f;
        timer = resetTime;
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerHealth();
    }

    private void SetPlayerHealth()
    {
        if (hp == Max_Hp || EnermyBehaviour.isAttacking)
        {
            timer = resetTime;
        }

        if (hp < Max_Hp)
        {
            if (!EnermyBehaviour.isAttacking)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    RestoreHealth();
                }
            }
        }
        else
        {
            hp = Max_Hp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterAttack"))
        {
            if (!isAttack)
            {
                hp -= 20;
                isAttack = true;
                Invoke("Reset", 2.63f);
            }
        }
    }
    private void Reset()
    {
        isAttack = !isAttack;
    }

    private void RestoreHealth()
    {
        hp += Time.deltaTime * speed;
    }
}
