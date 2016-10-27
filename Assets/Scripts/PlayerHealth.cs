using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health;
    GameObject hptext;
    bool m_vulnerable = true;
    public float m_invincibilityTime = 2;
    private SpriteRenderer m_spriteRenderer;

    // Use this for initialization
    void Start()
    {
        hptext = GameObject.Find("HPText");
        health = 10;
        hptext.GetComponent<Text>().text = health.ToString() + "x";
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PlayerDamage(int dmgs)
    {
        if (m_vulnerable)
        {

            health -= dmgs;
            hptext.GetComponent<Text>().text = health.ToString() + "x";
            if (health <= 0)
            {
                GameObject.Find("GameManager").GetComponent<gamemanager>().won = false;
                GameObject.Find("GameManager").GetComponent<gamemanager>().LoadScene(2);
            }
            GetInvincible(m_invincibilityTime);
        }
    }

    public void GetInvincible(float time, bool flashing = true)
    {
        if (m_vulnerable)
            StartCoroutine(Invincible(time, flashing));
    }

    IEnumerator Invincible(float time, bool flashing)
    {
        m_vulnerable = false;

        if (flashing)
        {
            Coroutine cor = StartCoroutine(FlashingHurt());
            yield return new WaitForSeconds(time);
            StopCoroutine(cor);
            SetAlpha(1);
        }
        else
        {
            yield return new WaitForSeconds(time);
        }
        m_vulnerable = true;
    }

    IEnumerator FlashingHurt()
    {
        while (true)
        {
            if (m_spriteRenderer.color.a == 0)
                SetAlpha(1);
            else SetAlpha(0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void SetAlpha(int i)
    {
        Color c = m_spriteRenderer.color;
        c.a = i;
        m_spriteRenderer.color = c;
    }
}
