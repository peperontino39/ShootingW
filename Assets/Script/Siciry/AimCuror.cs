using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCuror : MonoBehaviour {

    public Texture2D cursor;	//　カーソルに使用するテクスチャ

    [SerializeField]
    GameObject effect;

     void  Start()
    {
        //　カーソルを自前のカーソルに変更
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);
    }

    // 位置座標
    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;
    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        // Vector3でマウス位置座標を取得する
        position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        // ワールド座標に変換されたマウス座標を代入
        effect.transform.position = screenToWorldPointPosition;

        shot();
    }

    void shot(){
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(effect,effect.transform.position,effect.transform.rotation);
        }
        }

}
