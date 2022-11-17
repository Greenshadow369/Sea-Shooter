using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    [SerializeField] float parallaxEffect;
    [SerializeField] float speed;
    private float length, startPos;
    private float disFromMiddle = 0;

    private void Start() {
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update() {
        disFromMiddle = disFromMiddle + speed;
        float distance = (disFromMiddle * (1 - parallaxEffect));
        transform.position = new Vector3(transform.position.x, startPos + distance, transform.position.z);
        if(distance < startPos - length)
        {
            transform.position = new Vector3(transform.position.x, startPos - length, transform.position.z);
            disFromMiddle = 0f;
        }
    }
}
