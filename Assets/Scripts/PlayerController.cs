using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
     //public properties
    public float velocityX = 20;
    public float JumpForce = 40;
    
    //limites de salto
    private int saltos = 2;//maximo de saltos
    private int cont = 0;//contar saltos

    //vida
    private int vida;
    private bool isDead = false;
    //private int score = 0;
    private int lifes = 3;
    private int enemys = 10;

 
    
    //private components
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;
    private GameController game;

    
    //Const
    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;
    private const int ANIMATION_DEAD = 3;

    private const int LAYER_GROUND = 6;
    private const string TAG_ENEMY = "Enemy";

    void Start()
    {
        Debug.Log("Iniciando Game Objet");//mensaje en consola
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        game  = FindObjectOfType<GameController>();
        
    }

    void Update()
    {
        if(!isDead)
        {
        //cambio velocidad en X mas no en Y
        rb.velocity = new Vector2(0, rb.velocity.y);
        changeAnimation(ANIMATION_IDLE);
        
        //Ir a la derecha
        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            sr.flipX = false;
            changeAnimation(ANIMATION_RUN);
        }
        
        //Ir a la izquierda
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            sr.flipX = true;
            changeAnimation(ANIMATION_RUN);
        }

        
        //movimiento salto
        if(Input.GetKeyUp(KeyCode.Space) )
        {
            //vemos el limite de salto
            if(cont < saltos)
            {
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                cont++; //sumamos contador de saltos
            }
            changeAnimation(ANIMATION_JUMP);
            Debug.Log(cont);//mensaje en consola
        }
        }
       
    }

    //COLISIONES
    private void OnCollisionEnter2D(Collision2D colision)
    {
        //colisiona y se reseta los saltos a 0  
        if (colision.gameObject.layer == LAYER_GROUND)
        {
            Debug.Log("Collision: " + colision.gameObject.name);
            cont = 0;//reseteo el contador de saltos
        }
        //colisiona con enemigo
        if (colision.gameObject.CompareTag(TAG_ENEMY))
        {
            game.LoseLife();//pierde vida tras colisionar
            vida ++;
        }
        if (colision.gameObject.CompareTag(TAG_ENEMY)&& vida == 3)
        {
            isDead = true;
            changeAnimation(ANIMATION_DEAD);
        }
        
    }
    
    //cambio de estado
    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
    
    public void LoseLife()
    {
        lifes -=1;//reduce en uno
    }
}
