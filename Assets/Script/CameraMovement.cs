using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Creo un nuevo vector para que el Z ya sea el de la camara y no el del target
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

        //Clamp, Restringe el valor entre el minimo y mayor
        targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

        if(transform.position != target.position){
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
