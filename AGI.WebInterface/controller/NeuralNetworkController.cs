using Microsoft.AspNetCore.Mvc;
using AGI.NeuralNetworkModule;

[ApiController]
[Route("api/[controller]")]
public class NeuralNetworkController : ControllerBase
{
    private readonly ModelManagement _modelManagement;

    public NeuralNetworkController()
    {
        _modelManagement = new ModelManagement("/path/to/models");
    }

    [HttpPost("load-model")]
    public IActionResult LoadModel([FromBody] string modelPath)
    {
        try
        {
            var model = _modelManagement.LoadModel(modelPath);
            return Ok("Model loaded successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
