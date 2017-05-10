using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using SampleMvc.Board.Data;
using SampleMvc.Board.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMvc.Board.Controllers
{
    [Area("Board")]
    public class DocumentsController : Microsoft.AspNetCore.Mvc.Controller
    {
        IDocumentRepository _repo;

        public DocumentsController(IDocumentRepository repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var documents = _repo.Search();
            return View(documents);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var document = _repo.GetDocumentById(id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var entity = new Document();
            if (id.HasValue)
            {
                entity = _repo.GetDocumentById(id.Value);
            }
            return View(entity);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind("Id,Name,Title,Content")] Document entity)
        {
            if (ModelState.IsValid)
            {
                var ipAddress = Request.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();


                if (String.IsNullOrEmpty($"{RouteData.Values["id"]}"))
                {
                    entity.PostIp = ipAddress;
                    entity.ModifyIp = ipAddress;
                    _repo.AddDocument(entity);
                }
                else
                {
                    entity.ModifyIp = ipAddress;

                    _repo.UpdateDocument(entity);
                }

                return RedirectToAction("Detail", new { id = entity.Id });
            }

            return View(entity);
        }
    }
}
