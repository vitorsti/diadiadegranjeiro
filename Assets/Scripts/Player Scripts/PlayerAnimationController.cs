using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimationController : MonoBehaviour
{
    public static bool spriteDirection;
    int genreId;
    public bool ChangeGenre;
    public bool editor;
    public RuntimeAnimatorController femaleController, maleController;
    public Sprite maleBackIdle, maleFrontIdle, femaleBackIdle, femaleFrontIdle;
    SpriteRenderer spriteRenderer;
    Animator animator;
    PlayerController playerController;
    // Start is called before the first frame update

    void Awake()
    {

#if UNITY_EDITOR
        editor = true;
#endif
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;
        animator = GetComponent<Animator>();
        animator.SetBool("Front_back_idle", spriteDirection);
        playerController = FindObjectOfType<PlayerController>();

        /*if (editor == false)
        {*/
            genreId = PlayerPrefs.GetInt("Gender");

            if (genreId == 0)
                ChangeGenre = true;
            else
                ChangeGenre = false;
        //}

    }
    void Start()
    {


        if (ChangeGenre)
        {
            animator.runtimeAnimatorController = maleController;
        }
        else
        {
            animator.runtimeAnimatorController = femaleController;
        }


    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Walk", playerController.agent.velocity.magnitude);

        genreId = PlayerPrefs.GetInt("Gender");

        if (genreId == 0)
            ChangeGenre = true;
        else
            ChangeGenre = false;

        if (ChangeGenre)
        {
            animator.runtimeAnimatorController = maleController;
        }
        else
        {
            animator.runtimeAnimatorController = femaleController;
        }

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.M))
        {
            //verdadeiro para Genero masculino
            //ChangeGenre = true;
            animator.runtimeAnimatorController = maleController;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            //falso para Genero feminino
            //ChangeGenre = false;
            animator.runtimeAnimatorController = femaleController;
        }

#endif

    }

    public static void SpriteDirectior(bool direction)
    {

        spriteDirection = direction;
        PlayerAnimationController playerAnimation;
        playerAnimation = FindObjectOfType<PlayerAnimationController>();
        playerAnimation.animator.SetBool("Front_back_idle", direction);

    }
}
