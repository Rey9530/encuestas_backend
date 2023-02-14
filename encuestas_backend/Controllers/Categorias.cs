using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace encuestas_backend.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class Categorias : ControllerBase
    {
        [HttpGet]
        [Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme )]
        public dynamic Get(){
            return NoContent();
        }
        
    }
}