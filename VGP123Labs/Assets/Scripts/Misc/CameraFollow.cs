using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float minXClamp = 8.42f;
    public float maxXClamp = 242.4f;
    public float minYClamp = 0.0f;
    public float maxYClamp = 11.25f;

    //This function always runs after fixed update - unity specifies that this is where camera movement should happen
    private void LateUpdate()
    {
        Vector3 cameraPos = transform.position;

        cameraPos.x = Mathf.Clamp(player.transform.position.x,minXClamp, maxXClamp);
        cameraPos.y = Mathf.Clamp(player.transform.position.y, minYClamp, maxYClamp);

        transform.position = cameraPos;
    }
}
