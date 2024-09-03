using UnityEngine;

public class EnemyTurret : Enemy
{

    [SerializeField] private float projectileFireRate;

    private float timeSinceLastFire = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController pc = GameManager.Instance.PlayerInstance;
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        float distance = Vector2.Distance(pc.transform.position, transform.position);

        if (curPlayingClips[0].clip.name.Contains("Idle"))
        {
            if (Time.time >= timeSinceLastFire + projectileFireRate && distance <= 5.0)
            {
                anim.SetTrigger("Fire");
                timeSinceLastFire = Time.time;
            }
        }

        if (pc.transform.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
