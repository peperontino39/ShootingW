using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゴリラ登場クラス
public class GoriraAppearance : MonoBehaviour
{
	[SerializeField]
	Sprite tukamiSpr;       //つかんでる手
	[SerializeField]
	Sprite gorirakougeki;   //攻撃ゴリラ

	[SerializeField]
	Sprite NormalGorira;   //普通のゴリラ

	[SerializeField]
	Sprite teHiraki;        //開いてる手

	[SerializeField]
	GameObject concentrationLine;        //集中線



	[SerializeField]
	SpriteRenderer LeftRen;

	[SerializeField]
	SpriteRenderer RightRen;

	[SerializeField]
	SpriteRenderer MainRen;

	[SerializeField]
	GameObject tumeato;

	public BossManager bossManager;

	void Start()
	{
		AppearanceStart();
	}

	public void AppearanceStart()
	{
		StartCoroutine(Appearance());
	}

	//登場関数
	IEnumerator Appearance()
	{
		//右手上げる

		yield return
			StartCoroutine(
				Easing.Tween(2, (t) =>
				{
					var y = Mathf.Lerp(-4.2f, -1.4f, t);
					RightRen.gameObject.transform.position
					= new Vector3(RightRen.gameObject.transform.position.x, y,
					RightRen.gameObject.transform.position.z);
				}, () =>
				{
					RightRen.sprite = tukamiSpr;
					RightRen.transform.localRotation = Quaternion.Euler(0, 0, -33);
				}));
		//左手上げる
		yield return
	 StartCoroutine(
		 Easing.Tween(2, (t) =>
		 {
			 var y = Mathf.Lerp(-4.2f, -1.4f, t);
			 LeftRen.gameObject.transform.position
			 = new Vector3(LeftRen.gameObject.transform.position.x, y,
			 LeftRen.gameObject.transform.position.z);
		 }, () =>
		 {
			 LeftRen.sprite = tukamiSpr;
			 LeftRen.transform.localRotation = Quaternion.Euler(0, 0, 33);
		 }));

		//かおが出てくる
		yield return
		StartCoroutine(
		Easing.Tween(2, (t) =>
		{
			var y = Mathf.Lerp(-3.7f, 0.57f, t);
			MainRen.gameObject.transform.position
			= new Vector3(MainRen.gameObject.transform.position.x, y,
			MainRen.gameObject.transform.position.z);
		}));
		yield return null;

		yield return new WaitForSeconds(2);
		if (bossManager.tumeKougekiNum > 1)
			AtackStart();
		else
			StartCoroutine(Return());
	}

	void AtackStart()
	{
		StartCoroutine(Atack());
	}

	IEnumerator Atack()
	{
		MainRen.sprite = gorirakougeki;
		LeftRen.transform.localPosition = new Vector3(3.75f, 2.75f, 0);
		LeftRen.transform.localRotation = Quaternion.Euler(0, 0, 0);
		LeftRen.sprite = teHiraki;
		RightRen.gameObject.SetActive(false);
		concentrationLine.SetActive(true);
		yield return new WaitForSeconds(3);

		Instantiate(tumeato);

		yield return
	   StartCoroutine(Easing.Tween(0.5f, (t) =>
	   {
		   LeftRen.transform.localPosition = Vector3.Lerp(new Vector3(3.75f, 2.75f, 0),
			   new Vector3(-4.08f, -2.21f, 0), Easing.InOutQuart(t));
	   }));

		StartCoroutine(Return());
	}



	IEnumerator Return()
	{

		MainRen.sprite = NormalGorira;
		RightRen.gameObject.SetActive(true);
		concentrationLine.SetActive(false);
		RightRen.sprite = tukamiSpr;
		RightRen.transform.localRotation = Quaternion.Euler(0, 0, -33);
		RightRen.transform.localPosition = new Vector3(-3, -1.4f, 0);

		LeftRen.sprite = tukamiSpr;
		LeftRen.transform.localRotation = Quaternion.Euler(0, 0, 33);
		LeftRen.transform.localPosition = new Vector3(3, -1.4f, 0);

		yield return new WaitForSeconds(1);

		yield return StartCoroutine(
		Easing.Tween(2, (t) =>
		{
			var y = Mathf.Lerp(0.57f, -3.7f, t);
			MainRen.gameObject.transform.position
			= new Vector3(MainRen.gameObject.transform.position.x, y,
			MainRen.gameObject.transform.position.z);
		}));

		bossManager.isActionEnd = true;

		Destroy(gameObject);
	}




}
