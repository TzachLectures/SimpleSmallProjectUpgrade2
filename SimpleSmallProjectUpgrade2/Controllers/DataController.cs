using LegacyLibrary;
using SimpleSmallProjectUpgrade2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Http;

namespace SimpleSmallProjectUpgrade2.Controllers
{
    public class DataController : ApiController
    {
        private readonly LegacyHelper _legacyHelper = new LegacyHelper();

        [HttpGet]
        [Route("api/data/convertdate")]
        public IHttpActionResult ConvertDate()
        {
            string dateString = "2023-10-08";
            try
            {
                DateTime convertedDate = _legacyHelper.ConvertDate(dateString);
                return Ok($"Converted Date: {convertedDate.ToString("yyyy-MM-dd HH:mm:ss")}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error converting date: {ex.Message}");
            }

        }

        [HttpGet]
        [Route("api/data/deserialize")]
        public IHttpActionResult DeserializeBinary()
        {
            try
            {
                var mockObject = new Person(){ Name = "Test Object", Age = 42 };

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, mockObject);  

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var deserializedObject = _legacyHelper.DeserializeBinary(memoryStream);

                    return Ok(new
                    {
                        Message = "Deserialization successful!",
                        OriginalObject = mockObject,
                        DeserializedObject = deserializedObject
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error during deserialization: {ex.Message}");
            }
        }
    }
}
