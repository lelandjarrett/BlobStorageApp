using System;
using System.IO;
using System.Threading.Tasks;
using BlobStorage.Data;
using BlobStorage.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BlobSampleApp.Controllers
{
    public class BlobFilesController : Controller
    {
        private readonly IBlobService _blobService;
        private readonly IConfiguration config;

        public BlobFilesController(IBlobService blobService, IConfiguration _config)
        {
            this._blobService = blobService;
            this.config = _config;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var files = await _blobService.AllBlobs(config.GetValue<string>("BlobContainer"));
            return View(files);
        }

        [HttpGet]
        public IActionResult AddFile()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            if (file == null || file.Length < 1) return View();

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var res = await _blobService.UploadBlob(fileName, file, config.GetValue<string>("BlobContainer"));

            if (res)
                return RedirectToAction("Index");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewFile(string name)
        {
            var res = await _blobService.GetBlob(name, config.GetValue<string>("BlobContainer"));
            return Redirect(res);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFile(string name)
        {
            await _blobService.DeleteBlob(name, config.GetValue<string>("BlobContainer"));
            return RedirectToAction("Index");
        }
    }
}