using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Singleton<Circle>
{
    private CircleMovement circleMovement;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        circleMovement = GetComponent<CircleMovement>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();    
        
        GameStateManager.OnGameEnded.AddListener(() => circleMovement.enabled = false);
    }

    public void Dead()
    {
        StartCoroutine(Poof(0.2f, new Color(0, 0, 0, 0)));
    }

    private IEnumerator Poof(float speed, Color directColor)
    {
        while (spriteRenderer.color.a > 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 4, speed);
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, directColor, speed);

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IInteractable interactable = collision.gameObject.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            interactable.Interact();
        }
    }
}
