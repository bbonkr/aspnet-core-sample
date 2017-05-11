using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using SampleMvc.Board.Data;
using SampleMvc.Board.Models;
using System;
using System.Collections.Generic;
using System.Text;

using SampleMvc.Lib;

namespace SampleMvc.Board.Controllers
{
    [Area("Board")]
    public class DocumentsController : Controller
    {
        IDocumentRepository _repo;

        public DocumentsController(IDocumentRepository repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public IActionResult Index(string keyword = "", int? page = 1)
        {
            int pageSize = 10;
            var documents = _repo.Search(searchKeyword: keyword);
            ViewData["page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["keyword"] = keyword;
            return View(documents.ToPagedList(pageSize, page ?? 1));
        }

        [HttpGet]
        public IActionResult Detail(int id, int? page = 1, string keyword = "")
        {
            var document = _repo.GetDocumentById(id);

            if (document == null)
            {
                return NotFound();
            }

            //ViewData["routeData"] = new Dictionary<string, string>
            //{
            //    ["page"] = $"{page}",
            //    ["keyword"] = keyword
            //};

            ViewData["page"] = $"{page}";
            ViewData["keyword"] = keyword;

            return View(document);
        }

        [HttpGet]
        public IActionResult Edit(int? id, int? page = 1, string keyword = "")
        {
            var entity = new Document();
            if (id.HasValue)
            {
                entity = _repo.GetDocumentById(id.Value);
                ViewData["id"] = ((id ?? 0) == 0) ? String.Empty : $"{id ?? 0}";
            }

            ViewData["page"] = $"{page}";
            ViewData["keyword"] = keyword;

            return View(entity);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Save([Bind("Id,Name,Title,Content")] Document entity, int? page = 1, string keyword = "")
        {
            ViewData["page"] = $"{page}";
            ViewData["keyword"] = keyword;

            if (ModelState.IsValid)
            {
                var ipAddress = Request.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();


                if (String.IsNullOrEmpty($"{RouteData.Values["id"]}"))
                {
                    entity.PostDate = DateTime.Now;
                    entity.PostIp = ipAddress;
                    entity.ModifyDate = DateTime.Now;
                    entity.ModifyIp = ipAddress;
                    _repo.AddDocument(entity);
                }
                else
                {
                    var oEntity = _repo.GetDocumentById(entity.Id);

                    //oEntity.PostDate = DateTime.Now;
                    //oEntity.PostIp = ipAddress;

                    oEntity.ModifyDate = DateTime.Now;
                    oEntity.ModifyIp = ipAddress;
                    oEntity.Title = entity.Title;
                    oEntity.Name = entity.Name;
                    oEntity.Content = entity.Content;

                    _repo.UpdateDocument(oEntity);
                }

                return RedirectToAction("Detail", new { id = entity.Id, page = page, keyword = keyword });
            }

            return View(entity);
        }

        [HttpGet]
        public IActionResult Delete(int id){
            var entity = new Document();
            entity = _repo.GetDocumentById(id);
            
            if(entity == null){
                return NotFound();
            }

            return View(entity);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteDocument(int id)
        {
            var entity = _repo.GetDocumentById(id);
            if (entity == null)
            {
                return NotFound();
            }

            _repo.DeleteDocument(entity);

            return RedirectToAction("Index");
        }
    }
}
