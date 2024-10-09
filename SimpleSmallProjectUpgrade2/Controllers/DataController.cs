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

        // GET: api/data/convertdate
        [HttpGet]
        [Route("api/data/convertdate")]
        public IHttpActionResult ConvertDate()
        {
            string dateString = "2023-10-08";
            try
            {
                var convertedDate = _legacyHelper.ConvertDate(dateString);
                return Ok($"Converted Date: {convertedDate.ToString("yyyy-MM-dd HH:mm:ss")}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error converting date: {ex.Message}");
            }

        }

        // POST: api/data/deserialize
        [HttpGet]
        [Route("api/data/deserialize")]
        public IHttpActionResult DeserializeBinary()
        {
            try
            {
                // Step 1: Create a mock object to serialize and deserialize.
                var mockObject = new Person(){ Name = "Test Object", Age = 42 };

                // Step 2: Serialize the mock object into a binary format using BinaryFormatter.
                using (var memoryStream = new MemoryStream())
                {
                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, mockObject);  // Serialize mock data

                    // Step 3: Reset the memory stream position to the beginning.
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    // Step 4: Deserialize the binary data using the LegacyHelper method.
                    var deserializedObject = _legacyHelper.DeserializeBinary(memoryStream);

                    // Step 5: Return the deserialized object in a response.
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
