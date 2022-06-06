using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using UpLoadZip.Web.Models;
using UpLoadZip.Web.Services;

namespace UpLoadZip.Web.Controllers
{
    public class HomeController : ApiController
    {
        ZipServices zipservice = new ZipServices();

        [System.Web.Http.HttpGet]
        [Route("api/ZipAPI/testGet")]
        public string TestGet()
        {
            return "-Get: OK-";
        }

        [System.Web.Http.HttpPost]
        [Route("api/ZipAPI/testPost")]
        public string TestPost()
        {
            return "-Post: OK-";
        }

        /*
         * Исправления:
         *  - Навел чистоту в контроллере
         *  - методы GetStruct и DeleteFile - принимают адрес файла строкой
         *  - удалил работу сторонних библиотек
         *  - сделал Upload во временную папку
         *  - увелилчил доступный размер файла до 1 гб - проверял на файле около 800мб - оно же и проверка на размер файла
         *  - переделал работу с папками архива. Теперь использую только 1 foreach
         *  - добавил проверки на корректность структуры архива
         */

        [System.Web.Http.HttpPost]
        [Route("api/ZipAPI/UploadZipFile")]
        public async Task<List<ZipStructModel>> UpLoadFileAsync()
        {
            var httpRequest = HttpContext.Current.Request.Files;
            return await zipservice.UpLoadZip(httpRequest);
        }
    }
}
