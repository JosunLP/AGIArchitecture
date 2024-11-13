
using Microsoft.AspNetCore.Mvc;
using AGI.Adaptation;
using AGI.SelfMonitoring;
using System.Collections.Generic;

namespace AGI.WebInterface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdaptationController : ControllerBase
    {
        private readonly AdaptationModule _adaptationModule;
        private readonly SelfMonitoringModule _selfMonitoringModule;

        public AdaptationController(AdaptationModule adaptationModule, SelfMonitoringModule selfMonitoringModule)
        {
            _adaptationModule = adaptationModule;
            _selfMonitoringModule = selfMonitoringModule;
        }

        // Endpoint to list all available adaptation strategies
        [HttpGet("available-strategies")]
        public IActionResult GetAvailableStrategies()
        {
            var strategies = _adaptationModule.GetAvailableStrategies();
            return Ok(strategies);
        }

        // Endpoint to apply a specific adaptation strategy
        [HttpPost("apply-strategy")]
        public IActionResult ApplyAdaptationStrategy([FromQuery] string strategyName)
        {
            _adaptationModule.ApplyAdaptation(strategyName);
            return Ok($"Adaptation strategy '{strategyName}' applied successfully.");
        }

        // Endpoint to fetch logs from self-monitoring to observe adaptations
        [HttpGet("logs")]
        public IActionResult GetAdaptationLogs()
        {
            var logs = _selfMonitoringModule.GetLogs();
            return Ok(logs);
        }
    }
}
