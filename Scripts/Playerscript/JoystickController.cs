using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public RectTransform joystickOutline; //image olduklarý için recttransform kullandýk
    [SerializeField] private RectTransform joystickButton; //privata böyle de ulaþabilirliz
    private bool canControlJoystick;
    private Vector3 tapPosition;
    [SerializeField] private float moveFactor;
    private Vector3 move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideJoystick();


    }
    public void TappedOnJoystickZone()
    {
        Debug.Log("ekrana dokunuldu");
        tapPosition = Input.mousePosition;
        joystickOutline.position = tapPosition;
        //ekrana dokunulduðuunu anlýcaz ve joystick ekranda belirecek
        ShowJoystick();

    }
    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true); //ekranda joystiði göstercek 
        canControlJoystick = true;
    }
    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);  //ekranda joystiði gizlicek 
        canControlJoystick = false;
        move = Vector3.zero;//joystik ekrandan gidince hareketi durdur 
    }
    public void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - tapPosition;
        float canvasYScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.y;
        float joystickOutlineHalfWidth = joystickOutline.rect.width / 2;
        float newwidth = joystickOutlineHalfWidth * canvasYScale;
        float moveMagnitude = direction.magnitude * moveFactor * canvasYScale;
        moveMagnitude = Mathf.Min(moveMagnitude, newwidth);
        move = direction.normalized * moveMagnitude;
        Vector3 targetPos = tapPosition + move;
        joystickButton.position = targetPos;
        //karakteri joystik ile hareket ettireceðiz
        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }

    }
    public Vector3 GetMovePosition()
    {
        return move / 1.75f;
    }
    // Update is called once per frame
    void Update()
    {
        if (canControlJoystick)
        {
            ControlJoystick();

        }

    }
}

