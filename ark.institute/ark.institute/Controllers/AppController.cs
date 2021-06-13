using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ark.institute.Controllers
{
    [Route("api/inst")]
    [ApiController]
    public class AppController : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        public dynamic UploadFile()
        {
            return new Logic().PersistImage(Request.Form.Files);
        }
    }
}
