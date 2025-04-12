using UnityEngine;

public class PlayerAnimatior : MonoBehaviour

{
    [SerializeField] private Animator playerAC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ManageAnimations(Vector3 move)
    {
        if (move.magnitude > 0)
        {
            PlayRunAnimation();
            playerAC.transform.forward = move.normalized;//döndürülen tarafa koþmsý için 
        }
        else
        {
            PlayIdleAnimation();
        }



    }
    private void PlayRunAnimation()
    {
        playerAC.Play("RUN");
    }
    private void PlayIdleAnimation()
    {
        playerAC.Play("IDLE");
    }
}
