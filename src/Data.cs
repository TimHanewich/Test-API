using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestAPI;

namespace MyNamespace.Controllers
{
    [ApiController]
    [Route("data")]
    public class Data : ControllerBase
    {

        private DataStore _ds; //storing it locally here and using it throughout the class (it persists across multiple calls)

        public Data(DataStore ds) //the common value, the DataStore, that was registered as a singleton, is provided to each controller in its constructor
        {
            _ds = ds;
        }

        [HttpGet]
        public async Task Get()
        {
            Response.StatusCode = 200;
            Response.Headers.Append("Content-Type", "application/json");
            await Response.WriteAsync(_ds.Data.ToString());
        }

        [HttpPost]
        public async Task Post()
        {
            //Check correct header
            string? contenttype = Request.Headers["Content-Type"];
            if (contenttype == null || contenttype != "application/json")
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("You must provide data in JSON! Please ensure the data you provide is in JSON format and you specified the Content-Type header as application/json.");
                return;
            }

            //Get out body
            StreamReader sr = new StreamReader(Request.Body);
            string body = await sr.ReadToEndAsync();
            JObject jo;
            try
            {
                jo = JObject.Parse(body);
            }
            catch
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("There was an issue parsing the JSON out of the body. You must provide valid JSON!");
                return;
            }

            //Add it
            _ds.Add(jo);
        }

        //delete
        [HttpDelete]
        public async Task Delete()
        {
            //Check correct header
            string? contenttype = Request.Headers["Content-Type"];
            if (contenttype == null || contenttype != "application/json")
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("You must provide data in JSON! Please ensure the data you provide is in JSON format and you specified the Content-Type header as application/json.");
                return;
            }

            //Get out body
            StreamReader sr = new StreamReader(Request.Body);
            string body = await sr.ReadToEndAsync();
            JObject jo;
            try
            {
                jo = JObject.Parse(body);
            }
            catch
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("There was an issue parsing the JSON out of the body. You must provide valid JSON!");
                return;
            }

            //Find an object in the array that looks just like this one!
            JObject? ToDelete = null;
            foreach (JObject ptd in _ds.Data)
            {
                if (ptd.ToString() == jo.ToString())
                {
                    ToDelete = ptd;
                }
            }

            //not found?
            if (ToDelete == null)
            {
                Response.StatusCode = 404;
                await Response.WriteAsync("Unable to find the object you provided in the data.");
                return;
            }

            //Delete it
            _ds.Remove(ToDelete);

            //Success!
            Response.StatusCode = 204;
        }

    }
}
