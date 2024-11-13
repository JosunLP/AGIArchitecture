
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AGI.DataManagement;
using AGI.MLServices;
using AGI.NLPServices;
using AGI.ReasoningEngine;
using AGI.PlanningEngine;
using AGI.EthicsAndSafety;
using AGI.SelfMonitoring;
using Microsoft.Extensions.DependencyInjection;

namespace AGI.WebInterface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AGIController(
        DataManager dataManager,
        MachineLearningService mlService,
        NLPService nlpService,
        ReasoningEngine.ReasoningEngine reasoningEngine,
        PlanningEngine.PlanningEngine planningEngine,
        EthicsAndSafetyModule ethicsSafetyModule,
        SelfMonitoringModule selfMonitoringModule) : ControllerBase
    {
        private readonly DataManager _dataManager = dataManager;
        private readonly MachineLearningService _mlService = mlService;
        private readonly NLPService _nlpService = nlpService;
        private readonly ReasoningEngine.ReasoningEngine _reasoningEngine = reasoningEngine;
        private readonly PlanningEngine.PlanningEngine _planningEngine = planningEngine;
        private readonly EthicsAndSafetyModule _ethicsSafetyModule = ethicsSafetyModule;
        private readonly SelfMonitoringModule _selfMonitoringModule = selfMonitoringModule;

        // Test Data Setup
        [HttpPost("setup-test-data")]
        public IActionResult SetupTestData()
        {
            _dataManager.AddKnowledgeNodeAsync("AI Basics", "Introduction to AI").Wait();
            _dataManager.AddKnowledgeNodeAsync("Machine Learning", "Core concepts of machine learning").Wait();
            _planningEngine.AddGoal("Learn about AI", priority: 1);
            _planningEngine.AddGoal("Understand ML", priority: 2);
            _selfMonitoringModule.Log("Test data setup completed.");
            return Ok("Test data has been added successfully.");
        }

        // Machine Learning - Prediction Example
        [HttpPost("ml-predict")]
        public IActionResult MLPredict([FromBody] Dictionary<string, float> features)
        {
            // Example: Dummy input with a feature array
            // Would be extended in a real application
            var prediction = _mlService.Predict(features);
            return Ok(new { Prediction = prediction });
        }

        // NLP - Tokenize Text
        [HttpPost("nlp-tokenize")]
        public IActionResult TokenizeText([FromBody] string text)
        {
            var tokens = NLPService.Tokenize(text);
            return Ok(tokens);
        }

        // Reasoning - Add and Query Relation
        [HttpPost("reasoning-add-relation")]
        public IActionResult AddRelation(string subject, string relation, string obj)
        {
            _reasoningEngine.AddRelation(subject, relation, obj);
            return Ok("Relation added successfully.");
        }

        [HttpGet("reasoning-query-relation")]
        public IActionResult QueryRelation(string subject, string relation)
        {
            var results = _reasoningEngine.QueryRelation(subject, relation);
            return Ok(results);
        }
    }
}
