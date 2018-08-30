/*
 * Ones that survive on the screen the longest are the fittest
 * https://www.udemy.com/machine-learning-with-unity/
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{

    // gene for color that will be passed to the offsprings
    public float r;
    public float g;
    public float b;

    // size
    public float scaleX;     // Note:  with two values for x and y, can make people with different body proportions
    public float scaleY;

    bool dead = false;              // dead when clicked on
    public float timeToDie = 0f;    // time until they get clicked
    SpriteRenderer sRenderer;  // to turn them off
    Collider2D sCollider;           // to turn them off

    private void OnMouseDown()
    {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        Debug.Log("Dead At: " + timeToDie);
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();

        sRenderer.color = new Color(r, g, b);

        transform.localScale = new Vector3(scaleX, scaleY, 0);
    }

    void Update()
    {

    }
}
