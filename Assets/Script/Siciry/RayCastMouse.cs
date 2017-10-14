using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastMouse : MonoBehaviour
{
    public Camera _camera;
    public float distance = 100f;

   // public Texture2D cursor;	//　カーソルに使用するテクスチャ

    [SerializeField]
    GameObject effect;

    Vector3 vafPos;

    [SerializeField]
    GameObject aimImage;

    // 位置座標
    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;
    // Use this for initialization

    void Start()
    {

        //　カーソルを自前のカーソルに変更
        // Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);

        vafPos = Input.mousePosition;

        Cursor.visible = false;

    }




    void Update()
    {

        vafPos = Vector3.Lerp(vafPos, Input.mousePosition, 0.2f);

        vafPos.z = 10f;

        

        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(vafPos);
        // ワールド座標に変換されたマウス座標を代入
        effect.transform.position = screenToWorldPointPosition;

        aimImage.transform.position = vafPos;

        // 左クリックを取得
        if (Input.GetMouseButtonDown(0))
        {
            // クリックしたスクリーン座標をrayに変換
            Ray ray = _camera.ScreenPointToRay(vafPos);
            Debug.DrawRay(ray.origin, ray.direction, new Color(1, 0, 0, 1));
            //Rayの当たったオブジェクトの情報を格納する
            RaycastHit hit = new RaycastHit();
            AudioManager.Instans.PlaySE("ban");
            //オブジェクトにrayが当たった時
            if (Physics.Raycast(ray, out hit, distance))
            {

                // rayが当たったオブジェクトの名前を取得
                var _enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (_enemy != null)
                {
                    _enemy.AddDamage(1);
                }
            }
        }



        shot();

    }




    void shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(effect, effect.transform.position, effect.transform.rotation);
        }
    }


}
