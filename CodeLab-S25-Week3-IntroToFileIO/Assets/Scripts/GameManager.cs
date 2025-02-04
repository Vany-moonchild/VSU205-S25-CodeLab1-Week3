using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    private int score;

    //property of score (done with the capital "S" in score) - it knows Score is for score
    public int Score
    {   //set or get can be interchanged in order - typically done with get on top OR the bigger one on top 
        set
        {
            score = value;
            Debug.Log("New Score: " + score);
            
            if (targetScore == score)
            {
                targetScore *= 3;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (score > HighScore)
            {
                HighScore = score;
            }
            
            displayText.text = "Score: " + score + " High Score: " + HighScore;

        }        
        get
        {
            highScore = PlayerPrefs.GetInt(KeyHighScore, 0);
            return score;
        }
    }
    
    private const string KeyHighScore = "HIGH_SCORE";

    private int highScore;

    //property for highScore
    public int HighScore
    {
        set
        {
            highScore = value;

            if (!File.Exists(filePathHighScore))
            {
                string dirLocation = Application.dataPath + DirName + FileName;
                if (!Directory.Exists(filePathHighScore))
                {
                    Directory.CreateDirectory(dirLocation);
                }
            }
            
            File.WriteAllText(filePathHighScore, score + "");
            //PlayerPrefs.SetInt(KeyHighScore, highScore);
        }        
        get
        {
            if (File.Exists(filePathHighScore))
            {
                string fileContents = File.ReadAllText(filePathHighScore);
                
                score = int.Parse(fileContents);
            }
            //highScore = PlayerPrefs.GetInt(KeyHighScore, 0);
            return highScore;
        }
    }

    string filePathHighScore;
    const string DirName = "/Data/";
    const string FileName = DirName + "highScore.txt";
    
    
    public int targetScore = 3;
    
    TextMeshProUGUI displayText;
    
    //the static instance that holds the sole object of this Singleton
    public static GameManager instance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
        Vector3 newVec = UtilScript.CloneVec3(Vector3.down);
        
        //check to see if someone has set the instance
        if (instance == null)
        {
            //if they haven't this is the instance
            instance = this;
            //and keep it around in other scenes
            DontDestroyOnLoad(this);
            
            displayText = GameObject.Find("DisplayText").GetComponent<TextMeshProUGUI>();
            
            filePathHighScore = Application.dataPath + FileName;
            Debug.Log(Application.dataPath);
            
            //to show the UI in the beginning 
            Score = 0;
        }
        else //otherwise, if there is an existing instance
        {
            //destroy the new instance that was just created
            Destroy(gameObject);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(score);


    }

    public void UpdateScore()
    {
        
    }
}
