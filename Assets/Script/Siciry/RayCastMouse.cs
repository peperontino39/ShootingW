using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastMouse : MonoBehaviour {
    public Camera _camera;
    public float distance = 100f;
    void Update()
    {
        // 左クリックを取得
        if (Input.GetMouseButtonDown(0))
        {
            // クリックしたスクリーン座標をrayに変換
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, new Color(1, 0, 0, 1));
            //Rayの当たったオブジェクトの情報を格納する
            RaycastHit hit = new RaycastHit();
            
            //オブジェクトにrayが当たった時
            if (Physics.Raycast(ray, out hit,distance))
            {
                // rayが当たったオブジェクトの名前を取得
                var _enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if(_enemy != null)
                {
                    _enemy.AddDamage(1);
                }
            }
        }
        
    }

   
}
