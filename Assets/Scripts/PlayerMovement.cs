using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 3f; // Latimea drumului
    private int currentLane = 1; // 0 = stanga, 1 = mijloc, 2 = dreapta
    private Vector3 targetPosition;

    public float jumpHeight = 2f; // Inaltimea sariturii
    public float slideDuration = 1f; // Durata slide-ului
    private bool isJumping = false; // Flag pentru a verifica daca jucatorul sare
    private bool isSliding = false; // Flag pentru a verifica daca jucatorul face slide
    private bool isMoving = false; // Flag pentru a verifica daca exista vreo miscare in desfasurare
    private float originalYScale; // Inaltimea originala a jucatorului

    void Start()
    {
        // Pozitia initiala
        targetPosition = transform.position;
        originalYScale = transform.localScale.y; // Memoram inaltimea initiala a jucatorului
    }

    void Update()
    {
        // Verificam daca nu exista vreo miscare in desfasurare
        if (!isMoving)
        {
            // Mutare pe banda din stanga (A)
            if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
            {
                currentLane--;
                MoveToLane(); // Schimbam banda
            }
            // Mutare pe banda din dreapta (D)
            else if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
            {
                currentLane++;
                MoveToLane(); // Schimbam banda
            }

            // Saritura (W)
            if (Input.GetKeyDown(KeyCode.W) && !isJumping)
            {
                StartCoroutine(Jump()); // Incepem saritura
            }

            // Slide (S)
            if (Input.GetKeyDown(KeyCode.S) && !isSliding)
            {
                StartCoroutine(Slide()); // Incepem slide-ul
            }
        }

        // Tranzitie smooth catre noua pozitie
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
    }

    // Functie pentru schimbarea benzii
    void MoveToLane()
    {
        // Setam flag-ul de miscare
        isMoving = true;

        // Calculam noua pozitie pe axa X in functie de banda curenta
        targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);

        // Resetam flag-ul de miscare dupa un mic delay
        StartCoroutine(ClearMovementFlagAfterDelay(0.2f)); // Ajusteaza delay-ul daca este necesar
    }

    // Coroutine pentru saritura
    IEnumerator Jump()
    {
        isJumping = true; // Setam flag-ul de saritura
        isMoving = true;  // Setam flag-ul de miscare

        // Parametrii pentru saritura
        float jumpTime = 0.5f; // Durata sariturii
        float elapsedTime = 0;

        Vector3 startPos = transform.position;
        Vector3 jumpTarget = new Vector3(startPos.x, startPos.y + jumpHeight, startPos.z);

        // Realizam saritura in sus
        while (elapsedTime < jumpTime)
        {
            transform.position = Vector3.Lerp(startPos, jumpTarget, (elapsedTime / jumpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Revenirea pe sol
        elapsedTime = 0;
        while (elapsedTime < jumpTime)
        {
            transform.position = Vector3.Lerp(jumpTarget, startPos, (elapsedTime / jumpTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isJumping = false; // Resetam flag-ul de saritura
        isMoving = false;  // Resetam flag-ul de miscare
    }

    // Coroutine pentru slide
    IEnumerator Slide()
    {
        isSliding = true;  // Setam flag-ul de slide
        isMoving = true;   // Setam flag-ul de miscare

        // Salvam inaltimea originala si scala jucatorului
        float halfHeight = originalYScale / 2f;
        Vector3 originalScale = transform.localScale;
        Vector3 originalPosition = transform.position;

        // Reducem scala pe axa Y
        Vector3 targetScale = new Vector3(originalScale.x, halfHeight, originalScale.z);

        // Ajustam pozitia pe Y astfel incat jucatorul sa ramana la nivelul solului
        Vector3 slideTargetPosition = new Vector3(originalPosition.x, originalPosition.y / 2, originalPosition.z);

        // Timpul scurs pentru slide
        float elapsedTime = 0f;

        // Reducerea dimensiunii jucatorului
        while (elapsedTime < slideDuration / 2f)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, (elapsedTime / (slideDuration / 2f)));
            transform.position = Vector3.Lerp(originalPosition, slideTargetPosition, (elapsedTime / (slideDuration / 2f)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Pauza scurta in pozitia de slide
        yield return new WaitForSeconds(slideDuration / 2f);

        // Revenirea la dimensiunea si pozitia originala
        elapsedTime = 0f;
        while (elapsedTime < slideDuration / 2f)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, (elapsedTime / (slideDuration / 2f)));
            transform.position = Vector3.Lerp(slideTargetPosition, originalPosition, (elapsedTime / (slideDuration / 2f)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isSliding = false; // Resetam flag-ul de slide
        isMoving = false;  // Resetam flag-ul de miscare
    }

    // Coroutine pentru resetarea flag-ului de miscare dupa un delay
    IEnumerator ClearMovementFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isMoving = false; // Resetam flag-ul de miscare
    }
}
