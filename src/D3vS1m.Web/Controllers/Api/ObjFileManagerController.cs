﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using D3vS1m.Application;
using D3vS1m.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjFileManagerController : ApiControllerBase
    {
        public ObjFileManagerController(IHostingEnvironment env, FactoryBase factory) : base(env, factory)
        {
        }

        // GET: api/<controller>
        [HttpGet]
        public JsonResult Get()
        {
            // Check Current File 
            var currentFolderStr = Path.Combine(Directory.GetCurrentDirectory(), "data" );
            var listItemNames = Directory.GetFileSystemEntries(currentFolderStr);
            var hostStr = HttpContext.Request.Host.ToString();
            var files = new List<FileModel>();
            foreach (var listItemName in listItemNames)
            {
                var fileModel = new FileModel();
                fileModel.FileName = Path.GetFileNameWithoutExtension(listItemName);

                fileModel.Link = hostStr + "/data/" + Path.GetFileName(listItemName);
                files.Add(fileModel);
            }

            return new JsonResult(files);
        }


        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpPost("uploadfile")]
        public async Task<JsonResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new JsonResult("");
            var getCurrentFolder = Directory.GetCurrentDirectory();
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "data",
                file.FileName);
            var fileInfo = new FileModel();

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                var hostStr = HttpContext.Request.Host.ToString();
                fileInfo.FileName = file.FileName;
                fileInfo.Link = hostStr + "/data/" + file.FileName;
            }


            return new JsonResult(fileInfo);
        }
    }
}