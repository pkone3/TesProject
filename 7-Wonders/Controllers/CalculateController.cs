using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SevenWonders.Models;
using SevenWonders.Classes;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SevenWonders.Controllers
{
    public class CalculateController : Controller
    {

        public IActionResult Index()
        {
            if (Persistent.Wonders.Count == 0)
            {
                //Load the Wonders list if it is empty
                string value = "{\"Wonders\": [{\"id\": \"0\", \"name\": \"The Colossus of Rhodes\" },{\"id\": \"1\", \"name\": \"The Lighthouse of Alexandria\"}, {\"id\": \"2\", \"name\": \"The Temple of Artemis in Ephesus\"},{\"id\": \"3\", \"name\": \"The Hanging Gardens of Babylon\"}, {\"id\": \"4\", \"name\": \"The Statue of Zeus in Olympia\"},{\"id\": \"5\", \"name\": \"The Mausoleum of Halicarnassus\"},{\"id\": \"6\", \"name\": \"The Pyramids of Giza\"}]}";
                var result = JsonConvert.DeserializeObject<WonderList>(value);
                foreach (Wonder w in result.Wonders)
                {
                    Persistent.Wonders.Add(w);
                }
            }
            return View(Persistent.PlayerList);
        }

        public IActionResult AddPlayer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add()
        {
            if (ModelState.IsValid)
            {
                int counter = Persistent.PlayerList.Count();
                Persistent.PlayerList.Add(new Player { id = counter + 1, name = Request.Form["name"].ToString() });
            }
            ViewData["Message"] = "Player Added Successfully";
            return View("AddPlayer");
        }

        public ActionResult DeletePlayer(int id)
        {
            Player player = Persistent.PlayerList.Find(i => i.id == id);
            Persistent.PlayerList.Remove(player);
            return RedirectToAction("Index");
        }

        public IActionResult CalculateList()
        {
            //ViewData["Message"] = "Your contact page.";

            return View(Persistent.PlayerList);
        }

        public IActionResult AddScores(int id)
        {
            Player player = Persistent.PlayerList.Find(i => i.id == id);
            ViewBag.ddWonders = new SelectList(Persistent.Wonders, "id", "name");
            return View(player);
        }

        [HttpPost]
        public ActionResult UpdateScores()
        {
            Player p = Persistent.PlayerList.Find(i => i.id == int.Parse(Request.Form["id"]));

            //Populate the model
            p.victoryTokens = Tools.ConvertToInt(Request.Form["victoryTokens"]);
            p.defeatTokens = Tools.ConvertToInt(Request.Form["defeatTokens"]);
            p.coins = Tools.ConvertToInt(Request.Form["coins"]);
            p.isWonderSideA = (Request.Form["isWonderSideA"] == "false") ? false : true;
            p.wonderStages = Tools.ConvertToInt(Request.Form["wonderStages"]);
            p.wonderId = Tools.ConvertToInt(Request.Form["ddWonders"]);
            Wonder w = Persistent.Wonders.Find(i => i.id == p.wonderId.ToString());
            p.wonderName = w.name;
            p.civilianPoints = Tools.ConvertToInt(Request.Form["civilianPoints"]);
            p.commercialPoints = Tools.ConvertToInt(Request.Form["commercialPoints"]);
            p.gearCards = Tools.ConvertToInt(Request.Form["gearCards"]);
            p.protractorCards = Tools.ConvertToInt(Request.Form["protractorCards"]);
            p.tabletCards = Tools.ConvertToInt(Request.Form["tabletCards"]);
            p.hasSpies = (Request.Form["hasSpies"] == "false") ? false : true;
            p.hasMagistrates = (Request.Form["hasMagistrates"] == "false") ? false : true;
            p.hasWorkers = (Request.Form["hasWorkers"] == "false") ? false : true;
            p.hasCraftsmans = (Request.Form["hasCraftsmans"] == "false") ? false : true;
            p.hasTraders = (Request.Form["hasTraders"] == "false") ? false : true;
            p.hasPhilosophers = (Request.Form["hasPhilosophers"] == "false") ? false : true;
            p.hasBuilders = (Request.Form["hasBuilders"] == "false") ? false : true;
            p.hasShipOwners = (Request.Form["hasShipOwners"] == "false") ? false : true;
            p.hasStrategists = (Request.Form["hasStrategists"] == "false") ? false : true;
            p.hasScientists = (Request.Form["hasScientists"] == "false") ? false : true;
           
            //Determine which additional values to move over. Don't move values when they don't have the guild
            p.Spies = (p.hasSpies) ? Tools.ConvertToInt(Request.Form["Spies"]) : 0;
            p.Magistrates = (p.hasMagistrates) ? Tools.ConvertToInt(Request.Form["Magistrates"]) : 0;
            p.Workers = (p.hasWorkers) ? Tools.ConvertToInt(Request.Form["Workers"]) : 0;
            p.Craftsmans = (p.hasCraftsmans) ? Tools.ConvertToInt(Request.Form["Craftsmans"]) : 0;
            p.Traders = (p.hasTraders) ? Tools.ConvertToInt(Request.Form["Traders"]) : 0;
            p.Philosophers = (p.hasPhilosophers) ? Tools.ConvertToInt(Request.Form["Philosophers"]) : 0;
            p.Builders = (p.hasBuilders) ? Tools.ConvertToInt(Request.Form["Builders"]) : 0;
            p.ShipOwners = (p.hasShipOwners) ? Tools.ConvertToInt(Request.Form["ShipOwners"]) : 0;
            p.Strategists = (p.hasStrategists) ? Tools.ConvertToInt(Request.Form["Strategists"]) : 0;

            return RedirectToAction("CalculateList");
        }

        public IActionResult FinalScores()
        {
            //Clear out all scores
            Persistent.PlayerScores.RemoveAll(i => i.id > 0);

            //Score the players
            foreach(Player player in Persistent.PlayerList)
            { 
                Scores PlayerScore = new Scores();
                int TotalScore = 0;
                TotalScore = Tools.ScorePlayer(player);

                //Add player score record
                PlayerScore.id = player.id;
                PlayerScore.name = player.name;
                PlayerScore.score = TotalScore;

                Persistent.PlayerScores.Add(PlayerScore);
            }

            //determine the victor
            int position = 1;
            foreach (Scores player in Persistent.PlayerScores.OrderByDescending(s => s.score))
            {
                player.position = position;
                position++;
            }

            return View(Persistent.PlayerScores.OrderBy(s => s.position));
        }

        public ActionResult Reset()
        {
            Persistent.PlayerList.RemoveAll(p => p.id > 0);
            Persistent.PlayerScores.RemoveAll(p => p.id > 0);
            return RedirectToAction("Index");
        }


    }
}
