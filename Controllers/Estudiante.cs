using Microsoft.AspNetCore.Mvc;
using Estudiante;

namespace Estudiante.Controllers;

[ApiController]
[Route("api/estudiantes")]

public class EstudianteController : ControllerBase
{
    private List<Estudiante> _estudiantes = new List<Estudiante>
    {
        new Estudiante { ID = 1, Nombre = "Juan", Apellido = "Pérez", FechaNacimiento = new DateOnly(1995, 6, 15), CorreoElectronico = "juan@example.com" },
        new Estudiante { ID = 2, Nombre = "María", Apellido = "García", FechaNacimiento = new DateOnly(1998, 3, 25), CorreoElectronico = "maria@example.com" }
    };


    private readonly ILogger<EstudianteController> _logger;

    public EstudianteController(ILogger<EstudianteController> logger)
    {
        _logger = logger;
    }

    // POST
    [HttpPost(Name = "PostEstudiante")]
    public IActionResult Post(Estudiante estudiante)
    {
        // Asignar un ID único (simulación)
        estudiante.ID = _estudiantes.Count + 1;

        // Agregar el estudiante a la lista en memoria
        _estudiantes.Add(estudiante);

        Console.WriteLine("Contenido de la lista de estudiantes:");
        foreach (var e in _estudiantes)
        {
            Console.WriteLine($"ID: {e.ID}, Nombre: {e.Nombre}, Apellido: {e.Apellido}");
        }

        // Retornar una respuesta de éxito con el estudiante registrado
        return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.ID }, estudiante);
        
    }

    //GET Estudiantes
    [HttpGet(Name = "GetEstudiantes")]
    public IActionResult GetEstudiantes()
    {
        if (_estudiantes.Count == 0)
        {
            return Ok("No se encuentran estudiantes");
        }
    
        return Ok(_estudiantes);
    }

    //GET Estudiante por id
    [HttpGet("{id}", Name = "GetEstudiante")]
    public IActionResult GetEstudiante(int id)
    {
        var estudiante = _estudiantes.Find(e => e.ID == id);
        if (estudiante == null)
        {
            return NotFound();
        }
        return Ok(estudiante);
    }

    //PUT Estudiante por id
    [HttpPut("{id}", Name = "PutEstudiante")]
    public IActionResult PutEstudiante(int id)
    {
        var estudiante = _estudiantes.Find(e => e.ID == id);
        if (estudiante == null)
        {
            return NotFound();
        }

        
        Console.WriteLine("Contenido de la lista de estudiantes:");
        foreach (var e in _estudiantes)
        {
            Console.WriteLine($"ID: {e.ID}, Nombre: {e.Nombre}, Apellido: {e.Apellido}");
        }

        return Ok(estudiante);
    }
}