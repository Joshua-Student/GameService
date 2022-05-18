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

        //[HttpGet("{id}")]
        //// GET: GameController/Details/5
        //public ActionResult Details(int id)
        //{
        //    ReturnObject ro = new ReturnObject
        //    {
        //        Valid = true,
        //        Message = "Game started"
        //    };
        //    //return $"{id}";
        //    return new JsonResult(ro);
        //}

        // GET: GameController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("NewGame")]
        //[ValidateAntiForgeryToken]
        public ActionResult NewGame([FromBody] GameSettings s)
        {   
            //_manager.SetGame(s.Game, (bool)s.Machine);
            return new JsonResult(_manager.StartGame(s.Game, (bool)s.Machine));
        }

        // POST: GameController/Create
        [HttpPost("Post")]
        //[ValidateAntiForgeryToken]
        public ActionResult Post([FromBody] GameInput item)
        {
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}

            
            return new JsonResult(_manager.TakeTurn(item.Game,item.Move));
        }

        //// GET: GameController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: GameController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: GameController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: GameController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
