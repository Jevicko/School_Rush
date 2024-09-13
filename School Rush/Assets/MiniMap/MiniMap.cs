/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{

      public Transform player;
      //public Transform camera;

      private void LateUpdate()
      {
        Vector3 newPos = player.position;
        newPos = transform.position;
        //transform.position = newPos;

        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
      }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        // Get the player's position
        Vector3 newPos = player.position;

        // Keep the current minimap height (Y-axis)
        newPos.y = transform.position.y;

        // Update the minimap's position
        transform.position = newPos;

        // Rotate the minimap to match the player's rotation (around the Y-axis)
        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }
}
