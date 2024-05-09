using UnityEngine;

namespace Source.Scripts.MonoBehaviours
{
    public abstract class MoneyManager 
    {
        private const string PointsKey = "PlayerMoney";
        
        public static void SavePoints(int pointsNumber)
        {
            PlayerPrefs.SetInt(PointsKey, pointsNumber);
            PlayerPrefs.Save();
        }
        
        public static int LoadPoints()
        {
            return PlayerPrefs.GetInt(PointsKey, 0); 
        }
    }
}