using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhScore : UbhMonoBehaviour
{
	private const string HIGH_SCORE_KEY = "highScoreKey";

	private const string HIGH_SCORE_TITLE = "HighScore : ";

	[FormerlySerializedAs("_DeleteScore"), SerializeField]
	private bool m_deleteScore;

	[FormerlySerializedAs("_ScoreGUIText"), SerializeField]
	private GUIText m_scoreGUIText;

	[FormerlySerializedAs("_HighScoreGUIText"), SerializeField]
	private GUIText m_highScoreGUIText;

	private int m_score;

	private int m_highScore;

	private void Start()
	{
		this.Initialize();
	}

	private void Update()
	{
		if (this.m_highScore < this.m_score)
		{
			this.m_highScore = this.m_score;
		}
		this.m_scoreGUIText.text = this.m_score.ToString();
		this.m_highScoreGUIText.text = "HighScore : " + this.m_highScore.ToString();
	}

	public void Initialize()
	{
		if (this.m_deleteScore)
		{
			PlayerPrefs.DeleteAll();
		}
		this.m_score = 0;
		this.m_highScore = PlayerPrefs.GetInt("highScoreKey", 0);
	}

	public void AddPoint(int point)
	{
		this.m_score += point;
	}

	public void Save()
	{
		PlayerPrefs.SetInt("highScoreKey", this.m_highScore);
		PlayerPrefs.Save();
		this.Initialize();
	}
}
