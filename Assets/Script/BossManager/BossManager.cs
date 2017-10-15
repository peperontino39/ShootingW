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
	CLIMB,      //手前ビルよじ登り
    SCRATCH,    //ひっかき攻撃
    STAGGER,    //ひっかき攻撃時にダメージを受けたときのひるみ
    DRUMMING,   //ゴリラのドラミング
    NATUMI_BLOW,//なつみ投げ（ラスト演出）
    NONE
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
    }

    public struct Root
    {
        public int p;
        public List<TakeTime> takeTimes;
    }

    public string pointDataFileName = "data/BossPoint.csv";
    public string nextPointFileName = "data/BossNextPoint.csv";
    
    public List<Point> points = new List<Point>();
    public List<Root> roots = new List<Root>();
    public int nowPointIndex = 0;
    public int nextPointIndex = 0;
    public bool isActionEnd = false;
    public Status status = Status.NONE;
	public Phase phase = Phase.FIRST_HALF;
	public const float maxStayTime = 1.0f;
	public const float minStayTime = 0.5f;
	public float stayTime = 0.0f;

    void Start()
    {
        SetupPoint();
        SetupTakeTimes();
    }

    void SetupPoint()
    {
        string currentPath = Directory.GetCurrentDirectory() + "/";

        Debug.Log(currentPath);

        using (StreamReader sr = new StreamReader(currentPath + pointDataFileName))
        {
            sr.ReadLine();
            while(sr.Peek() > -1)
            {
                string[] data = sr.ReadLine().Split(',');
                Point p = new Point();
                p.pos = new Vector3(
                    float.Parse(data[0]),
                    float.Parse(data[1]),
                    float.Parse(data[2]));
                p.scale = float.Parse(data[3]);
            }
        }
    }

    void SetupTakeTimes()
    {
        string currentPath = Directory.GetCurrentDirectory() + "/";
        using (StreamReader sr = new StreamReader(currentPath + nextPointFileName))
        {
            sr.ReadLine();
            while(sr.Peek() > -1)
            {
                string[] data = sr.ReadLine().Split(',');
                Root r = new Root();
                r.takeTimes = new List<TakeTime>();
                r.p = int.Parse(data[0]);

                for(int i = 1; i < data.Length; i += 2)
                {
                    TakeTime takeTime = new TakeTime();
                    takeTime.p = int.Parse(data[i]);
                    takeTime.time = float.Parse(data[i + 1]);

                    r.takeTimes.Add(takeTime);
                }

                roots.Add(r);
            }
        }
    }

    void Update()
    {
        if(isActionEnd)
        {
            DecideModePoint();
        }

		if (stayTime > 0.0f)
		{
			stayTime = Mathf.Max(0.0f, stayTime - Time.deltaTime);
			if(stayTime <= 0.0f)
			{
				status = Status.ROOLLING;
				nextPointIndex = nowPointIndex + 1;
			}
		}
    }

    public void DecideModePoint()
    {


        switch (status)
        {
            case Status.STAY_LEFT:
            case Status.STAY_RIGHT:

                //移動

                foreach (var root in roots)
                {
                    if (nowPointIndex != root.p) continue;

                    //移動位置を候補からランダムで取得
                    int selectIndex = Random.Range(0, root.takeTimes.Count - 1);
                    //ポイントID、移動時間を取得
                    TakeTime next = root.takeTimes[selectIndex];
                    nextPointIndex = next.p;

                    Vector3 nowPos = points[nowPointIndex].pos;
                    Vector3 nextPos = points[nextPointIndex].pos;
                    Vector3 distance = nextPos - nowPos;

                    Easing.Tween(next.time, (t) => {
                        float value = Easing.InOutBack(t);
                        Vector3 pos = Vector3.Lerp(nowPos, nextPos, value);
                        });

                    break;
                }

                break;
            case Status.ROOLLING:

                //前半フェイズ :　ステイ(STAY_LEFT, STAY_RIGHT)
                //後半フェイズ :　ステイ(STAY_LEFT, STAY_RIGHT) or unko(UNKO_BLOW)

				if(phase == Phase.FIRST_HALF)
				{
					switch (nowPointIndex)
					{
						case 0:
							break;
						case 1:
						case 2:
						case 3:
							//タワーにつかまっている時間
							stayTime = Random.Range(minStayTime, maxStayTime);

							break;
						
						case 4:
							break;

						case 5:
							break;
					}
				}

                break;
            case Status.CLIMB:


                break;
            case Status.SCRATCH:
                break;
            case Status.STAGGER:
                break;
            case Status.DRUMMING:
                break;
			case Status.UNKO_BLOW_LEFT:
				break;
            case Status.UNKO_BLOW_RIGHT:
                break;
            case Status.NATUMI_BLOW:
                break;
        }
    }

    public void UpdateAction()
    {
        switch(status)
        {
            case Status.STAY_LEFT:
                break;
            case Status.STAY_RIGHT:
                break;
            case Status.ROOLLING:
                break;
            case Status.CLIMB:
                break;
            case Status.SCRATCH:
                break;
            case Status.STAGGER:
                break;
            case Status.DRUMMING:
                break;
			case Status.UNKO_BLOW_LEFT:
				break;
            case Status.UNKO_BLOW_RIGHT:
                break;
            case Status.NATUMI_BLOW:
                break;
        }
    }
}
