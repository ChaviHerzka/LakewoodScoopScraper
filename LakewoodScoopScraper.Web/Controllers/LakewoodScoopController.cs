
using LakewoodScoopScraper.Scraper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakewoodScoopScraper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LakewoodScoopController : ControllerBase
    {
        [HttpGet]
        [Route("scrape")]
        public List<LakewoodScoopResult> Scrape()
        {
            return Scraping.ScrapeLakewoodScoop();
        }
    }
}