using System;
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

    // Animator pentru animatii
    private Animator animator;

    // Referinta la CapsuleCollider
    private CapsuleCollider capsuleCollider;
    private float originalColliderHeight; // Inaltimea originala a collider-ului
    private Vector3 originalColliderCenter; // Pozitia originala a centrului collider-ului

    void Start()
    {
        // Pozitia initiala
        targetPosition = transform.position;

        // Luam componenta Animator atasata la obiectul playerului
        animator = GetComponent<Animator>();

        // Luam referinta la CapsuleCollider
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Salvam inaltimea si pozitia originala a collider-ului
        originalColliderHeight = capsuleCollider.height;
        originalColliderCenter = capsuleCollider.center;
    }

    void Update()
    {
        // Verificam daca nu exista vreo miscare in desfasurare
        if (!isMoving)
        {
            // Mutare pe banda din stanga (A)
            if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
            {
                ChangeLane(-1);
                animator.SetTrigger("RunLeft"); // Declanseaza animatia RunLeft
            }
            // Mutare pe banda din dreapta (D)
            else if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
            {
                ChangeLane(1);
                animator.SetTrigger("RunRight"); // Declanseaza animatia RunRight
            }

            // Saritura (W)
            if (Input.GetKeyDown(KeyCode.W) && !isJumping)
            {
                animator.SetTrigger("JumpWhileRunning"); // Declanseaza animatia de saritura
                StartCoroutine(Jump()); // Incepem saritura
            }

            // Slide (S)
            if (Input.GetKeyDown(KeyCode.S) && !isSliding)
            {
                animator.SetTrigger("RollForward"); // Declanseaza animatia RollForward
                StartCoroutine(Slide()); // Incepem slide-ul
            }
        }

        // Tranzitie smooth catre noua pozitie
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
    }

    public void ChangeLane(int direction)
{
    // direction poate fi -1 pentru stânga sau 1 pentru dreapta
    int newLane = currentLane + direction;

    // Verificăm limitele benzii (0 = stânga, 2 = dreapta)
    if (newLane >= 0 && newLane <= 2)
    {
        currentLane = newLane;
        MoveToLane();
    }
}


    // Functie pentru schimbarea benzii
    void MoveToLane()
    {
        // Setam flag-ul de miscare
        isMoving = true;

        // Calculam noua pozitie pe axa X in functie de banda curenta
        targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);

        // Resetam flag-ul de miscare dupa un mic delay
        StartCoroutine(ClearMovementFlagAfterDelay(0.3f)); // Ajusteaza delay-ul daca este necesar
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

        // Modificam collider-ul pentru saritura
        capsuleCollider.height = originalColliderHeight; // Setam inaltimea originala
        capsuleCollider.center = originalColliderCenter; // Setam pozitia originala a centrului

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

        // Salvam original collider settings pentru revenirea dupa slide
        capsuleCollider.height = 1f; // Reducem inaltimea collider-ului pentru slide
        capsuleCollider.center = new Vector3(capsuleCollider.center.x, 0.5f, capsuleCollider.center.z); // Ajustam centrul collider-ului pentru a se potrivi cu slide-ul

        // Durata slide-ului
        yield return new WaitForSeconds(slideDuration);

        // Revenim la setarile initiale ale collider-ului
        capsuleCollider.height = originalColliderHeight;
        capsuleCollider.center = originalColliderCenter;

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
