using ChessTrainer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChessGame;
using System.Text;

namespace ChessTrainer.Controllers
{

    public class HomeController : Controller
    {

        ChessGame.ApplicationController ChessApp= new ChessGame.ApplicationController();
        
      
        BoardModel MyBoard= new BoardModel();
        public ActionResult Index()
        {
      
            ChessApp.SetVerbose(false);
            ChessApp.SetXMLOutput(false);
          
            MyBoard.InitialPosition();
            ScoreSheetModel Smodel = new ScoreSheetModel();
            ViewBag.ScoreSheet = Smodel;


            return View(MyBoard);
        }

        public JsonResult LoadData()
        {
            string sessionId = Session.SessionID;
            ScoreSheet Sh = Session[$"ScoreSheet_{sessionId}"] as ScoreSheet;
            var result = new
            {
                success = true,
                WhiteMobility = Sh.WhiteMobility(),
                BlackMobility = Sh.BlackMobility(),
                WhiteValue = Sh.WhiteValue(),
                BlackValue = Sh.BlackValue()
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadPGN()
        {
            try
            {
                ScoreSheet Sh = null;
                if (Request.Files.Count == 0)
                {
                    return Json(new { success = false, message = "No file uploaded." });
                }

                HttpPostedFileBase file = Request.Files[0];

                if (Path.GetExtension(file.FileName).ToLower() != ".pgn")
                {
                    return Json(new { success = false, message = "Only PGN files are allowed." });
                }

                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(file.InputStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                }

                ChessApp.SetVerbose(false);
                ChessApp.SetXMLOutput(false);
                ChessApp.Load(sb);
                List<Board> lB = ChessApp.GetTheGame();
                Sh = ChessApp.GetScoreSheet();
          
                string sessionId = Session.SessionID;
                Session[$"BoardStates_{sessionId}"] = lB;
                Session[$"CurrentIndex_{sessionId}"] = -1;
                Session[$"ScoreSheet_{sessionId}"] = Sh;

                MyBoard.SetGame(lB);

                var result = new
                {
                    success = true,
                    WhiteMobility = Sh.WhiteMobility(),
                    BlackMobility = Sh.BlackMobility(),
                    WhiteValue = Sh.WhiteValue(),
                    BlackValue = Sh.BlackValue(),
                    Moves=Sh.GetMovesAnnotation(),
                    Metadata = new
                    {
                        Tournament = (Sh != null) ? Sh.Tournament : "",
                        TournamentDate = (Sh != null) ? Convert.ToString(Sh.UTCDate) : "",
                        Score = (Sh != null) ? Sh.Score : "",
                        WhitePlayer = (Sh != null) ? Sh.WhitePlayer : "",
                        BlackPlayer = (Sh != null) ? Sh.BlackPlayer : "",
                        WhitePlayerELO = (Sh != null) ? Sh.WhitePlayerELO : 0,
                        BlackPlayerELO = (Sh != null) ? Sh.BlackPlayerELO : 0
                    }

                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Server error: " + ex.Message });
            }
        }
        public JsonResult NextMove()
        { 
            return MakeMove(1);
        }

        public JsonResult PreviousMove()
        {
            return MakeMove(-1);
        }
        public JsonResult Gotostart()
        {
            string sessionId = Session.SessionID;
            Session[$"CurrentIndex_{sessionId}"] = 0;
         
            return MakeMove(0);
        }
        public JsonResult Gotoend()
        {
                string sessionId = Session.SessionID;
                List<Board> lB = Session[$"BoardStates_{sessionId}"] as List<Board>;
               
                if (lB is object)
                { 
                    Session[$"CurrentIndex_{sessionId}"] = lB.Count - 1;
                }

            return MakeMove(0);
        }

        [HttpPost]
        public JsonResult SetSelectedMove(int moveIndex)
        {
            // Save the selected move (e.g., in session, database, etc.)
            Session["SelectedMove"] = moveIndex;

            return MakeMove(moveIndex);
        }

        [HttpPost]
        public JsonResult ClickMouseMove()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                  

                
                    string requestBody = reader.ReadToEnd();
                    int ID = 0;
                    dynamic array = Newtonsoft.Json.JsonConvert.DeserializeObject(requestBody);
                    foreach (var item in array)
                    {
                        ID = Convert.ToInt32(item.Value);
                    }

              
                

                    string sessionId = Session.SessionID;
                    List<Board> lB = Session[$"BoardStates_{sessionId}"] as List<Board>;

                   
                    Session[$"CurrentIndex_{sessionId}"] = ID;
                   
                    // Your logic here (return updated board and move ID)
                    return MakeMove(1);
                }
            }
            catch
            {
                return Json(new { success = false, message = "Invalid request" });
            }
        }
        private JsonResult MakeMove(int i)
        {
            Response.ContentType = "application/json";
            string sessionId = Session.SessionID;
            object result=null;
            List<Board> lB = Session[$"BoardStates_{sessionId}"] as List<Board>;
            ScoreSheet Sh = Session[$"ScoreSheet_{sessionId}"] as ScoreSheet;
            string move = "";
            if (lB is object)
            {
                int currentIndex = Session[$"CurrentIndex_{sessionId}"] != null ? (int)Session[$"CurrentIndex_{sessionId}"] : 0;
                    
                


                if ((currentIndex + i < lB.Count) && (currentIndex + i > -1))
                    currentIndex += i;
               
                    Session[$"CurrentIndex_{sessionId}"] = currentIndex;
                    MyBoard.SetGame(lB);
                    MyBoard.SetMove(currentIndex);
                  
              if (currentIndex % 2 == 0)
                        move = "White" + (currentIndex/2).ToString();
                    else
                        move = "Black" + (currentIndex/2).ToString();
            }
            else
            {
                MyBoard.InitialPosition();
            }
            if (Sh is object)
            {
                ScoreSheetModel Smodel = new ScoreSheetModel(Sh);
                ViewBag.ScoreSheet = Smodel;

                // Render the updated chessboard as an HTML string
                string boardHtml = RenderPartialViewToString("_Board", MyBoard);
                
                result = new
                {
                    success = true,
                    boardHtml = boardHtml,
                    ID= move,
                    WhiteMobility = (Sh != null && Sh.WhiteMobility().Any()) ? Sh.WhiteMobility().First() : 0,
                    BlackMobility = (Sh != null && Sh.BlackMobility().Any()) ? Sh.BlackMobility().First() : 0,
                    WhiteValue = (Sh != null && Sh.WhiteValue().Any()) ? Sh.WhiteValue().First() : 0,
                    BlackValue = (Sh != null && Sh.BlackValue().Any()) ? Sh.BlackValue().First() : 0,
                   
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // Utility function to render a partial view to a string
        private string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

      

    }
}