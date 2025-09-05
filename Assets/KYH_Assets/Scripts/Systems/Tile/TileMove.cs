using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector2 playerPos = Player.instance.transform.position;
        Vector2 tilePos = transform.position;

        float dotX = Vector2.Dot((playerPos - tilePos).normalized, Vector2.right);
        float dotY = Vector2.Dot((playerPos - tilePos).normalized, Vector2.up);

        if (Mathf.Abs(dotX) > Mathf.Abs(dotY))
            transform.Translate(new Vector2(dotX > 0 ? 40 : -40, 0));
        else if (Mathf.Abs(dotX) < Mathf.Abs(dotY))
            transform.Translate(new Vector2(0, dotY > 0 ? 40 : -40));
        else
            transform.Translate(new Vector2(dotX > 0 ? 40 : -40, dotY > 0 ? 40 : -40));
    }
}
