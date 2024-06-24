using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSensorData()
        {
            // 임의의 데이터 생성 (실제 데이터로 대체 가능)
            var random = new Random();
            var temperatureData = new List<int>();
            var pressureData = new List<int>();

            for (int i = 0; i < 8; i++)
            {
                temperatureData.Add(random.Next(20, 30)); // 예: 20도에서 30도 사이의 임의 값
                pressureData.Add(random.Next(900, 1100)); // 예: 900hPa에서 1100hPa 사이의 임의 값
            }

            return Ok(new { temperature = temperatureData, pressure = pressureData });
        }
    }
}