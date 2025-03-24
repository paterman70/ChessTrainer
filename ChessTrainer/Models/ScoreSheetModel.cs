using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChessGame;

namespace ChessTrainer.Models
{
    public class ScoreSheetModel
    {
        string score = "";
        string site = "";
        string Player1 = "";
        string Player2 = "";
        string tournament = "";
        string round = "";
        string timecontrol = "";
        int Player1Elo = 0;
        int Player2Elo = 0;
        DateTime date;
        DateTime Player1EndingTime;
        DateTime Player2EndingTime;
        List<Move> Sheet;
        private List<int> WMobility = new List<int>();
        private List<int> BMobility = new List<int>();
        private List<int> WValue = new List<int>();
        private List<int> BValue = new List<int>();
        public ScoreSheetModel(ScoreSheet sh)
        {
           score=sh.Score;
            site = sh.Site;
            Player1 = sh.WhitePlayer;
            Player2 = sh.BlackPlayer;
            tournament = sh.Tournament;
            round = sh.Round;
            timecontrol = sh.TimeControl;
            Player1Elo = sh.WhitePlayerELO;
            Player2Elo = sh.BlackPlayerELO;
            date = sh.UTCDate;
            Player1EndingTime = sh.WhitePlayerTime;
            Player2EndingTime = sh.BlackPlayerTime;
            Sheet = new List<Move>();
            Sheet=sh.MoveRange(1, sh.NumberOfMoves());
            WMobility = sh.WhiteMobility();
            BMobility = sh.BlackMobility();
            WValue = sh.WhiteValue();
            BValue = sh.BlackValue();

        }

        public ScoreSheetModel()
        {

        }
        public string Site   // property
        {
            get { return site; }   // get method
        }
        public string Score   // property
        {
            get { return score; }   // get method
       }
        public string WhitePlayer   // property
        {
            get { return Player1; }   // get method
       }
        public string BlackPlayer   // property
        {
            get { return Player2; }   // get method
       }
        public string Tournament  // property
        {
            get { return tournament; }   // get method
        }
        public string Round   // property
        {
            get { return round; }   // get method
         }

        public string TimeControl   // property
        {
            get { return timecontrol; }   // get method
         }
        public int WhitePlayerELO   // property
        {
            get { return Player1Elo; }   // get method
        }
        public int BlackPlayerELO   // property
        {
            get { return Player2Elo; }   // get method
        }

        public DateTime TournamentDate   // property
        {
            get { return date; }   // get method
        }
        public DateTime WhitePlayerTime   // property
        {
            get { return Player1EndingTime; }   // get method
       }
        public DateTime BlackPlayerTime   // property
        {
            get { return Player2EndingTime; }   // get method
        }

        public List<Move> GetMoves()
        {

            return Sheet;
        }
        public List<int> WhiteMobility()
        {
            
            return WMobility;
        }

        public List<int> BlackMobility()
        {
            return BMobility;
        }
        public List<int> WhiteValue()
        {
            return WValue;
        }

        public List<int> BlackValue()
        {
            return BValue;
        }

    }
}