using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator), typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    //private - private to the class that has created it. It is only a property of the class and nothing else can access it.
    //public - it's a party and everybody is invited. Any object that has reference to this class can grab the variable or property.
    //protected - private but also accessible to child classes.

    protected SpriteRenderer sr;
    protected Animator anim;
    protected AudioSource audioSource;

    protected int health;
    [SerializeField] protected int maxHealth;

    //Audio for enemy deaths
    [SerializeField] protected AudioClip deathClip;

    // Start is called before the first frame update
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = GameManager.Instance.SFXGroup;

        if (maxHealth <= 0) maxHealth = 10;

        health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (deathClip != null)
            {
                audioSource.PlayOneShot(deathClip);
            }

            anim.SetTrigger("Death");

            if (transform.parent != null)
                Destroy(transform.parent.gameObject, 2);
            else
                Destroy(gameObject, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
