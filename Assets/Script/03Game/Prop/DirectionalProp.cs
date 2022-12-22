using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUBT;

public class DirectionalProp : MonoBehaviour
{
    public enum Dir
    {
        Up, Left, Right, Down
    }
    [Header("力度")]
    public float force;
    [Header("方向")]
    public Dir dir;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoleController roleController = collision.GetComponent<RoleController>();
        if (roleController != null)
        {
            Database database = roleController.GetComponent<Database>();
            Vector2 moveDir = Vector2.up;
            switch (dir)
            {
                case Dir.Up:
                    moveDir = Vector2.up;
                    break;
                case Dir.Left:
                    moveDir += Vector2.left;
                    break;
                case Dir.Right:
                    moveDir += Vector2.right;
                    break;
                case Dir.Down:
                    moveDir += Vector2.down;
                    break;
            }

            roleController.RoleRigidbody.velocity = Vector2.zero;
            roleController.Move(moveDir, force);
            database.InputDir = moveDir;
        }
    }
}
