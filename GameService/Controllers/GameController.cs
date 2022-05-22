using GameService.Models;
using GameService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameService.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : Controller
    {
        private readonly IManager _manager;

        public GameController(IManager manager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        [HttpGet]
        // GET: GameController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost("NewGame")]
        //[ValidateAntiForgeryToken]
        public ActionResult NewGame([FromBody] GameSettings s)
        {   
            return new JsonResult(_manager.StartGame(s.Game, (bool)s.Machine));
        }

        [HttpPost("TakeTurn")]
        //[ValidateAntiForgeryToken]
        public ActionResult Post([FromBody] GameInput item)
        {   
            return new JsonResult(_manager.TakeTurn(item.Game,item.Move));
        }

    }
}
