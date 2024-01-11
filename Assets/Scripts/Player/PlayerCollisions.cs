using UnityEngine;
using DG.Tweening;
public static class Tags
{
    public const string Size = nameof(Size);
    public const string Obstacle = nameof(Obstacle);
    public const string Gate = nameof(Gate);
    public const string Saw = nameof(Saw);
    public const string Finish = nameof(Finish);
    public const string Gem = nameof(Gem);
    public const string Hammer = nameof(Hammer);
}


public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticles;
    private Animator playerAnim;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        bloodParticles.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
{
    switch (other.tag)
    {
        case Tags.Size:
            GameEvents.instance.playerSize.Value += 1;
            other.GetComponent<Collider>().enabled = false;
            AudioManager.instance.PlayHealthSound();
            other.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                Destroy(other.gameObject);
            });
            break;

        case Tags.Obstacle:
            playerAnim.SetTrigger("kick");
            other.GetComponent<Block>().CheckHit();
            break;

        case Tags.Gate:
            other.GetComponent<Gate>().ExecuteOperation();
            break;

        case Tags.Saw:
            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
            bloodParticles.SetActive(true);
            GetComponent<Collider>().enabled = false;
            break;

        case Tags.Finish:
            GameEvents.instance.gameWon.SetValueAndForceNotify(true);
            break;
        
        case Tags.Gem:
            GameEvents.instance.gemCollected.Value += 1;
            other.GetComponent<Collider>().enabled = false;
            AudioManager.instance.PlayGemSound();
            other.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                Destroy(other.gameObject);
            });
            break;
            
        case Tags.Hammer:
            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
            bloodParticles.SetActive(true);
            GetComponent<Collider>().enabled = false;
            break;    

    }
}

}