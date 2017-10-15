using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public enum Status
{
	STAY_LEFT,  //タワー左つかまり
	STAY_RIGHT, //タワー右つかまり
	ROOLLING,   //タワー間ローリング移動
	UNKO_BLOW_LEFT,//unko投げ
	UNKO_BLOW_RIGHT,//unko投げ
	CLIMB_SCRATCH,    //手前ビルよじ登りひっかき攻撃
	STAGGER,    //ひっかき攻撃時にダメージを受けたときのひるみ
	START_ACTION,//スタート時のアクション
	FHASE_CHANGE_ATION,//フェイズ変更時のアクション
	END_ACTION//最後のアクション
}

public enum Phase
{
	FIRST_HALF,
	LATTER_HALF,
	END
}

public class BossManager : MonoBehaviour
{
	public struct Point
	{
		public Vector3 pos; //位置
		public float scale; //拡大サイズ
	}

	public struct TakeTime
	{
		public int p;
		public float time;
		public int rotateDirection;
	}

	public struct Root
	{
		public int p;
		public List<TakeTime> takeTimes;
	}

	private string pointDataFileName = "data/BossPoint.csv";
	private string nextPointFileName = "data/BossNextPoint.csv";
	private string nextLaterPointFileName = "data/BossNextLaterPoint.csv";
	private string spriteFileName = "data/Sprites.csv";

	public struct Anim
	{
		public List<Sprite> sprites;
		public List<float> times;
		public float takeTime;
	}

	public Dictionary<Status, Anim> anims = new Dictionary<Status, Anim>();

	public List<Point> points = new List<Point>();
	public List<Root> roots = new List<Root>();
	public List<Root> laterRoots = new List<Root>();
	public int nowPointIndex = 0;
	public int nextPointIndex = 0;
	public bool isActionEnd = false;
	public Status status = Status.START_ACTION;
	public Phase phase = Phase.FIRST_HALF;
	public const float maxStayTime = 1.0f;
	public const float minStayTime = 0.5f;
	public float stayTime = 0.0f;
	public const int maxHP = 100;
	public int hp = 0;
	public const int power = 5;
	public const int maxStagger = 5;
	public int hitWhileStagger = 0;
	public int maxUnko = 3;
	public int moveNum = 0;
	public AnimationCurve curve;
	private SpriteRenderer spriteR;
	private bool isAnim = false;
	private float animTime = 0.0f;
	private int animIndex = 0;
	private bool isUnko = false;
	public GameObject unko;
	public GameObject menoaeGorira;
	public const int maxTumekougeki = 2;
	public int tumeKougekiNum = 0;

	void Start()
	{
		hp = maxHP;
		stayTime = maxStayTime;

		SetupPoint();
		SetupTakeTimes();
		SetupSprites();

		transform.position = points[0].pos;
		transform.localScale = Vector3.one * points[0].scale;
		spriteR = GetComponent<SpriteRenderer>();
	}

	void SetupPoint()
	{
		string currentPath = Directory.GetCurrentDirectory() + "/";

		using (StreamReader sr = new StreamReader(currentPath + pointDataFileName))
		{
			sr.ReadLine();
			while (sr.Peek() > -1)
			{
				string[] data = sr.ReadLine().Split(',');
				Point p = new Point();
				p.pos = new Vector3(
					float.Parse(data[0]),
					float.Parse(data[1]),
					float.Parse(data[2]));
				p.scale = float.Parse(data[3]);
				points.Add(p);
			}
		}
	}
	void SetupTakeTimes()
	{
		string currentPath = Directory.GetCurrentDirectory() + "/";
		using (StreamReader sr = new StreamReader(currentPath + nextPointFileName))
		{
			sr.ReadLine();
			while (sr.Peek() > -1)
			{
				string[] data = sr.ReadLine().Split(',');
				Root r = new Root();
				r.takeTimes = new List<TakeTime>();
				r.p = int.Parse(data[0]);

				for (int i = 1; i < data.Length; i += 3)
				{
					TakeTime takeTime = new TakeTime();
					takeTime.p = int.Parse(data[i]);
					takeTime.time = float.Parse(data[i + 1]);
					takeTime.rotateDirection = int.Parse(data[i + 2]);

					r.takeTimes.Add(takeTime);
				}

				roots.Add(r);
			}
		}

		using (StreamReader sr = new StreamReader(currentPath + nextLaterPointFileName))
		{
			sr.ReadLine();
			while (sr.Peek() > -1)
			{
				string[] data = sr.ReadLine().Split(',');
				Root r = new Root();
				r.takeTimes = new List<TakeTime>();
				r.p = int.Parse(data[0]);

				for (int i = 1; i < data.Length; i += 3)
				{
					TakeTime takeTime = new TakeTime();
					takeTime.p = int.Parse(data[i]);
					takeTime.time = float.Parse(data[i + 1]);
					takeTime.rotateDirection = int.Parse(data[i + 2]);

					r.takeTimes.Add(takeTime);
				}

				laterRoots.Add(r);
			}
		}
	}
	void SetupSprites()
	{
		string currentPath = Directory.GetCurrentDirectory() + "/";
		using (StreamReader sr = new StreamReader(currentPath + spriteFileName))
		{
			sr.ReadLine();
			while (sr.Peek() > -1)
			{
				string[] data = sr.ReadLine().Split(',');

				Anim anim = new Anim();
				anim.sprites = new List<Sprite>();
				anim.times = new List<float>();
				Status s = (Status)int.Parse(data[0]);
				for (int i = 1; i < data.Length; i += 2)
				{
					anim.sprites.Add(Resources.Load<Sprite>(data[i]));
					float time = float.Parse(data[i + 1]);
					anim.times.Add(time);
					anim.takeTime += time;
				}

				anims.Add(s, anim);
			}
		}
	}

	void Update()
	{
		if (isActionEnd)
		{
			if (isUnko) isUnko = false;
			nowPointIndex = nextPointIndex;
			animIndex = 0;
			animTime = 0.0f;
			ActionPattern();
			DecideModePoint();
		}

		if (stayTime > 0.0f)
		{
			stayTime = Mathf.Max(0.0f, stayTime - Time.deltaTime);
			if (stayTime <= 0.0f)
				isActionEnd = true;
		}

		UpdateAnimation();
	}

	public void ActionPattern()
	{
		switch (status)
		{
			case Status.STAY_LEFT:
			case Status.STAY_RIGHT:

				status = Status.ROOLLING;
				break;

			case Status.ROOLLING:

				if (phase == Phase.LATTER_HALF)
					moveNum++;

				if (nowPointIndex == 4)
				{
					status = Status.CLIMB_SCRATCH;
					hitWhileStagger = 0;
					break;
				}

				int randNum = Random.Range(0, 2);
				if (randNum == 0)
				{
					if (moveNum == maxUnko)
					{
						moveNum = 0;
						status = Status.UNKO_BLOW_LEFT;
					}
					else
					{
						status = Status.STAY_LEFT;
					}
				}
				else if (randNum == 1)
				{
					if (moveNum == maxUnko)
					{
						moveNum = 0;
						status = Status.UNKO_BLOW_RIGHT;
					}
					else
					{
						status = Status.STAY_RIGHT;
					}
				}
				break;

			case Status.UNKO_BLOW_LEFT:
			case Status.UNKO_BLOW_RIGHT:

				status = Status.ROOLLING;
				break;

			case Status.CLIMB_SCRATCH:
			case Status.STAGGER:

				status = Status.ROOLLING;

				break;
		}
	}


	public void DecideModePoint()
	{
		isActionEnd = false;
		if (nowPointIndex != 4)
			spriteR.sprite = anims[status].sprites[0];

		switch (status)
		{
			case Status.STAY_LEFT:
			case Status.STAY_RIGHT:

				//前半フェイズ :　ステイ(STAY_LEFT, STAY_RIGHT)
				//後半フェイズ :　ステイ(STAY_LEFT, STAY_RIGHT) or unko(UNKO_BLOW)

				isAnim = false;
				//タワーにつかまっている時間
				stayTime = Random.Range(minStayTime, maxStayTime);

				break;
			case Status.ROOLLING:

				//移動
				isAnim = false;

				List<Root> select = (phase == Phase.FIRST_HALF) ? roots : laterRoots;

				if(nowPointIndex == 4)
				{
					nextPointIndex = 0;
					Vector3 nowPos = points[nowPointIndex].pos;
					Vector3 nextPos = points[nextPointIndex].pos;
					float nowScale = points[nowPointIndex].scale;
					float nextScale = points[nextPointIndex].scale;

					//移動イージング
					var r = 0;
					bool ttakai = nowPos.y < nextPos.y;
					StartCoroutine(Easing.Tween(4, (t) =>
					{
						var x = Mathf.Lerp(nowPos.x, nextPos.x, t);
						var y = ttakai ? nowPos.y + (-nowPos.y + nextPos.y) * curve.Evaluate(t) :
						nextPos.y + (-nextPos.y + nowPos.y) * curve.Evaluate(1 - t);

						transform.localPosition = new Vector3(x, y, Mathf.Lerp(nowPos.z, nextPos.z, t));
						transform.localRotation = Quaternion.Euler(0, 0, (r += 30) * -1);
						transform.localScale = Vector3.one * Mathf.Lerp(nowScale, nextScale, t);

						if (t >= 1.0f)
						{
							isActionEnd = true;
						}
					}));

					break;
				}

				foreach (var root in select)
				{
					if (nowPointIndex != root.p) continue;

					//移動位置を候補からランダムで取得
					int selectIndex = Random.Range(0, root.takeTimes.Count);
					//ポイントID、移動時間を取得
					TakeTime next = root.takeTimes[selectIndex];
					nextPointIndex = next.p;

					Vector3 nowPos = points[nowPointIndex].pos;
					Vector3 nextPos = points[nextPointIndex].pos;
					float nowScale = points[nowPointIndex].scale;
					float nextScale = points[nextPointIndex].scale;

					//移動イージング
					var r = 0;
					bool ttakai = nowPos.y < nextPos.y;
					StartCoroutine(Easing.Tween(next.time, (t) =>
					{
						var x = Mathf.Lerp(nowPos.x, nextPos.x, t);
						var y = ttakai ? nowPos.y + (-nowPos.y + nextPos.y) * curve.Evaluate(t) :
						nextPos.y + (-nextPos.y + nowPos.y) * curve.Evaluate(1 - t);

						transform.localPosition = new Vector3(x, y, Mathf.Lerp(nowPos.z, nextPos.z, t));
						transform.localRotation = Quaternion.Euler(0, 0, (r += 30) * next.rotateDirection);
						transform.localScale = Vector3.one * Mathf.Lerp(nowScale, nextScale, t);

						if (t >= 1.0f)
						{
							isActionEnd = true;
						}
					}));

					break;
				}

				break;
			case Status.CLIMB_SCRATCH:

				//よじ登り引っかきモーション
				//引っかき途中で撃たれた場合怯みモーション移行

				tumeKougekiNum++;
				if (tumeKougekiNum == maxTumekougeki)
					phase = Phase.LATTER_HALF;
				var g = Instantiate(menoaeGorira);
				g.GetComponent<GoriraAppearance>().bossManager = this;

				break;
			case Status.STAGGER:

				//怯みモーション
				break;
			case Status.UNKO_BLOW_LEFT:
			case Status.UNKO_BLOW_RIGHT:

				//unko投げるモーション
				isAnim = true;
				StartCoroutine(Easing.Tween(anims[status].takeTime, (t) =>
				{
					if (animIndex == 1 && isUnko == false)
					{
						isUnko = true;
						//unko投げる関数
						Instantiate(unko);
					}

					if (t >= 1.0f)
					{
						isActionEnd = true;
					}
				}));

				break;

			case Status.START_ACTION:

				isAnim = true;
				//スタート時のアクション
				break;
			case Status.FHASE_CHANGE_ATION:

				isAnim = true;
				//フェイズ変更時のアクション（ドラミング）
				break;
			case Status.END_ACTION:

				isAnim = true;
				//最後のアクション（なつみを投げる）
				break;
		}
	}

	public void UpdateAnimation()
	{
		if (!isAnim) return;
		if (isActionEnd) return;

		animTime += Time.deltaTime;
		if (animTime < anims[status].times[animIndex]) return;

		animTime = 0.0f;
		animIndex++;
		spriteR.sprite = anims[status].sprites[animIndex];
		isAnim = false;
	}
}
